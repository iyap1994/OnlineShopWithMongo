using System;
using System.Net;
using OnlineShop.Model;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;


        public ProductController(IProductService productService)
        {
            _ProductService = productService;
        }

        [HttpGet]
        public IActionResult GetProduct()
        {
            var products = _ProductService.GetProduct();

            if (products != null) return Ok(products);

            return NotFound();
        }


        [HttpGet("{productId}", Name = "GetProduct")]
        public IActionResult GetProduct(int productId)
        {
            var product = _ProductService.GetProduct(productId);

            if (product != null) return Ok(product);

            return NotFound();
        }


        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            try
            {
                _ProductService.AddProduct(product);
                return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            try
            {
                _ProductService.UpdateProduct(product.ProductId, product);
                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [HttpDelete]
        public IActionResult RemoveProduct(int productId)
        {
            try
            {
                _ProductService.RemoveProduct(productId);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
