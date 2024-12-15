using CatalogApi.Models;
using CatalogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly CatalogsService _catalogsService;

    public CatalogController(CatalogsService catalogsService){
        _catalogsService = catalogsService;
    }
        

    [HttpGet]
    public async Task<List<Product>> GetProductsAsync() {
        return await _catalogsService.GetProductsAsync();
    }

    [HttpGet("{id:length(24)}", Name = "GetProductByIdAsync")]
    public async Task<ActionResult<Product>> GetProductByIdAsync(string id)
    {
        var product = await _catalogsService.GetProductByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(Product product)
    {
        await _catalogsService.CreateProductAsync(product);

        return CreatedAtRoute("GetProductByIdAsync", new { id = product.Id }, product);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateProductAsync(string id, Product updatedProduct)
    {
        var product = await _catalogsService.GetProductByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        updatedProduct.Id = product.Id;

        await _catalogsService.UpdateProductAsync(id, updatedProduct);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteProductAsync(string id)
    {
        var product = await _catalogsService.GetProductByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        await _catalogsService.RemoveProductAsync(id);

        return NoContent();
    }
}