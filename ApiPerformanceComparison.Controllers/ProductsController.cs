using ApiPerformanceComparison.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ApiPerformanceComparison.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly List<Product> _products;

        public ProductsController(List<Product> products)
        {
            _products = products;
        }

        // GET /products/123
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return product is null ? NotFound() : Ok(product);
        }

        // GET /products/list?count=50
        [HttpGet("list")]
        public IEnumerable<Product> GetList([FromQuery] int count = 50)
        {
            return _products.Take(count);
        }

        // POST /products
        [HttpPost]
        public ActionResult<Product> Create([FromBody] Product newProduct)
        {
            if (newProduct == null)
                return BadRequest();

            // Generate a new Id if necessary
            newProduct.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(newProduct);

            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        // PUT /products/123
        [HttpPut("{id}")]
        public ActionResult<Product> Update(int id, [FromBody] Product updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct is null)
                return NotFound();

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            return Ok(existingProduct);
        }

        // DELETE /products/123
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product is null)
                return NotFound();

            _products.Remove(product);
            return NoContent();
        }
    }
}
