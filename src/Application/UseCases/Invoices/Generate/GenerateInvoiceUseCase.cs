using Core.DTOs;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace Application.UseCases.Invoices.Generate;

public class GenerateInvoiceUseCase(IUnitOfWork unitOfWork) : IGenerateInvoiceUseCase
{
    public async Task<byte[]> ExecuteAsync(Guid customerId, DateTime startDate, DateTime endDate,
        string language)
    {
        Settings.License = LicenseType.Community;
        
        if (startDate > endDate)
            throw new BadRequestException("Start date cannot be greater than end date.");
        
        // the range of dates cannot be greater than 60 days
        if (startDate.AddDays(60) < endDate)
            throw new BadRequestException(ResourceManagerService.GetString("InvoicesBadRequest", language));
            // throw new BadRequestException("The range of dates cannot be more than 60 days or Start date cannot be greater than end date.");
        
        if (startDate == DateTime.MinValue)
            startDate = DateTime.Now.AddMonths(-1);
        
        if (endDate == DateTime.MinValue)
            endDate = DateTime.Now;

        var orders = await unitOfWork.OrderRepository.GetAllPaginatedOrdersAsync(
            1,
            int.MaxValue,
            customerId,
            Status.Completed.ToString(),
            startDate,
            endDate,
            "OrderDate",
            "asc");

        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerId);

        var products = await unitOfWork.ProductRepository.GetAllAsync();

        if (!orders.Any())
            // throw new NotFoundException("No orders found for the specified customer and date range.");
            throw new NotFoundException(ResourceManagerService.GetString("NoOrdersInvoice", language));

        var pdf = GeneratePdf(orders, customer, products, startDate, endDate, language);

        return pdf;
    }

    private static byte[] GeneratePdf(IEnumerable<OrderResponse> orders, Core.Entities.Customer customer,
        IEnumerable<Core.Entities.Product> products, DateTime startDate, DateTime endDate, string language)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                var culture = language; // Definir a cultura conforme o parâmetro de linguagem
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.Header().Text(ResourceManagerService.GetString("CustomerInvoice", culture))
                    .FontSize(16).Bold().AlignCenter();
                
                page.Footer().AlignCenter().Text(ResourceManagerService.GetString("ThankYou", culture));

                page.Content().Column(column =>
                {
                    column.Spacing(3);
                    column.Item().PaddingTop(2);
                    
                    // Detalhes do Cliente
                    column.Item().Text($"{ResourceManagerService.GetString("Name", culture)}: {customer.Name}")
                        .FontSize(10);
                    column.Item().Text($"{ResourceManagerService.GetString("Email", culture)}: {customer.Email}")
                        .FontSize(10);
                    column.Item().Text($"{ResourceManagerService.GetString("Address", culture)}: {customer.Address}")
                        .FontSize(10);
                    column.Item()
                        .Text($"{ResourceManagerService.GetString("Date", culture)}: {DateTime.Now:dd/MM/yyyy}")
                        .FontSize(10);

                    column.Item().LineHorizontal(1);
                    column.Item().PaddingTop(2);

                    // Detalhes do Período
                    column.Item()
                        .Text(
                            $"{ResourceManagerService.GetString("InvoicePeriod", culture)}: {startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}")
                        .Bold().FontSize(12).AlignCenter();
                    column.Item().Text($"{ResourceManagerService.GetString("TotalOrders", culture)}: {orders.Count()}")
                        .FontSize(10);
                    column.Item()
                        .Text(
                            $"{ResourceManagerService.GetString("TotalAmount", culture)}: {orders.Sum(o => o.TotalAmount):C}")
                        .FontSize(10);

                    column.Item().LineHorizontal(1);
                    column.Item().PaddingTop(2);

                    // Título dos Pedidos
                    column.Item().Text(ResourceManagerService.GetString("OrdersSummary", culture)).Bold().FontSize(12)
                        .AlignCenter();
                    column.Item().PaddingBottom(2);

                    foreach (var order in orders)
                    {
                        column.Item().Text($"{ResourceManagerService.GetString("OrderID", culture)}: {order.Id}").FontSize(10);
                        column.Item()
                            .Text(
                                $"{ResourceManagerService.GetString("OrderDate", culture)}: {order.OrderDate:dd/MM/yyyy}")
                            .FontSize(10);
                        column.Item()
                            .Text(
                                $"{ResourceManagerService.GetString("TotalOrderValue", culture)}: {order.TotalAmount:C}")
                            .FontSize(10);

                        // Espaçamento entre pedidos
                        column.Item().Padding(2);

                        // Tabela para exibir produtos, quantidade, preço unitário, e subtotal
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(4);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().AlignLeft().Text(ResourceManagerService.GetString("Product", culture)).FontSize(10);
                                header.Cell().AlignRight().Text(ResourceManagerService.GetString("Quantity", culture)).FontSize(10);
                                header.Cell().AlignRight().Text(ResourceManagerService.GetString("UnitPrice", culture)).FontSize(10);
                                header.Cell().AlignRight().Text(ResourceManagerService.GetString("Subtotal", culture)).FontSize(10);
                                header.Cell().AlignRight().Text(ResourceManagerService.GetString("Payment", culture)).FontSize(10);
                            });

                            for (var i = 0; i < order.Products.Count; i++)
                            {
                                var paymentMethod = GetPaymentMethod(order.PaymentMethod, culture);
                                // var paymentMethod = "aaa";
                                Console.WriteLine(paymentMethod);
                                table.Cell().AlignLeft().Text(order.Products[i]).FontSize(10);
                                table.Cell().AlignRight().Text($"{order.Quantity[i]:F3}").FontSize(10);
                                table.Cell().AlignRight().Text($"{order.UnitPrice[i]:C}").FontSize(10);
                                table.Cell().AlignRight().Text($"{order.Subtotal[i]:C}").FontSize(10);
                                // table.Cell().AlignRight().Text(paymentMethod).FontSize(10);
                                table.Cell().AlignRight().Text(paymentMethod).FontSize(10);
                            }
                        });
                        column.Item().PaddingTop(2);
                        column.Item().LineHorizontal(1);
                        column.Item().PaddingTop(2);
                    }
                });
            });
        });
        
        // document.ShowInPreviewer();
        return document.GeneratePdf();
    }
    
    private static string GetPaymentMethod(string paymentMethod, string culture)
    {
        return paymentMethod switch
        {
            "Cash" => ResourceManagerService.GetString(paymentMethod, culture),
            "Pix" => ResourceManagerService.GetString(paymentMethod, culture),
            "Courtesy" => ResourceManagerService.GetString(paymentMethod, culture),
            "" => ResourceManagerService.GetString("NoPayment", culture),
            null => ResourceManagerService.GetString("NoPayment", culture),
            _ => paymentMethod
        };
    }
}