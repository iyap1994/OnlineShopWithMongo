using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Model;
using OnlineShop.Repository;

namespace OnlineShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IServiceProvider _Provider;

        public OrderService(IServiceProvider provider, IOrderRepository orderRepository)
        {
            _Provider = provider;
            _OrderRepository = orderRepository;
        }

        public IEnumerable<Order> GetOrder()
        {
            return _OrderRepository.GetOrder();
        }

        public Order GetOrder(int id)
        {
            return _OrderRepository.GetOrder(id);
        }

        public Order PlaceOrder(Order order)
        {
            int availableQty = _Provider.GetRequiredService<IProductService>().GetQuantity(order.ProductId);

            if (availableQty < order.Quantity) throw new Exception($"Only {availableQty} stock available");

            _OrderRepository.PlaceOrder(order);

            int remainingQty = availableQty - order.Quantity;

            _Provider.GetRequiredService<IProductService>().UpdateQuantity(order.ProductId, remainingQty);

            return order;
        }

        public void CancelOrder(Order order)
        {
            try
            {
                _OrderRepository.CancelOrder(order);
                int availableQty = _Provider.GetRequiredService<IProductService>().GetQuantity(order.ProductId);
                _Provider.GetRequiredService<IProductService>().UpdateQuantity(order.ProductId, availableQty + order.Quantity);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CancelOrder(int orderId)
        {
            try
            {
                var order = GetOrder(orderId);
                CancelOrder(order);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public interface IOrderService
    {
        IEnumerable<Order> GetOrder();
        Order GetOrder(int id);
        Order PlaceOrder(Order order);
        void CancelOrder(Order order);
        void CancelOrder(int orderId);
    }
}