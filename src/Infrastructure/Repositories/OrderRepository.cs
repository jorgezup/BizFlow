using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using OrderResponse = Core.DTOs.OrderResponse;

namespace Infrastructure.Repositories;

public class OrderRepository(AppDbContext appDbContext) : IOrderRepository
{
    public async Task AddAsync(Order order)
    {
        await appDbContext.Orders.AddAsync(order);
    }

    public async Task<OrderResponse> GetByIdAsync(Guid id)
    {
        return await appDbContext.Orders
            .Where(o => o.Id == id)
            .Select(order => new OrderResponse(
                order.Id,
                order.CustomerId,
                order.Customer.Name,
                order.OrderDate,
                order.OrderDetails.Sum(od => od.Subtotal),
                order.OrderDetails.Select(od => od.Product.Name).ToList(),
                order.OrderDetails.Select(od => od.Quantity).ToList(),
                order.OrderDetails.Select(od => od.UnitPrice).ToList(),
                order.OrderDetails.Select(od => od.Subtotal).ToList(),
                order.Generated,
                order.OrderLifeCycle.OrderByDescending(olc => olc.CreatedAt).FirstOrDefault().Status,
                order.Payment.PaymentMethod.ToString(),
                order.CreatedAt,
                order.UpdatedAt
            ))
            .FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(
        Guid? customerId,
        string status,
        DateTime? startDate,
        DateTime? endDate,
        string? sortColumn,
        string? sortDirection)
    {
        var query = appDbContext.Orders.AsQueryable();

        // Aplicar filtros
        if (customerId.HasValue)
        {
            query = query.Where(o => o.CustomerId == customerId.Value);
        }

        if (!string.IsNullOrEmpty(status))
        {
            if (Enum.TryParse(typeof(Status), status, out var parsedStatus))
            {
                query = query.Where(o => o.OrderLifeCycle
                    .OrderByDescending(olc => olc.CreatedAt)
                    .FirstOrDefault().Status == (Status)parsedStatus);
            }
        }

        if (startDate.HasValue)
        {
            query = query.Where(o => o.CreatedAt >= startDate.Value.Date);
        }

        if (endDate.HasValue)
        {
            query = query.Where(o => o.CreatedAt <= endDate.Value.Date.AddDays(1));
        }

        // Aplicar ordenação
        if (!string.IsNullOrEmpty(sortColumn))
        {
            query = sortColumn switch
            {
                "CustomerName" =>
                    sortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(o => o.Customer.Name)
                        : query.OrderBy(o => o.Customer.Name),
                _ => sortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(o => EF.Property<object>(o, sortColumn))
                    : query.OrderBy(o => EF.Property<object>(o, sortColumn))
            };
        }

        // Projeção para OrderResponse sem paginação
        return await query
            .Select(order => new OrderResponse(
                order.Id,
                order.CustomerId,
                order.Customer.Name,
                order.OrderDate,
                order.OrderDetails.Sum(od => od.Subtotal),
                order.OrderDetails.Select(od => od.Product.Name).ToList(),
                order.OrderDetails.Select(od => od.Quantity).ToList(),
                order.OrderDetails.Select(od => od.UnitPrice).ToList(),
                order.OrderDetails.Select(od => od.Subtotal).ToList(),
                order.Generated,
                order.OrderLifeCycle.OrderByDescending(olc => olc.CreatedAt).FirstOrDefault().Status,
                order.Payment.PaymentMethod.ToString(),
                order.CreatedAt,
                order.UpdatedAt
            ))
            .ToListAsync();
    }


    public async Task<IEnumerable<OrderResponse>> GetAllPaginatedOrdersAsync(
        int page,
        int pageSize,
        Guid? customerId,
        string? status,
        DateTime? startDate,
        DateTime? endDate,
        string? sortColumn,
        string? sortDirection)
    {
        var query = appDbContext.Orders.AsQueryable();

        // Apply filtering
        if (!string.IsNullOrEmpty(customerId.ToString()))
        {
            query = query.Where(o => o.CustomerId == customerId);
        }

        if (!string.IsNullOrEmpty(status))
        {
            if (Enum.TryParse(typeof(Status), status, out var parsedStatus))
            {
                query = query.Where(o => o.OrderLifeCycle
                    .OrderByDescending(olc => olc.CreatedAt)
                    .FirstOrDefault().Status == (Status)parsedStatus);
            }
        }

        if (startDate.HasValue)
        {
            query = query.Where(o => o.OrderDate >= startDate.Value.Date);
        }

        if (endDate.HasValue)
        {
            query = query.Where(o => o.OrderDate <= endDate.Value.Date.AddDays(1));
        }

        // Apply sorting
        query = ApplySorting(query, sortColumn, sortDirection);

        // Apply pagination
        var result = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(order => new OrderResponse(
                order.Id,
                order.CustomerId,
                order.Customer.Name,
                order.OrderDate,
                order.OrderDetails.Sum(od => od.Subtotal),
                order.OrderDetails.Select(od => od.Product.Name).ToList(),
                order.OrderDetails.Select(od => od.Quantity).ToList(),
                order.OrderDetails.Select(od => od.UnitPrice).ToList(),
                order.OrderDetails.Select(od => od.Subtotal).ToList(),
                order.Generated,
                order.OrderLifeCycle.OrderByDescending(olc => olc.CreatedAt).FirstOrDefault().Status,
                order.Payment.PaymentMethod.ToString(),
                order.CreatedAt,
                order.UpdatedAt
            ))
            .ToListAsync();

        return result;
    }

    public async Task<int> GetTotalPaginatedOrdersAsync(
        Guid? customerId,
        string? status,
        DateTime? startDate,
        DateTime? endDate)
    {
        var query = appDbContext.Orders.AsQueryable();

        // Apply the same filters as above
        if (!string.IsNullOrEmpty(customerId.ToString()))
        {
            query = query.Where(o => o.CustomerId == customerId);
        }

        if (!string.IsNullOrEmpty(status))
        {
            if (Enum.TryParse(typeof(Status), status, out var parsedStatus))
            {
                query = query.Where(o => o.OrderLifeCycle.OrderByDescending(olc => olc.CreatedAt)
                    .FirstOrDefault().Status == (Status)parsedStatus);
            }
        }

        if (startDate.HasValue)
        {
            query = query.Where(o => o.OrderDate >= startDate.Value.Date);
        }

        if (endDate.HasValue)
        {
            query = query.Where(o => o.OrderDate <= endDate.Value.Date.AddDays(1));
        }

        // Return the count of records without loading unnecessary relations
        return await query.CountAsync();
    }


    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        return await appDbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Include(o => o.OrderLifeCycle)
            .ToListAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        var existingOrder = await appDbContext.Orders.FindAsync(order.Id);
        if (existingOrder is not null)
        {
            appDbContext.Entry(existingOrder).CurrentValues.SetValues(order);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await appDbContext.Orders.FindAsync(id);
        if (order is not null)
        {
            appDbContext.Orders.Remove(order);
        }
    }

    private static IQueryable<Order> ApplySorting(IQueryable<Order> query, string? sortColumn, string? sortDirection)
    {
        if (!string.IsNullOrEmpty(sortColumn))
        {
            query = sortColumn switch
            {
                "Products" =>
                    sortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(o => o.OrderDetails.Select(od => od.Product.Name).FirstOrDefault())
                        : query.OrderBy(o => o.OrderDetails.Select(od => od.Product.Name).FirstOrDefault()),
                "CustomerName" =>
                    sortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(o => o.Customer.Name)
                        : query.OrderBy(o => o.Customer.Name),
                _ => sortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(e => EF.Property<object>(e, sortColumn))
                    : query.OrderBy(e => EF.Property<object>(e, sortColumn))
            };
        }

        return query;
    }
}