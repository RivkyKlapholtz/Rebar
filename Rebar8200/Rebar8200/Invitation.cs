using System;
using System.Collections.Generic;
using static Shake;

public class Invitation
{
    public List<Shake> Shakes { get; } = new List<Shake>();
    public Guid OrderID { get; } = Guid.NewGuid();
    public string CustomerName { get; }
    public DateTime OrderDate { get; } = DateTime.Now;
    public string DiscountsAndPromotions { get; }

    public Invitation(string customerName, string discountsAndPromotions)
    {
        CustomerName = customerName;
        DiscountsAndPromotions = discountsAndPromotions;
    }

    public double CalculateTotalAmount()
    {
        // Calculate the total amount for this order based on the chosen sizes and shake prices
        double totalAmount = 0;
        foreach (Shake shake in Shakes)
        {
            // Calculate the total price based on the size chosen (L, M, or S)
            // Adjust the following logic to match your actual size selection method
            switch (shake.SelectedSize)
            {
                case ShakeSize.Large:
                    totalAmount += shake.PriceLarge;
                    break;
                case ShakeSize.Medium:
                    totalAmount += shake.PriceMedium;
                    break;
                case ShakeSize.Small:
                    totalAmount += shake.PriceSmall;
                    break;
            }
        }
        return totalAmount;
    }
}
