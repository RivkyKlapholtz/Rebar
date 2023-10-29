using System;
using System.Collections.Generic;

public class OrderProcessor
{
    private MenuManager menuManager; // You'll need a reference to the MenuManager for menu access
    private List<Invitation> orderHistory; // This is where you can store order details for MongoDB

    public OrderProcessor(MenuManager menuManager)
    {
        this.menuManager = menuManager;
        orderHistory = new List<Invitation>();
    }

    public void ProcessOrder()
    {
        // Step 1: Create a new order (Invitation)
        Console.WriteLine("Welcome to Rebar Shake Chain!");
        Console.Write("Enter your name: ");
        string customerName = Console.ReadLine();

        // Step 2: Display the menu and allow the customer to select shakes
        List<Shake> menu = menuManager.GetMenu();
        Console.WriteLine("Menu:");
        for (int i = 0; i < menu.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {menu[i].Name} - {menu[i].Description}");
        }

        List<Shake> selectedShakes = new List<Shake>();
        int maxShakesPerOrder = 10;
        int selectedShakeCount = 0;

        while (selectedShakeCount < maxShakesPerOrder)
        {
            Console.Write($"Select a shake (1-{menu.Count}) or 0 to finish: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                continue;
            }

            if (choice == 0)
                break;

            if (choice < 1 || choice > menu.Count)
            {
                Console.WriteLine("Invalid choice. Please select a shake from the menu.");
                continue;
            }

            Shake selectedShake = menu[choice - 1];

            // Step 3: Prompt for the shake size
            Console.Write("Select size (S/M/L): ");
            if (!Enum.TryParse<Shake.ShakeSize>(Console.ReadLine(), out Shake.ShakeSize selectedSize))
            {
                Console.WriteLine("Invalid size. Please select S, M, or L.");
                continue;
            }

            selectedShake.SelectedSize = selectedSize;
            selectedShakes.Add(selectedShake);
            selectedShakeCount++;
        }

        if (selectedShakes.Count == 0)
        {
            Console.WriteLine("No shakes selected. Order canceled.");
            return;
        }

        // Step 4: Calculate the total price of the order
        Invitation order = new Invitation(customerName, "");

        foreach (Shake selectedShake in selectedShakes)
        {
            order.Shakes.Add(selectedShake);
        }


        // Step 5: Generate a unique order ID
        orderHistory.Add(order);

        // Step 6: Record the order details (In this example, we simply added it to orderHistory list)

        // Step 7: Display a success message
        Console.WriteLine($"Order placed successfully!\nOrder ID: {order.OrderID}");
    }

    public List<Invitation> GetOrderHistory()
    {
        return orderHistory;
    }
}
