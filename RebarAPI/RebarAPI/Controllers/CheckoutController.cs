using Microsoft.AspNetCore.Mvc;
using RebarAPI.Data;
using RebarAPI.Models;
using System.Collections.Generic;


[Route("api/[controller]")]
[ApiController]
public class CheckoutController : ControllerBase
{
    private readonly MongoService _mongoService;
    private readonly string managerPassword = "SomeSecurePassword"; 

    public CheckoutController(MongoService mongoService)
    {
        _mongoService = mongoService;
    }

    // POST: api/Checkout/Close
    [HttpPost("Close")]
    public ActionResult<string> CloseCheckout([FromBody] string enteredPassword)
    {
        if (enteredPassword != managerPassword)
        {
            return Unauthorized("Incorrect password.");
        }

        var ordersToday = _mongoService.GetOrdersByDate(DateTime.Today);

        var totalOrders = ordersToday.Count;
        var totalRevenue = ordersToday.Sum(order => order.TotalPrice);

        // reminder! Save the information in the database

        return Ok($"Total orders today: {totalOrders}. Total revenue today: ${totalRevenue}");
    }

    // GET: api/Checkout/Report
    [HttpGet("Report")]
    public ActionResult<IEnumerable<Order>> GetDailyReport()
    {
        var ordersToday = _mongoService.GetOrdersByDate(DateTime.Today);
        return Ok(ordersToday);
    }
}
