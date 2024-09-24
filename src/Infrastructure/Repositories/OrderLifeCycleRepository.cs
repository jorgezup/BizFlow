using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class OrderLifeCycleRepository(AppDbContext appDbContext) : IOrderLifeCycleRepository
{
    public async Task AddAsync(OrderLifeCycle orderLifeCycle)
    {
        await appDbContext.OrderLifeCycles.AddAsync(orderLifeCycle);
    }
}