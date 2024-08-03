using Application.DTOs.PriceHistory;
using Application.UseCases.PriceHistory.Create;
using Application.UseCases.PriceHistory.Delete;
using Application.UseCases.PriceHistory.GetAll;
using Application.UseCases.PriceHistory.GetById;
using Application.UseCases.PriceHistory.GetByProductId;
using Application.UseCases.PriceHistory.Update;
using Asp.Versioning;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/price-histories")]
[ApiController]
public class PriceHistoryController(
    CreatePriceHistoryUseCase createPriceHistoryUseCase,
    GetPriceHistoryByIdUseCase getPriceHistoryByIdUseCase,
    UpdatePriceHistoryUseCase updatePriceHistoryUseCase,
    DeletePriceHistoryUseCase deletePriceHistoryUseCase,
    GetAllPriceHistoriesUseCase getAllPriceHistoriesUseCase,
    GetPriceHistoryByProductIdUseCase getPriceHistoryByProductIdUseCase)
    : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePriceHistory(PriceHistoryRequest request)
    {
        try
        {
            var response = await createPriceHistoryUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetPriceHistoryById), new { id = response.Id }, response);
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

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPriceHistoryById(Guid id)
    {
        try
        {
            var response = await getPriceHistoryByIdUseCase.ExecuteAsync(id);
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

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdatePriceHistory(Guid id, UpdatePriceHistoryRequest request)
    {
        try
        {
            var response = await updatePriceHistoryUseCase.ExecuteAsync(id, request);
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
            var success = await deletePriceHistoryUseCase.ExecuteAsync(id);
            if (!success) return NotFound();
            return NoContent();
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllPriceHistories()
    {
        try
        {
            var response = await getAllPriceHistoriesUseCase.ExecuteAsync();
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

    [HttpGet("{productId:guid}/product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPriceHistoryByProductId(Guid productId)
    {
        try
        {
            var response = await getPriceHistoryByProductIdUseCase.ExecuteAsync(productId);
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
}