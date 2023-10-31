using System;

namespace RebarAPI.Models
{
    public record Shake
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PriceL { get; set; }
        public decimal PriceM { get; set; }
        public decimal PriceS { get; set; }
    }
}
