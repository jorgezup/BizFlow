using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await customerService.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await customerService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task AddAsync(Customer customer)
    {
        await customerService.AddAsync(customer);
    }

    [HttpPut]
    public async Task UpdateAsync(Customer customer)
    {
        await customerService.UpdateAsync(customer);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteAsync(Guid id)
    {
        await customerService.DeleteAsync(id);
    }
}