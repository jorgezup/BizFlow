using Application.DTOs.Payment;
using Application.UseCases.Payment.GetPendingPayments;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces;

public class GetPendingPaymentsUseCase(IUnitOfWork unitOfWork) : IGetPendingPaymentsUseCase
{
    public async Task<PendingPaymentResponse> ExecuteAsync(Guid? customerId, DateTime? startDate, DateTime? endDate)
    {
        // Obter todos os pedidos concluídos com os filtros aplicados
        var allCompletedOrders = await unitOfWork.OrderRepository.GetAllOrdersAsync(
            customerId: customerId,
            status: Status.Completed.ToString(),
            startDate: startDate,
            endDate: endDate,
            sortColumn: null,
            sortDirection: null
        );

        // Obter todos os pagamentos com os filtros aplicados
        var allPayments = await unitOfWork.PaymentRepository.GetAllPaymentsAsync(
            customerId: customerId,
            startDate: startDate,
            endDate: endDate,
            sortColumn: null,
            sortDirection: null
        );

        // Obter IDs dos pedidos que já possuem pagamento
        var paidOrderIds = allPayments.Select(p => p.OrderId).ToHashSet();

        // Filtrar pedidos concluídos que não possuem pagamentos associados
        var ordersWithoutPayment = allCompletedOrders
            .Where(order => !paidOrderIds.Contains(order.Id))
            .ToList();

        // Agrupar e somar os valores pendentes por cliente
        var customerPendingPayments = ordersWithoutPayment
            .GroupBy(order => new { order.CustomerId, order.CustomerName })
            .Select(group => new CustomerPendingPayment
            {
                CustomerId = group.Key.CustomerId,
                CustomerName = group.Key.CustomerName,
                TotalPendingAmount = group.Sum(o => o.TotalAmount)
            })
            .ToList();

        // Calcular o total pendente geral
        var totalPendingAmount = ordersWithoutPayment.Sum(order => order.TotalAmount);

        return new PendingPaymentResponse
        {
            Orders = ordersWithoutPayment,
            CustomerPendingPayment = customerPendingPayments,
            TotalPendingAmount = totalPendingAmount,
        };
    }
}
