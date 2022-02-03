using System;
using System.Collections.Generic;
using OnlineShop.Model;
using OnlineShop.Repository;

namespace OnlineShop.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetProduct()
        {
            return _productRepository.GetProduct();
        }

        public Product GetProduct(int productId)
        {
            return _productRepository.GetProduct(productId);
        }

        public Product AddProduct(Product product)
        {
            var p = GetProduct(product.ProductId);

            if (p != null) throw new Exception("Product can't be duplicate.");

            _productRepository.AddProduct(product);
            return product;
        }

        public void UpdateProduct(int productId, Product product)
        {
            try
            {
                _productRepository.UpdateProduct(productId, product);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RemoveProduct(Product product)
        {
            try
            {
                _productRepository.RemoveProduct(product);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RemoveProduct(int productId)
        {
            try
            {
                var product = GetProduct(productId);
                _productRepository.RemoveProduct(product);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int GetQuantity(int productId)
        {
            try
            {
                return GetProduct(productId).Quantity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            try
            {
                var product = GetProduct(productId);
                product.Quantity = quantity;
                UpdateProduct(productId, product);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public interface IProductService
    {
        IEnumerable<Product> GetProduct();
        Product GetProduct(int id);
        Product AddProduct(Product product);
        void UpdateProduct(int id, Product product);
        void RemoveProduct(Product product);
        void RemoveProduct(int id);
        int GetQuantity(int productId);
        void UpdateQuantity(int productId, int quantity);
    }
}
