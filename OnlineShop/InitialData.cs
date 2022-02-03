using System;

//namespace OnlineShop
//{
//    using System.Linq;
//    using Model;

//    public static class InitialData
//    {
//        public static void Seed(this OnlineShopDbContext OnlineShopDbContext)
//        {
//            if (!OnlineShopDbContext.Products.Any())
//            {
//                OnlineShopDbContext.Products.Add(new Product
//                {
//                    Price = 100,
//                    ProductId = 1,
//                    ProductName = "test",
//                    Quantity = 10

//                });
//                OnlineShopDbContext.Products.Add(new Product
//                {
//                    Price = 200,
//                    ProductId = 2,
//                    ProductName = "test2",
//                    Quantity = 20
//                });
//                OnlineShopDbContext.Products.Add(new Product
//                {
//                    Price = 300,
//                    ProductId = 3,
//                    ProductName = "test3",
//                    Quantity = 30
//                });

//                OnlineShopDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.DestuffedContainer ON");
//                OnlineShopDbContext.SaveChanges();
//                OnlineShopDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.DestuffedContainer OFF");
//            }

//            if (!OnlineShopDbContext.Orders.Any())
//            {
//                OnlineShopDbContext.Orders.Add(new Order
//                {
//                    OrderId = 1,
//                    ProductId = 1,
//                    Quantity = 5,
//                    DateTime = DateTime.Now

//                });
//                OnlineShopDbContext.Orders.Add(new Order
//                {
//                    OrderId = 2,
//                    ProductId = 2,
//                    Quantity = 5,
//                    DateTime = DateTime.Now
//                });
//                OnlineShopDbContext.Orders.Add(new Order
//                {
//                    OrderId = 3,
//                    ProductId = 3,
//                    Quantity = 5,
//                    DateTime = DateTime.Now
//                });

//                OnlineShopDbContext.SaveChanges();
//            }

//        }
//    }
//}
