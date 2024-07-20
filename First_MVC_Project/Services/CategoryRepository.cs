using First_MVC_Project.Context;
using First_MVC_Project.Models;
using First_MVC_Project.ReposInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace First_MVC_Project.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private MyContext context;
        public CategoryRepository(MyContext _context) 
        {
            context = _context;
        }
        public int Create(Category category)
        {
            context.Categories.Add(category);
            return context.SaveChanges();
        }

        public int Update(int id, Category _category)
        {
            Category category = context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                category.Name = _category.Name;
                category.DesplayOrder = _category.DesplayOrder;
            }
            return context.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return context.Categories.OrderBy(c=>c.DesplayOrder).ToList();
        }

        public Category GetById(int id)
        {
            Category category = context.Categories.FirstOrDefault(c => c.Id == id);
                return category;
        }

        public Category GetBydesOrder(int desOrder)
        {
            Category category = context.Categories.FirstOrDefault(c => c.DesplayOrder == desOrder);
                return category;
        }

        public Category GetByName(string name)
        {
            Category category = context.Categories.FirstOrDefault(c => c.Name == name);
                return category;
        }

        
        public int Delete(int id)
        {
            Category category = context.Categories.FirstOrDefault(c => c.Id == id);
            context.Categories.Remove(category);
            return context.SaveChanges();
        }

    }
}
