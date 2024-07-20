using First_MVC_Project.Models;

namespace First_MVC_Project.ReposInterfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllToProd();
        List<Product> GetAll();
        Product GetById(int id);
        Product GetByTitle(string title);
        Task<int> CreateAsync(Product product);
        int Update(int id, Product product);
        List<Product> SearchTitle(string title);
        int Delete(int id);
        List<Product> GetForLanding();
    }
}
