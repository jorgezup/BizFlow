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
[Route("/api/v{version:apiVersion}/customers")]
[ApiController]
public class CustomerController(
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
    public async Task<IActionResult> GetAllCustomers()
    {
        try
        {
            var customersOutput = await getAllCustomers.ExecuteAsync();
            return Ok(customersOutput);
        }
        catch (NotFoundException)
        {
            return NotFound();
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
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        try
        {
            var response = await getCustomerById.ExecuteAsync(id);
            return Ok(response);
        }
        catch (BadRequestException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (NotFoundException)
        {
            return NotFound();
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
    public async Task<IActionResult> CreateCustomer(CustomerRequest customer)
    {
        try
        {
            var response = await createCustomer.ExecuteAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = response.CustomerId }, response);
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
    public async Task<IActionResult> UpdateCustomer(Guid customerId, CustomerUpdateRequest customer)
    {
        try
        {
            var updatedCustomer = await updateCustomer.ExecuteAsync(customerId, customer);
            return Ok(updatedCustomer);
        }
        catch (ConflictException e)
        {
            return Conflict(new { message = e.Message });
        }
        catch (NotFoundException)
        {
            return NotFound();
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
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        try
        {
            var result = await deleteCustomer.ExecuteAsync(id);

            if (!result) return NotFound();

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
}