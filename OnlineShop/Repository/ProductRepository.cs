using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnlineShop.Model;

namespace OnlineShop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _Products;

        public ProductRepository(IOnlineShopDbSetting onlineShopDbSettings)
        {
            var client = new MongoClient(onlineShopDbSettings.ConnectionString);
            var db = client.GetDatabase(onlineShopDbSettings.DatabaseName);
            _Products = db.GetCollection<Product>(onlineShopDbSettings.ProductsCollectionName);
        }

        public void AddProduct(Product product)
        {
            _Products.InsertOne(product);
        }

        public IEnumerable<Product> GetProduct()
        {
            return _Products.Find(x => true).ToList();
        }

        public Product GetProduct(int productId)
        {
            return _Products.Find(x => x.ProductId == productId).FirstOrDefault();
        }

        public void UpdateProduct(int productId, Product product)
        {
            _Products.ReplaceOne(item => item.ProductId == productId, product);
        }

        public void RemoveProduct(Product product)
        {
            _Products.DeleteOne(x => x.ProductId == product.ProductId);
        }

    }

    public interface IProductRepository
    {
        void AddProduct(Product product);
        IEnumerable<Product> GetProduct();
        Product GetProduct(int id);
        void UpdateProduct(int id, Product product);
        void RemoveProduct(Product product);
    }
}
