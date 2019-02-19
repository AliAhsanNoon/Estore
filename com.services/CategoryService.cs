using com.database;
using com.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace com.services
{
    public class CategoryService
    {
        public static CategoryService Instance{
            get
            {
                if (instance == null) instance = new CategoryService();
                return instance;
            }
        }

        private static CategoryService instance {get;set;}
        
        private CategoryService()
        {

        }

        public int GetTotalItem(int pageNo)
        {
            using (var _context = new CContext())
            {
                return _context.Categories.Count()
;
            }
        }

        public List<Category> GetListSize(int pageNo)
        {
            int pageSize = 3;
            using (var _context = new CContext())
            {
                return _context.Categories.OrderBy(x => x.ID)
                            .Skip((pageNo - 1) * pageSize)
                            .Take(pageSize).ToList();
            }
        }

        public List<Category> GetCategories()
        {
            using (var context = new CContext())
            {
                return context.Categories.ToList();
            }
        }

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new CContext())
            {
                return context.Categories
                    .Where(x => x.isFeatured == true && x.ImageURL != null)
                    .Take(3).ToList();
            }
        }

        public Category Edit(int ID)
        {
            using (var context = new CContext())
            {
                return context.Categories.ToList().SingleOrDefault(x => x.ID == ID);
            }
        }

        public void SaveCategory(Category category)
        {
            using (var context = new CContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void updateCategory(Category category)
        {
            using (var context = new CContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new CContext())
            {
                var catId = context.Categories.Single(x => x.ID == id);
                context.Categories.Remove(catId);
                context.SaveChanges();
            }
        }
    }
}
