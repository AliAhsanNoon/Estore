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
    public class CategoryService
    {
        CContext context = new CContext();

        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public List<Category> GetFeaturedCategories()
        {
            return context.Categories.Where(x => x.isFeatured == true && x.ImageURL != null).Take(3).ToList();
        }

        public Category Edit(int ID)
        {
            using (context)
            {
                return context.Categories.ToList().SingleOrDefault(x => x.ID == ID);
            }
        }

        public void SaveCategory(Category category)
        {
            using (context)
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void updateCategory(Category category)
        {
            using (context)
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var catId = context.Categories.Single(x => x.ID == id);
            context.Categories.Remove(catId);
            context.SaveChanges();
        }
    }
}
