using System;
using System.Net;
using OnlineShop.Model;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetOrder()
        {
            try
            {
                var orders = _orderService.GetOrder();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult GetOrder(int orderId)
        {
            try
            {
                var order = _orderService.GetOrder(orderId);
                return Ok(order);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            try
            {
                _orderService.PlaceOrder(order);
                return CreatedAtRoute("GetOrder", new { id = order.OrderId }, order);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        
        [HttpDelete]
        public IActionResult CancelOrder(int id)
        {
            try
            {
                _orderService.CancelOrder(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
            
        }
    }
}

