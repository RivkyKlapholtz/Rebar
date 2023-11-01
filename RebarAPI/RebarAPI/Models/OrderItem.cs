using System;

namespace RebarAPI.Models
{
    public record OrderItem
    {
        public Shake Shake { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
    }
}
