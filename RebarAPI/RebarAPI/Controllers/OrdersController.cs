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

        // ... other methods ...

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

        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order data is required.");
            }

            if (order.Items == null || order.Items.Count == 0)
                return BadRequest("Order must have at least one item.");

            order.Id = Guid.NewGuid();
            order.OrderDate = DateTime.Now;
            order.TotalPrice = CalculateTotalPrice(order);

            var createdOrder = _mongoService.AddOrder(order);
            if (createdOrder == null)
            {
                return BadRequest("Unable to save order.");
            }

            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
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
