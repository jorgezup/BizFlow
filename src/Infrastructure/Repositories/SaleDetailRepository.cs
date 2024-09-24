using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SaleDetailRepository(AppDbContext appDbContext) : ISaleDetailRepository
{
    public async Task<OrderDetailResponse?> GetByIdAsync(Guid id)
    {
        return await appDbContext.OrderDetails
            .Where(sd => sd.Id == id)
            .Select(sd => new OrderDetailResponse(
                sd.Id,
                sd.OrderId,
                sd.ProductId,
                sd.Order.CustomerId,
                sd.Order.Customer.Name,
                sd.Product.Name,
                sd.Quantity,
                sd.UnitPrice,
                sd.Subtotal,
                sd.CreatedAt,
                sd.UpdatedAt
            ))
            .FirstOrDefaultAsync();
    }


    public async Task<List<OrderDetailResponse>> GetAllAsync()
    {
        return await appDbContext.OrderDetails
            .Select(sd => new OrderDetailResponse(
                sd.Id,
                sd.OrderId,
                sd.ProductId,
                sd.Order.CustomerId,
                sd.Order.Customer.Name,
                sd.Product.Name,
                sd.Quantity,
                sd.UnitPrice,
                sd.Subtotal,
                sd.CreatedAt,
                sd.UpdatedAt
            ))
            .ToListAsync();
    }

    public async Task<List<OrderDetailResponse>> GetByOrderIdAsync(Guid orderId)
    {
        return await appDbContext.OrderDetails
            .Where(sd => sd.OrderId == orderId)
            .Select(sd => new OrderDetailResponse(
                sd.Id,
                sd.OrderId,
                sd.ProductId,
                sd.Order.CustomerId,
                sd.Order.Customer.Name,
                sd.Product.Name,
                sd.Quantity,
                sd.UnitPrice,
                sd.Subtotal,
                sd.CreatedAt,
                sd.UpdatedAt
            ))
            .ToListAsync();
    }

    public async Task AddAsync(OrderDetail orderDetail)
    {
        await appDbContext.OrderDetails.AddAsync(orderDetail);
    }

    public Task UpdateAsync(OrderDetail orderDetail)
    {
        appDbContext.OrderDetails.Update(orderDetail);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var saleDetail = await appDbContext.OrderDetails.FindAsync(id);
        if (saleDetail is not null)
        {
            appDbContext.OrderDetails.Remove(saleDetail);
        }
    }
}