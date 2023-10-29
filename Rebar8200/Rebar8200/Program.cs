using MongoDB.Driver;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Create the MongoDB client and database connection
        var client = new MongoClient("mongodb+srv://4900800:<password>@cluster0.bsff8tw.mongodb.net/<YourDatabaseName>");
        var database = client.GetDatabase("<YourDatabaseName>");

        // Initialize your MenuManager
        MenuManager menuManager = new MenuManager();

        // Create an instance of OrderProcessor with the database connection
        OrderProcessor orderProcessor = new OrderProcessor(menuManager, database);

        // Process an order
        orderProcessor.ProcessOrder();

        // Close the checkout and generate a daily report
        orderProcessor.CloseCheckout();
    }
}
