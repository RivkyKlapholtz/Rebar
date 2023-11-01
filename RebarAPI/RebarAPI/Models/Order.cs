namespace RebarAPI.Models
{
    public record Order
    {
        public Guid Id { get; init; } = Guid.NewGuid(); // Set ID automatically
        public List<OrderItem> Items { get; set; }
        public decimal TotalPrice { get; private set; } // Make setter private
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; init; } = DateTime.Now; // Set date automatically
        public List<Discount> Discounts { get; set; }

        // Add a method to compute the TotalPrice
        public void CalculateTotalPrice()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Price;
            }
            foreach (var discount in Discounts)
            {
                total -= (total * discount.Percentage / 100);
            }
            TotalPrice = total;
        }
    }
}
