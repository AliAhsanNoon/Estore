using com.database;
using com.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.services
{
    public class ProductService
    {
        CContext context = new CContext();

        public List<Product> GetProducts()
        {
            return context.Products.ToList();
        }
    }
}
