using Application.DTOs.PriceHistory;
using Application.UseCases.PriceHistory.Create;
using Application.UseCases.PriceHistory.Delete;
using Application.UseCases.PriceHistory.GetById;
using Application.UseCases.PriceHistory.Update;
using Asp.Versioning;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/price-histories")]
[ApiController]
public class PriceHistoryController(
    CreatePriceHistory createPriceHistory,
    GetPriceHistoryById getPriceHistoryById,
    UpdatePriceHistory updatePriceHistory,
    DeletePriceHistory deletePriceHistory)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePriceHistory(PriceHistoryRequest request)
    {
        try
        {
            var response = await createPriceHistory.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetPriceHistoryById), new { id = response.Id }, response);
        }
        catch (BadRequestException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPriceHistoryById(Guid id)
    {
        try
        {
            var response = await getPriceHistoryById.ExecuteAsync(id);
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

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdatePriceHistory(Guid id, UpdatePriceHistoryRequest request)
    {
        try
        {
            var response = await updatePriceHistory.ExecuteAsync(id, request);
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

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletePriceHistory(Guid id)
    {
        try
        {
            var success = await deletePriceHistory.ExecuteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
        catch (BadRequestException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
}