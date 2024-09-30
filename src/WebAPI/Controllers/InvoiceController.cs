using Application;
using Application.UseCases.Invoices.Generate;
using Asp.Versioning;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/invoices")]
[ApiController]
public class InvoiceController(GenerateInvoiceUseCase generateInvoiceUseCase) : ControllerBase
{
    [HttpGet("generate-invoice")]
    public async Task<IActionResult> GenerateInvoice(Guid customerId, DateTime startDate, DateTime endDate,
        string language = "pt-br")
    {
        try
        {
            var pdfBytes = await generateInvoiceUseCase.ExecuteAsync(customerId, startDate, endDate, language);

            return File(pdfBytes, "application/pdf", "invoice.pdf");
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
            
            // return NotFound(new { message = ResourceManagerService.GetString("NoOrdersInvoice", "pt-br") });
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
            // return BadRequest(new { message = ResourceManagerService.GetString("InvoicesBadRequest", "pt-br") });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}