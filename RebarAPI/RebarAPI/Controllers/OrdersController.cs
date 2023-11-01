using Microsoft.AspNetCore.Mvc;
using RebarAPI.Data;
using RebarAPI.Models;
using System;

namespace RebarAPI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public OrdersController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(Guid id)
        {
            var order = _mongoService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        // GET: api/orders
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _mongoService.GetOrders();
            return Ok(orders);
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order order)
        {
            if (order == null || string.IsNullOrWhiteSpace(order.CustomerName) || order.Items == null || order.Items.Count == 0)
            {
                return BadRequest("Order data or customer name is missing or incomplete.");
            }

            try
            {
                order.CalculateTotalPrice(); 
                var createdOrder = _mongoService.CreateOrder(order);
                return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Unable to save order.");
            }
        }


        private decimal CalculateTotalPrice(Order order)
        {
            decimal total = 0;
            foreach (var item in order.Items)
            {
                total += item.Price;
            }
            foreach (var discount in order.Discounts)
            {
                total -= (total * discount.Percentage / 100);
            }
            return total;
        }
    }
}
