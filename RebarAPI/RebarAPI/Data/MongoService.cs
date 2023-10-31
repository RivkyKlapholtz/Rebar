using MongoDB.Driver;
using RebarAPI.Models;
using System;
using System.Collections.Generic;

namespace RebarAPI.Data
{
    public class MongoService
    {
        private readonly IMongoCollection<Order> _orders;
        private readonly IMongoCollection<Shake> _menu;

        public MongoService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("RebarDB");
            _orders = database.GetCollection<Order>("Orders");
            _menu = database.GetCollection<Shake>("Menu");
        }


        public List<Shake> GetMenu() => _menu.Find(shake => true).ToList();

        public Shake AddShakeToMenu(Shake shake)
        {
            _menu.InsertOne(shake);
            return shake;
        }

        public Order AddOrder(Order order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public Order GetOrderById(Guid id)
        {
            return _orders.Find(order => order.Id == id).FirstOrDefault();
        }

        public Shake GetShake(Guid id) => _menu.Find<Shake>(shake => shake.Id == id).FirstOrDefault();

        public Shake CreateShake(Shake shake)
        {
            _menu.InsertOne(shake);
            return shake;
        }

        public void UpdateShake(Guid id, Shake shakeIn) => _menu.ReplaceOne(shake => shake.Id == id, shakeIn);

        public void RemoveShake(Shake shakeIn) => _menu.DeleteOne(shake => shake.Id == shakeIn.Id);

        public void RemoveShake(Guid id) => _menu.DeleteOne(shake => shake.Id == id);

        // CRUD for Orders
        public List<Order> GetOrders() => _orders.Find(order => true).ToList();

        public Order GetOrder(Guid id) => _orders.Find<Order>(order => order.Id == id).FirstOrDefault();

        public Order CreateOrder(Order order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public void UpdateOrder(Guid id, Order orderIn) => _orders.ReplaceOne(order => order.Id == id, orderIn);

        public void RemoveOrder(Order orderIn) => _orders.DeleteOne(order => order.Id == orderIn.Id);

        public void RemoveOrder(Guid id) => _orders.DeleteOne(order => order.Id == id);
        public List<Order> GetTodaysOrders()
        {
            var startOfDay = DateTime.Today;
            var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            return _orders.Find(order => order.OrderDate >= startOfDay && order.OrderDate <= endOfDay).ToList();
        }


        public Shake GetShakeById(Guid id)
        {
            // Find a specific shake by its GUID.
            return _menu.Find<Shake>(shake => shake.Id == id).FirstOrDefault();
        }


        public void DeleteShake(Shake shakeIn)
        {
            // Delete a specific shake using the Shake object.
            _menu.DeleteOne(shake => shake.Id == shakeIn.Id);
        }

        public void DeleteShakeById(Guid id)
        {
            // Delete a specific shake by its GUID.
            _menu.DeleteOne(shake => shake.Id == id);
        }



        //... Other DB Operations like getting today's orders, etc.
    }
}
