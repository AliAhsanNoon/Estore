using com.database;
using com.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace com.services
{
    public class ProductService
    {
        public static ProductService Instance
        {
            get
            {
                if (instance == null) instance = new ProductService();
                return instance;
            }
        }

        private static ProductService instance { get; set; }

        private ProductService() { }

        public List<Product> GetProducts()
        {
            using (var context = new CContext())
            {
                return context.Products.Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetProducts(int pageNo, int productList)
        {
            int pageSize = 3; 
            using (var _context = new CContext())
            {
                //return context.Products.Include(x => x.Category).ToList();
                return _context.Products.OrderBy(x => x.ID)
                            .Skip((pageNo - 1) * pageSize)
                            .Include(x => x.Category)
                            .Take(productList).ToList();
            }
        }

        public List<Product> GetProducts (List<int> pId)
        {
            using (var context = new CContext())
            {
                return context.Products.Where(x => pId.Contains(x.ID)).ToList();
            }
        }

        public List<Product> GetFeaturedProducts()
        {
            using (var context = new CContext())
            {
                return context.Products.Where(x => x.isFeatured == true && x.ImageURL != null).Take(3).ToList();

            }
        }

        public List<Product> NewProduct()
        {
            using (var context = new CContext())
            {
                return context.Products
                       .OrderByDescending(x=> x.ID )
                       .Where(x=> x.ImageURL != null)
                       .Include(x=> x.Category)
                       .Take(4)
                       .ToList();
            }
        }

        public List<Product> NewProductx()
        {
            using (var context = new CContext())
            {

            return context.Products.Where(x => x.ImageURL != null).Take(8).ToList();
            }
        }

        public Product GetProduct(int id)
        {
            using (var context = new CContext())
            {

            return context.Products.Include(x=> x.Category).SingleOrDefault(x => x.ID == id);
            }
        }

        public void SaveProducts(Product product)
        {
            using (var context = new CContext())
            {
            context.Entry(product).State = EntityState.Unchanged;
            context.Products.Add(product);
            context.SaveChanges();
            }
        }

        public void UpdateProducts(Product product)
        {
            using (var context = new CContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProducts(int ID)
        {
            using (var context = new CContext())
            {
                var product = context.Products.Where(p => p.ID.Equals(ID)).SingleOrDefault();
                context.Products.Remove(product);
                context.SaveChanges();
            }
                // var pID = context.Products.Single(p => p.ID == ID);
        }
    }
}
