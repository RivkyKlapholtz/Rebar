using System;

public class Shake
{
    public string Name { get; }
    public string Description { get;  }
    public double PriceLarge { get; }
    public double PriceMedium { get; }
    public double PriceSmall { get; }
    public int ID { get; }
    public ShakeSize SelectedSize { get; set; }

    private static int shakeCounter = 1; // Static counter for generating unique IDs

    public enum ShakeSize
    {
        Small,
        Medium,
        Large,
        NotSelectedYet
    }


    public Shake(string name, string description, double priceLarge, double priceMedium, double priceSmall, ShakeSize selectedSize)
    {
        Name = name;
        Description = description;
        PriceLarge = priceLarge;
        PriceMedium = priceMedium;
        PriceSmall = priceSmall;
        ID = shakeCounter++; // Assign a unique ID and increment the counter
        SelectedSize = ShakeSize.NotSelectedYet;
    }
    public void SetSelectedSize(ShakeSize selectedSize)
    {
        SelectedSize = selectedSize;
    }
   
}

