using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

public class OrderProcessor
{
    private MenuManager menuManager;
    private IMongoCollection<Invitation> orderCollection; // MongoDB collection for orders
    private IMongoDatabase database;

    public OrderProcessor(MenuManager menuManager, IMongoDatabase database) // Pass the database connection
    {
        this.menuManager = menuManager;
        this.database = database; // Store the database connection

        // Initialize MongoDB client and order collection
        var client = new MongoClient("mongodb+srv://4900800:<password>@cluster0.bsff8tw.mongodb.net/<YourDatabaseName>\r\n");
        orderCollection = database.GetCollection<Invitation>("Orders");
    }


    public void ProcessOrder()
    {
        Console.WriteLine("Welcome to Rebar Shake Chain!");
        Console.Write("Enter your name: ");
        string customerName = Console.ReadLine();

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


        Invitation order = new Invitation(customerName, "");

        foreach (Shake selectedShake in selectedShakes)
        {
            order.Shakes.Add(selectedShake);
        }

        // Calculate the total price of the order
        double totalAmount = order.CalculateTotalAmount();

        // Step 6: Record the order details in the MongoDB database
        orderCollection.InsertOne(order);

        // Step 7: Display a success message
        Console.WriteLine($"Order placed successfully!\nOrder ID: {order.OrderID}");
    }
    public void CloseCheckout()
    {
        // Get the current date for filtering orders
        DateTime today = DateTime.Now.Date;

        // Get orders placed today from the MongoDB database
        var filter = Builders<Invitation>.Filter.Gte(order => order.OrderDate, today);
        var todayOrders = orderCollection.Find(filter).ToList();

        // Calculate the number of orders and total revenue for the day
        int numberOfOrders = todayOrders.Count;
        double totalRevenue = todayOrders.Sum(order => order.CalculateTotalAmount());

        // Print the statistics
        Console.WriteLine($"Today's Statistics:");
        Console.WriteLine($"Number of Orders: {numberOfOrders}");
        Console.WriteLine($"Total Revenue: ${totalRevenue}");

        // Save the statistics to the database (you can create a new collection for daily reports)
        var dailyReportCollection = database.GetCollection<DailyReport>("DailyReports");
        var dailyReport = new DailyReport
        {
            Date = today,
            NumberOfOrders = numberOfOrders,
            TotalRevenue = totalRevenue
        };
        dailyReportCollection.InsertOne(dailyReport);
    }

}
