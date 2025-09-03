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
    }
}
