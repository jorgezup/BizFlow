using Application.DTOs.Product;
using Application.UseCases.Product.Create;
using Application.UseCases.Product.Delete;
using Application.UseCases.Product.GetAll;
using Application.UseCases.Product.GetById;
using Application.UseCases.Product.Update;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/v{version:apiVersion}/products")]
[ApiController]
public class ProductController(
    CreateProductUseCase createProductUseCase,
    GetProductByIdUseCase getProductByIdUseCase,
    GetAllProductsUseCase getAllProductsUseCase,
    UpdateProductUseCase updateProductUseCase,
    DeleteProductUseCase deleteProductUseCase) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var productsOutput = await getAllProductsUseCase.ExecuteAsync();
            return Ok(productsOutput);
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
    public async Task<IActionResult> GetProductById(Guid id)
    {
        try
        {
            var productOutput = await getProductByIdUseCase.ExecuteAsync(id);
            return Ok(productOutput);
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

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProduct(ProductRequest product)
    {
        try
        {
            var productOutput = await createProductUseCase.ExecuteAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = productOutput.ProductId }, productOutput);
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
    public async Task<IActionResult> UpdateProduct(Guid id, ProductUpdateRequest request)
    {
        try
        {
            var updatedCustomer = await updateProductUseCase.ExecuteAsync(id, request);
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
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var result = await deleteProductUseCase.ExecuteAsync(id);

            if (!result) return NotFound();

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
}