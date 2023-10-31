namespace RebarAPI.Models
{
    public record Discount
    {
        public string Name { get; set; }
        public decimal Percentage { get; set; }
    }
}
