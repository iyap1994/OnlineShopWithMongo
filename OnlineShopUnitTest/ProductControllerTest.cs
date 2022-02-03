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
    class ProductControllerTest
    {
        private readonly Mock<IProductService> _processor;
        private readonly ProductController _controller;
        private Product _product;
        private List<Product> _products;

        public ProductControllerTest()
        {
            _processor = new Mock<IProductService>();
            _controller = new ProductController(_processor.Object);
        }

        [SetUp]
        public void Setup()
        {
            _products = new List<Product>();
            _product = new Product()
            {
                Id = "32D3FB618F8FACAED7254837",
                ProductId = 1,
                ProductName = "Test1",
                Quantity = 10,
                Price = 100
            };

            _products.Add(_product);
        }

        [Test]
        public void Test_GetProduct()
        {
            _processor.Setup(x => x.GetProduct()).Returns(_products);
            var result = _controller.GetProduct() as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }


        [Test]
        public void Test_GetProduct_Id()
        {
            _processor.Setup(x => x.GetProduct(1)).Returns(_product);
            var result = _controller.GetProduct(1) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }


        [Test]
        public void Test_GetProduct_Invalid_Id()
        {
            _processor.Setup(x => x.GetProduct(-1)).Verifiable();
            _processor.Object.GetProduct(-1);
            _processor.VerifyAll();
        }


        [Test]
        public void Test_AddProduct()
        {
            var product = new Product {ProductId = 5,ProductName = "Test5", Quantity = 50, Price = 500};
            _processor.Setup(x => x.AddProduct(product)).Returns(_product);
            var result = _controller.AddProduct(product) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.Created, result.StatusCode.Value);
        }


        [Test]
        public void Test_AddProduct_Duplicate()
        {
            var product = new Product { ProductId = 5, ProductName = "Test55", Quantity = 55, Price = 550 };
            _processor.Setup(x => x.AddProduct(product)).Verifiable();
            _processor.Object.AddProduct(product);
            _processor.VerifyAll();
        }


        [Test]
        public void Test_UpdateProduct()
        {
            var product = new Product { ProductId = 5, ProductName = "Test55", Quantity = 55, Price = 550 };
            _processor.Setup(x => x.UpdateProduct(5,product));
            var result = _controller.UpdateProduct(product) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }


        [Test]
        public void Test_UpdateProduct_Invalid()
        {
            var product = new Product { ProductId = -1, ProductName = "Test55", Quantity = 55, Price = 550 };
            _processor.Setup(x => x.UpdateProduct(-1, product)).Verifiable();
            _processor.Object.UpdateProduct(-1, product);
            _processor.VerifyAll();
        }


        [Test]
        public void Test_RemoveProduct()
        {
            var product = new Product { ProductId = 5, ProductName = "Test55", Quantity = 55, Price = 550 };
            _processor.Setup(x => x.RemoveProduct(product.ProductId)).Verifiable();
            _processor.Object.RemoveProduct(product.ProductId);
            _processor.VerifyAll();
        }
    }
}