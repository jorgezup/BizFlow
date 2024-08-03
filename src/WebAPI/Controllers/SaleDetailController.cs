using Application.DTOs.SaleDetail;
using Application.UseCases.SaleDetail.Create;
using Application.UseCases.SaleDetail.Delete;
using Application.UseCases.SaleDetail.GetAll;
using Application.UseCases.SaleDetail.GetById;
using Application.UseCases.SaleDetail.GetBySaleId;
using Application.UseCases.SaleDetail.Update;
using Asp.Versioning;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/sale-details")]
public class SaleDetailController(
    GetSaleDetailByIdUseCase getSaleDetailByIdUseCase,
    GetAllSalesDetailsUseCase getAllSaleDetailsUseCase,
    GetSaleDetailBySaleIdUseCase getSaleDetailBySaleIdUseCase,
    // CreateSaleDetailUseCase createSaleDetailUseCase,
    UpdateSaleDetailUseCase updateSaleDetailUseCase,
    DeleteSaleDetailUseCase deleteSaleDetailUseCase) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SaleDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSaleDetailById(Guid id)
    {
        try
        {
            var response = await getSaleDetailByIdUseCase.ExecuteAsync(id);
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
    [ProducesResponseType(typeof(IEnumerable<SaleDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllSaleDetails()
    {
        try
        {
            var response = await getAllSaleDetailsUseCase.ExecuteAsync();
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

    // [HttpPost]
    // [ProducesResponseType(typeof(SaleDetailResponse), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status409Conflict)]
    // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public async Task<IActionResult> CreateSaleDetail(SaleDetailRequest createSaleDetailRequest)
    // {
    //     try
    //     {
    //         var response = await createSaleDetailUseCase.ExecuteAsync(createSaleDetailRequest);
    //         return CreatedAtAction(nameof(GetSaleDetailById), new { id = response.Id }, response);
    //     }
    //     catch (DataContractValidationException e)
    //     {
    //         return BadRequest(new { message = e.Message, errors = e.ValidationErrors });
    //     }
    //     catch (ConflictException e)
    //     {
    //         return Conflict(new { message = e.Message });
    //     }
    //     catch (Exception e)
    //     {
    //         return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
    //     }
    // }

    [HttpPut]
    [ProducesResponseType(typeof(SaleDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateSaleDetail(Guid id, UpdateSaleDetailRequest updateSaleDetailRequest)
    {
        try
        {
            var response = await updateSaleDetailUseCase.ExecuteAsync(id, updateSaleDetailRequest);
            return Ok(response);
        }
        catch (DataContractValidationException e)
        {
            return BadRequest(new { message = e.Message, errors = e.ValidationErrors });
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
    public async Task<IActionResult> DeleteSaleDetail(Guid id)
    {
        try
        {
            var result = await deleteSaleDetailUseCase.ExecuteAsync(id);
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

    [HttpGet("sale/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<SaleDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSaleDetailsBySaleId(Guid id)
    {
        try
        {
            var response = await getSaleDetailBySaleIdUseCase.ExecuteAsync(id);
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
}