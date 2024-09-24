namespace Application.UseCases.Invoices.Generate;

public interface IGenerateInvoiceUseCase
{
    Task<byte[]> ExecuteAsync(Guid customerId, DateTime startDate, DateTime endDate, string language);
}