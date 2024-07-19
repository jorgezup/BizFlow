using Application.DTOs.Payment;
using Application.UseCases.Payment.Create;
using Application.UseCases.Payment.GetById;
using Application.UseCases.Payment.GetBySaleId;
using Application.UseCases.Payment.GetRemainingBalanceForCustomer;
using Application.UseCases.Payment.GetTotalPaymentsForCustomer;
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
    GetPaymentsBySaleIdUseCaseUseCase getPaymentsBySaleIdUseCaseUseCase,
    GetRemainingBalanceForCustomerUseCase getRemainingBalanceForCustomerUseCase,
    GetTotalPaymentsForCustomerUseCase getTotalPaymentsForCustomerUseCase,
    UpdatePaymentUseCase updatePaymentUseCase) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
    {
        try
        {
            var response = await createPaymentUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetPaymentById), new { id = response.Id }, response);
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


    [HttpGet("sales/{saleId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaymentsBySaleId(Guid saleId)
    {
        try
        {
            var response = await getPaymentsBySaleIdUseCaseUseCase.ExecuteAsync(saleId);
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

    [HttpGet("remaining-balance/{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRemainingBalanceForCustomer(Guid customerId)
    {
        try
        {
            var response = await getRemainingBalanceForCustomerUseCase.ExecuteAsync(customerId);
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

    [HttpGet("total-payments/{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTotalPaymentsForCustomer(Guid customerId)
    {
        try
        {
            var response = await getTotalPaymentsForCustomerUseCase.ExecuteAsync(customerId);
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