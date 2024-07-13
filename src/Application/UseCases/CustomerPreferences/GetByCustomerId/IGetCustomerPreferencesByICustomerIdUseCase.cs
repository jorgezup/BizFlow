namespace Application.UseCases.CustomerPreferences.GetByCustomerId;

public interface IGetCustomerPreferencesByICustomerIdUseCase
{
    public Task<IEnumerable<Core.Entities.CustomerPreferences>> ExecuteAsync(Guid customerId);
}