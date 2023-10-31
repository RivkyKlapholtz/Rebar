using System.Collections.Generic;

namespace RebarAPI.Models
{
    public record Invoice
    {
        public List<Order> Orders { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
