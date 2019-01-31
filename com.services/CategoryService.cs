using com.database;
using com.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.services
{
    public class CategoryService
    {
        CContext context = new CContext();

        public List<Category> GetProducts()
        {
            return context.Categories.ToList();
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
    }
}
