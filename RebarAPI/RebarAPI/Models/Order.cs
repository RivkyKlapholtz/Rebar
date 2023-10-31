using System;
using System.Collections.Generic;

namespace RebarAPI.Models
{
    public record Order
    {
        public Guid Id { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Discount> Discounts { get; set; }
    }
}
