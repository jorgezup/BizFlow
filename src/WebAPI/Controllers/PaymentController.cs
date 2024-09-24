using Application.DTOs;
using Application.DTOs.Paginate;
using Application.DTOs.Payment;
using Application.UseCases.Payment.Create;
using Application.UseCases.Payment.Delete;
using Application.UseCases.Payment.GetAll;
using Application.UseCases.Payment.GetById;
using Application.UseCases.Payment.GetPendingPayments;
using Application.UseCases.Payment.Update;
using Asp.Versioning;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/payments")]
[ApiController]
public class PaymentController(
    CreatePaymentUseCase createPaymentUseCase,
    GetPaymentByIdUseCase getPaymentByIdUseCase,
    GetAllPaymentsUseCase getAllPaymentsUseCase,
    UpdatePaymentUseCase updatePaymentUseCase,
    DeletePaymentUseCase deletePaymentUseCase,
    GetPendingPaymentsUseCase getPendingPaymentsUseCase) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePayment(PaymentRequest request)
    {
        try
        {
            var response = await createPaymentUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetPaymentById), new { response.Id }, response);
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
    public async Task<IActionResult> GetPaymentById(Guid id)
    {
        try
        {
            var response = await getPaymentByIdUseCase.ExecuteAsync(id);
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllPayments(
        int page = 1,
        int pageSize = 25,
        Guid? customerId = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string? sortColumn = "PaymentDate",
        string? sortDirection = "desc")
    {
        try
        {
            var response = await getAllPaymentsUseCase.ExecuteAsync(
                page,
                pageSize,
                customerId,
                startDate,
                endDate,
                sortColumn,
                sortDirection);

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

    [HttpGet("pending-payments")]
    [ProducesResponseType(typeof(PaginatedResponse<PendingPaymentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPendingPayments([FromQuery] Guid? customerId, [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await getPendingPaymentsUseCase.ExecuteAsync(customerId, startDate, endDate, page, pageSize);
            return Ok(result);
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
    public async Task<IActionResult> UpdatePayment(Guid paymentId, PaymentUpdateRequest request)
    {
        try
        {
            var response = await updatePaymentUseCase.ExecuteAsync(paymentId, request);
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
    public async Task<IActionResult> DeletePayment(Guid id)
    {
        try
        {
            var response = await deletePaymentUseCase.ExecuteAsync(id);
            if (!response)
                return NotFound();
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
}