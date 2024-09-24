using Application.DTOs.OrderDetail;
using Application.UseCases.OrderDetail.Delete;
using Application.UseCases.OrderDetail.GetAll;
using Application.UseCases.OrderDetail.GetById;
using Application.UseCases.OrderDetail.GetBySaleId;
using Application.UseCases.OrderDetail.Update;
using Asp.Versioning;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/order-details")]
public class OrderDetailController(
    GetOrderDetailByIdUseCase getOrderDetailByIdUseCase,
    GetAllOrdersDetailsUseCase getAllOrderDetailsUseCase,
    GetOrderDetailByOrderIdUseCase getOrderDetailByOrderIdUseCase,
    UpdateOrderDetailUseCase updateOrderDetailUseCase,
    UpdateOrderDetailByOrderIdUseCase updateOrderDetailByOrderIdUseCase,
    DeleteOrderDetailUseCase deleteOrderDetailUseCase) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OrderDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderDetailById(Guid id)
    {
        try
        {
            var response = await getOrderDetailByIdUseCase.ExecuteAsync(id);
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
    [ProducesResponseType(typeof(IEnumerable<OrderDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllOrderDetails()
    {
        try
        {
            var response = await getAllOrderDetailsUseCase.ExecuteAsync();
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
    [ProducesResponseType(typeof(OrderDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOrderDetail(Guid id, UpdateOrderDetailRequest updateOrderDetailRequest)
    {
        try
        {
            var response = await updateOrderDetailUseCase.ExecuteAsync(id, updateOrderDetailRequest);
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
    
    [HttpPut("order/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<OrderDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOrderDetailByOrderId(Guid id, UpdateOrderDetailRequest updateOrderDetailRequest)
    {
        try
        {
            var response = await updateOrderDetailByOrderIdUseCase.ExecuteAsync(id, updateOrderDetailRequest);
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
    public async Task<IActionResult> DeleteOrderDetail(Guid id)
    {
        try
        {
            var result = await deleteOrderDetailUseCase.ExecuteAsync(id);
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

    [HttpGet("order/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<OrderDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderDetailsByOrderId(Guid id)
    {
        try
        {
            var response = await getOrderDetailByOrderIdUseCase.ExecuteAsync(id);
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