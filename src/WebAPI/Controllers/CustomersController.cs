using Application.DTOs.Customer;
using Application.UseCases.Customer.Create;
using Application.UseCases.Customer.Delete;
using Application.UseCases.Customer.GetAll;
using Application.UseCases.Customer.GetById;
using Application.UseCases.Customer.Update;
using Asp.Versioning;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CustomersController(
    UpdateCustomer updateCustomer,
    DeleteCustomer deleteCustomer,
    CreateCustomer createCustomer,
    GetCustomerById getCustomerById,
    GetAllCustomers getAllCustomers) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var customersOutput = await getAllCustomers.ExecuteAsync();
            return Ok(customersOutput);
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
        {
            var customerOutput = await getCustomerById.ExecuteAsync(id);
            return Ok(customerOutput);
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAsync(CustomerRequest customer)
    {
        try
        {
            var customerOutput = await createCustomer.ExecuteAsync(customer);
            return Created($"/api/customers/{customerOutput.CustomerId}", customerOutput);
        }
        catch (DataContractValidationException e)
        {
            return BadRequest(new { message = e.Message, errors = e.ValidationErrors });
        }
        catch (ConflictException e)
        {
            return Conflict(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(CustomerUpdateRequest customer)
    {
        try
        {
            var updatedCustomer = await updateCustomer.ExecuteAsync(customer.CustomerId, customer);
            return Ok(updatedCustomer);
        }
        catch (ConflictException e)
        {
            return Conflict(new { message = e.Message });
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (DataContractValidationException e)
        {
            return StatusCode(StatusCodes.Status422UnprocessableEntity,
                new { message = e.Message, errors = e.ValidationErrors });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            var result = await deleteCustomer.ExecuteAsync(id);

            if (!result) throw new NotFoundException("Customer not found");

            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
}