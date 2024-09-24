using Core.Entities;

namespace Core.Interfaces;

public interface IOrderLifeCycleRepository
{
    Task AddAsync(OrderLifeCycle orderLifeCycle);
}