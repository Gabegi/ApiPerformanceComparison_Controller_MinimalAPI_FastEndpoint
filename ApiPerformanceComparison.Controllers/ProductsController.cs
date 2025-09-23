using ApiPerformanceComparison.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace ApiPerformanceComparison.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly ConcurrentDictionary<int, Product> _products;
        private readonly AtomicCounter _counter;

        public ProductsController(ConcurrentDictionary<int, Product> products, AtomicCounter counter)
        {
            _products = products;
            _counter = counter;
        }

        [HttpGet("list")]
        public IActionResult GetProducts(int count)
        {
            var result = _products.Values.Take(count).ToList();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            return _products.TryGetValue(id, out var product)
                ? Ok(product)
                : NotFound();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product newProduct)
        {
            if (newProduct == null)
                return BadRequest();

            newProduct.Id = _counter.GetNext();
            _products[newProduct.Id] = newProduct;

            return Created($"/products/{newProduct.Id}", newProduct);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct(int id, Product updatedProduct)
        {
            if (!_products.TryGetValue(id, out var existingProduct))
                return NotFound();

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            return Ok(existingProduct);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            return _products.TryRemove(id, out _)
                ? NoContent()
                : NotFound();
        }
    }
}
