using Application.DTOs.Sale;
using Application.UseCases.Sale.Create;
using Application.UseCases.Sale.Delete;
using Application.UseCases.Sale.GetAll;
using Application.UseCases.Sale.GetById;
using Application.UseCases.Sale.Update;
using Asp.Versioning;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/sales")]
public class SaleController(
    GetSaleByIdUseCase getSaleByIdUseCase,
    GetAllSalesUseCase getAllSalesUseCase,
    CreateSaleUseCase createSaleUseCase,
    UpdateSaleUseCase updateSaleUseCase,
    DeleteSaleUseCase deleteSaleUseCase) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSaleById(Guid id)
    {
        try
        {
            var response = await getSaleByIdUseCase.ExecuteAsync(id);
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
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllSales()
    {
        try
        {
            var response = await getAllSalesUseCase.ExecuteAsync();
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
    
    [HttpPost]
    [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateSale(SaleRequest sale)
    {
        try
        {
            var response = await createSaleUseCase.ExecuteAsync(sale);
            return CreatedAtAction(nameof(GetSaleById), new { id = response.Id }, response);
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
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateSale(Guid id, UpdateSaleRequest sale)
    {
        try
        {
            var response = await updateSaleUseCase.ExecuteAsync(id, sale);
            return Ok(response);
        }
        catch (DataContractValidationException e)
        {
            return BadRequest(new { message = e.Message, errors = e.ValidationErrors });
        }
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteSale(Guid id)
    {
        try
        {
            var result = await deleteSaleUseCase.ExecuteAsync(id);
            if (!result) return NotFound();
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