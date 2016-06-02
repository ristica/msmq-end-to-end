using System;
using System.Collections.Generic;
using System.Linq;
using Msmq.OnlineShop.Entities;

namespace Msmq.OnlineShop.Db.Repository
{
    public class ProductRepository : IDisposable
    {
        private static readonly List<Product> Products = new List<Product>();

        public static List<Product> Get()
        {
            return Products;
        }

        public static Product GetProduct(int id)
        {
            var product = Products.SingleOrDefault(p => p.ProductId == id);
            if (product != null)
                return product;

            return new Product();
        }

        public static void Create(Product product)
        {
            Products.Add(product);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                // free up the resources
                // ...
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
