using Application.DTOs.CustomerPreferences;
using Application.UseCases.CustomerPreferences.Create;
using Application.UseCases.CustomerPreferences.Delete;
using Application.UseCases.CustomerPreferences.GetAll;
using Application.UseCases.CustomerPreferences.GetById;
using Application.UseCases.CustomerPreferences.Update;
using Asp.Versioning;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/customer-preferences")]
public class CustomerPreferencesController(
    CreateCustomerPreferences createCustomerPreferences,
    GetCustomerPreferencesById getCustomerPreferencesById,
    UpdateCustomerPreferences updateCustomerPreferences,
    DeleteCustomerPreferences deleteCustomerPreferences,
    GetAllCustomerPreferences getAllCustomerPreferences)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CustomerPreferencesResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCustomerPreferences(CustomerPreferencesRequest request)
    {
        try
        {
            var response = await createCustomerPreferences.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetCustomerPreferencesById), new { id = response.Id }, response);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (DataContractValidationException e)
        {
            return BadRequest(new { message = e.Message, errors = e.ValidationErrors });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerPreferencesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCustomerPreferences()
    {
        try
        {
            var response = await getAllCustomerPreferences.ExecuteAsync();
            return Ok(response);
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
    [ProducesResponseType(typeof(CustomerPreferencesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomerPreferencesById(Guid id)
    {
        try
        {
            var response = await getCustomerPreferencesById.ExecuteAsync(id);
            return Ok(response);
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

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CustomerPreferencesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCustomerPreferences(Guid id, UpdateCustomerPreferencesRequest request)
    {
        try
        {
            var response = await updateCustomerPreferences.ExecuteAsync(id, request);
            return Ok(response);
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

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCustomerPreferences(Guid id)
    {
        try
        {
            var success = await deleteCustomerPreferences.ExecuteAsync(id);
            if (!success) return NotFound();
            return NoContent();
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
}