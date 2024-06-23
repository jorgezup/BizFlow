using Application.DTOs.Product;
using Application.UseCases.Product.Create;
using Application.UseCases.Product.Delete;
using Application.UseCases.Product.GetAll;
using Application.UseCases.Product.GetById;
using Application.UseCases.Product.Update;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ProductsController(
    CreateProduct createProduct,
    GetProductById getProductById,
    GetAllProducts getAllProducts,
    UpdateProduct updateProduct,
    DeleteProduct deleteProduct) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var productsOutput = await getAllProducts.ExecuteAsync();
            return Ok(productsOutput);
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
        {
            var productOutput = await getProductById.ExecuteAsync(id);
            return Ok(productOutput);
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProduct(ProductRequest product)
    {
        try
        {
            var productOutput = await createProduct.ExecuteAsync(product);
            return Created($"/api/products/{productOutput.ProductId}", productOutput);
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

    [HttpPut]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(Guid id, ProductUpdateRequest request)
    {
        try
        {
            var updatedCustomer = await updateProduct.ExecuteAsync(id, request);
            return Ok(updatedCustomer);
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (DataContractValidationException e)
        {
            return StatusCode(StatusCodes.Status422UnprocessableEntity,
                new { message = e.Message, errors = e.ValidationErrors });
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
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            var result = await deleteProduct.ExecuteAsync(id);

            if (!result) throw new NotFoundException("Product not found");

            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
}