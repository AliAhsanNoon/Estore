using com.database;
using com.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.services
{
    public class ProductService
    {
        CContext context = new CContext();

        public List<Product> GetProducts()
        {   return context.Products.Include(x=> x.Category).ToList();   }

        public Product GetProduct(int id)
        {
            return context.Products.Include(x=> x.Category).SingleOrDefault(x => x.ID == id);
        }

        public void SaveProducts(Product product)
        {
            using (context)
            {
                context.Entry(product).State = EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void UpdateProducts(Product product)
        {
            using (context)
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProducts(int ID)
        {
           // var pID = context.Products.Single(p => p.ID == ID);
            var product = context.Products.Where(p => p.ID.Equals(ID)).SingleOrDefault();
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
