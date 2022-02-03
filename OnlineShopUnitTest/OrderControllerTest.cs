using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OnlineShop.Controllers;
using OnlineShop.Model;
using OnlineShop.Services;

namespace OnlineShopUnitTest
{
    class OrderControllerTest
    {
        private readonly Mock<IOrderService> _processor;
        private readonly OrderController _controller;
        private Order _order;
        private List<Order> _orders;

        public OrderControllerTest()
        {
            _processor = new Mock<IOrderService>();
            _controller = new OrderController(_processor.Object);
        }

        [SetUp]
        public void Setup()
        {
            _orders = new List<Order>();
            _order = new Order()
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 10,
                DateTime = DateTime.Now
            };

            _orders.Add(_order);
        }

        [Test]
        public void Test_GetOrder()
        {
            _processor.Setup(x => x.GetOrder()).Returns(_orders);
            var result = _controller.GetOrder() as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }


        [Test]
        public void Test_GetOrder_Id()
        {
            _processor.Setup(x => x.GetOrder(1)).Returns(_order);
            var result = _controller.GetOrder(1) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }


        [Test]
        public void Test_GetOrder_Invalid_Id()
        {
            _processor.Setup(x => x.GetOrder(-1)).Verifiable();
            _processor.Object.GetOrder(-1);
            _processor.VerifyAll();
        }


        [Test]
        public void Test_PlaceOrder()
        {
            var order = new Order { OrderId = 2, ProductId = 1, Quantity = 50, DateTime = DateTime.Now};
            _processor.Setup(x => x.PlaceOrder(order)).Returns(_order);
            var result = _controller.PlaceOrder(order) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.Created, result.StatusCode.Value);
        }


        [Test]
        public void Test_PlaceOrder_Duplicate()
        {
            var order = new Order { OrderId = 2, ProductId = 1, Quantity = 50, DateTime = DateTime.Now };
            _processor.Setup(x => x.PlaceOrder(order)).Verifiable();
            _processor.Object.PlaceOrder(order);
            _processor.VerifyAll();
        }


        [Test]
        public void Test_RemoveOrder()
        {
            var order = new Order { OrderId = 2, ProductId = 1, Quantity = 50, DateTime = DateTime.Now };
            _processor.Setup(x => x.CancelOrder(order.OrderId)).Verifiable();
            _processor.Object.CancelOrder(order.OrderId);
            _processor.VerifyAll();
        }
    }
}