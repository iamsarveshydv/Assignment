using Carl_Assignment.Entity;
using Carl_Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carl_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }       

        [HttpPost]
        public async Task<IActionResult> AddProducts([FromBody] List<ProductDto> productdto)
        {
            var product = await _productService.AddProducts(productdto);
            var productresult = product.Item1;
            var error = product.Item2;

            if (error.error_code == 400)
                return BadRequest(error);
            else
                return Created("URI is going here", productresult);
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var result = await _productService.GetProduct();

            var productresult = result.Item1;
            var error = result.Item2;

            if (error.error_code == 204)
                return NoContent();
            else if (error.error_code == 400)
                return BadRequest(error);
            else
                return Ok(productresult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            //Logic with the injected _context object.
            var result = await _productService.GetProductById(id);

            var productresult = result.Item1;
            var error = result.Item2;

            if (error.error_code == 204)
                return NoContent();
            else if (error.error_code == 400)
                return BadRequest(error);
            else
                return Ok(productresult);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductById(int id)
        {
            var result = _productService.DeleteProductById( id);
            var error = result.Item2;

            if (error.error_code == 404)
                return NotFound();
            else if (error.error_code == 400)
                return BadRequest(error);
            else
                return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productdto)
        {

            var dbProduct = await _productService.UpdateProduct( id,  productdto);

            var productresult = dbProduct.Item1;
            var error = dbProduct.Item2;

            if (error.error_code == 404)
                return NotFound();
            else if (error.error_code == 400)
                return BadRequest(error);
            else
                return Created("Put", productresult);
        }

        [HttpPut("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> StockDecrement(int id, int quantity)
        {
            var dbProduct = await _productService.StockDecrement(id, quantity);

            var productresult = dbProduct.Item1;
            var error = dbProduct.Item2;

            if (error.error_code == 404)
                return NotFound();
            else if (error.error_code == 400)
                return BadRequest(error);
            else
                return Ok(productresult);
        }

        [HttpPut("add-to-stock/{id}/{quantity}")]
        public async Task<IActionResult> StockIncrement(int id, int quantity)
        {
            var dbProduct = await _productService.StockIncrement( id,  quantity);

            var productresult = dbProduct.Item1;
            var error = dbProduct.Item2;

            if (error.error_code == 404)
                return NotFound();
            else if (error.error_code == 400)
                return BadRequest(error);
            else
                return Ok(productresult);
        }
    }
}
