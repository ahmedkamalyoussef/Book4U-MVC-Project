using First_MVC_Project.Models;

namespace First_MVC_Project.ReposInterfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id);
        public Category GetBydesOrder(int desOrder);
        Category GetByName(string name);
        int Create(Category category);
        int Update(int id, Category category);
        int Delete(int id);
    }
}
