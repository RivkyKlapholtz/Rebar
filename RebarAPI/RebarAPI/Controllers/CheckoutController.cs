using Microsoft.AspNetCore.Mvc;
using RebarAPI.Data;
using RebarAPI.Models;
using System.Collections.Generic;


[Route("api/[controller]")]
[ApiController]
public class CheckoutController : ControllerBase
{
    private readonly MongoService _mongoService;
    private readonly string managerPassword = "SomeSecurePassword"; // This should be stored securely, not hardcoded!

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

        // You can further save this summary into the database for monthly/annual reporting.

        return Ok($"Total orders today: {totalOrders}. Total revenue today: ${totalRevenue}");
    }

    // GET: api/Checkout/Report
    [HttpGet("Report")]
    public ActionResult<IEnumerable<Order>> GetDailyReport()
    {
        // Ensure that only authorized personnel can access the daily report
        // This is a simplistic implementation; a more secure method should be used in a real application.

        var ordersToday = _mongoService.GetOrdersByDate(DateTime.Today);
        return Ok(ordersToday);
    }
}
