using System;

public class Invoice
{
    public List<Invitation> Orders { get; } = new List<Invitation>();
    public double TotalAmount => Orders.Sum(order => order.CalculateTotalAmount());

    public Invoice()
    {
 
    }

    public void AddOrder(Invitation order)
    {
        Orders.Add(order);
    }
}


