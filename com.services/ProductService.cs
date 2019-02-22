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

        public List<Product> SearchProducts(string searchTerm, int? minPrice, int? maxPrice, int? CategoryId, int? sortBy)
        {
            using (var _context = new CContext())
            {
                var _products = _context.Products.Include(x=>x.Category).ToList();

                if (CategoryId.HasValue)
                {
                    _products = _products.Where(x => x.Category.ID == CategoryId.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    _products = _products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minPrice.HasValue)
                {
                    _products = _products.Where(x => x.Price >= minPrice.Value).ToList();
                }
                if (maxPrice.HasValue)
                {
                    _products = _products.Where(x => x.Price <= maxPrice.Value).ToList();
                }
                
                if (sortBy.HasValue)
                {
                    var sort = (SortByEnum)sortBy.Value;
                    switch (sort)
                    {
                        case SortByEnum.Popularity:
                            break;
                        case SortByEnum.PriceLowToHigh:
                            _products = _products.OrderBy( x => x.Price ).ToList();
                            break;
                        case SortByEnum.PriceHighToLow:
                            _products = _products.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            _products = _products.OrderByDescending(x => x.ID).ToList();
                            break;
                    }
                }
                return _products;
            }
        }

        public int GetMaxPrice()
        {
            using (var _context = new CContext())
            {
                return (int)(_context.Products.Max(x => x.Price));
            }
        }

        public List<Product> GetProducts()
        {
            using (var context = new CContext())
            {
                return context.Products.Include(x => x.Category).ToList();
            }
        }

        public List<Product> NewProduct()
        {
            using (var context = new CContext())
            {
                return context.Products
                       .OrderByDescending(x => x.ID)
                       .Where(x => x.ImageURL != null)
                       .Include(x => x.Category)
                       .Take(4)
                       .ToList();
            }
        }

        public Product GetProduct(int id)
        {
            using (var context = new CContext())
            {
                return context.Products.Include(x => x.Category).SingleOrDefault(x => x.ID == id);
            }
        }

        public List<Product> GetProducts(int pageNo, int productList)
        {
            int pageSize = 3; 
            using (var _context = new CContext())
            {
                return _context.Products.OrderBy(x => x.ID)
                            .Skip((pageNo - 1) * pageSize)
                            .Include(x => x.Category)
                            .Take(productList).ToList();
            }
        }

        public List<Product> RelatedProducts(int categoryID)
        {
            using (var _context = new CContext())
            {
                return _context.Products
                    .Where( x => x.Category.ID == categoryID )
                    .OrderByDescending(x => x.ID).Take(4)
                    .Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetProducts (List<int> pId)
        {
            using (var context = new CContext())
            {
                return context.Products.Where(x => pId.Contains(x.ID)).ToList();
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
        }
    }
}
