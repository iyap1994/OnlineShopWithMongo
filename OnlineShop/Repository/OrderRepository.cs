using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnlineShop.Model;

namespace OnlineShop.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _Orders;        

        public OrderRepository(IServiceProvider provider, IOnlineShopDbSetting onlineShopDbSetting)
        {
            var client = new MongoClient(onlineShopDbSetting.ConnectionString);
            var db = client.GetDatabase(onlineShopDbSetting.DatabaseName);
            _Orders = db.GetCollection<Order>(onlineShopDbSetting.OrdersCollectionName);
        }

        public Order PlaceOrder(Order order)
        {
            _Orders.InsertOne(order);
            return order;
        }

        public IEnumerable<Order> GetOrder()
        {
            return _Orders.Find(x => true).ToList();
        }

        public Order GetOrder(int id)
        {
            return _Orders.Find(x => x.OrderId == id).FirstOrDefault();
        }

        public void UpdateOrder(int orderId, Order order)
        {
            _Orders.ReplaceOne(x => x.OrderId == orderId, order);
        }

        public void CancelOrder(Order order)
        {
            _Orders.DeleteOne(x => x.OrderId == order.OrderId);
        }
    }


    public interface IOrderRepository
    {
        Order PlaceOrder(Order product);
        IEnumerable<Order> GetOrder();
        Order GetOrder(int id);
        void UpdateOrder(int id, Order product);
        void CancelOrder(Order product);
    }
}
