using ApiPerformanceComparison.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ApiPerformanceComparison.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly Dictionary<int, Product> _products;
        private readonly AtomicCounter _counter;

        public ProductsController(Dictionary<int, Product> products, AtomicCounter counter)
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
            if (_products.TryGetValue(id, out var product))
                return Ok(product);

            return NotFound();
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
            if (_products.Remove(id))
                return NoContent();

            return NotFound();
        }
    }
}
