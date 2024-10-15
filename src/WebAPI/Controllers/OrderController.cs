using Application.DTOs.Order;
using Application.UseCases.Order.Create;
using Application.UseCases.Order.Delete;
using Application.UseCases.Order.GenerateOrders;
using Application.UseCases.Order.GetAll;
using Application.UseCases.Order.GetById;
using Application.UseCases.Order.Update;
using Asp.Versioning;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using OrderResponse = Core.DTOs.OrderResponse;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/orders")]
[ApiController]
public class OrderController(
    GetOrderByIdUseCase getOrderByIdUseCase,
    UpdateOrderStatusUseCase updateOrderStatusUseCase,
    CreateOrderUseCase createOrderUseCase,
    GenerateOrdersUseCase generateOrdersUseCase,
    GetAllPaginatedOrdersUseCase getAllPaginatedOrdersUseCase,
    DeleteOrderUseCase deleteOrderUseCase
) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        try
        {
            var orderFound = await getOrderByIdUseCase.ExecuteAsync(id);
            return Ok(orderFound);
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderUpdateStatusRequest status)
    {
        try
        {
            await updateOrderStatusUseCase.ExecuteAsync(id, status);
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


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllOrders(
        int page = 1,
        int pageSize = 25,
        Guid? customerId = null,
        string? status = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string? sortColumn = "OrderDate",
        string? sortDirection = "desc")
    {
        try
        {
            var orders = await getAllPaginatedOrdersUseCase.ExecuteAsync(
                page,
                pageSize,
                customerId,
                status,
                startDate,
                endDate,
                sortColumn,
                sortDirection);
            return Ok(orders);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOrder(OrderRequest orderRequest)
    {
        try
        {
            var orderResponse = await createOrderUseCase.ExecuteAsync(orderRequest);
            // return CreatedAtAction(nameof(GetOrderById), new { id = orderResponse.Id }, orderRequest);
            return Created("", orderResponse);
        }
        catch (DataContractValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpPost("generate-orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GenerateOrders()
    {
        try
        {
            await generateOrdersUseCase.ExecuteAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        try
        {
            await deleteOrderUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
}