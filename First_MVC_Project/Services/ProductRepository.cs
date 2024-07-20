using First_MVC_Project.Context;
using First_MVC_Project.Models;
using First_MVC_Project.ReposInterfaces;
using First_MVC_Project.utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace First_MVC_Project.Services
{
    public class ProductRepository : IProductRepository
    {
        private MyContext context;
        UserManager<AppUser> userManager;
        IHttpContextAccessor httpContextAccessor;
        public ProductRepository
            (MyContext _context, UserManager<AppUser> _userManager, IHttpContextAccessor _httpContextAccessor) 
        {
            context = _context;
            userManager = _userManager;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task<int> CreateAsync(Product _product)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userManager.FindByIdAsync(userId);
            var category = context.Categories.FirstOrDefault(c =>c.Id== _product.CategoryId);
            var product = new Product();
            product.price = _product.price;
            product.price100 = _product.price100;
            product.price50 = _product.price50;
            product.ISBN = _product.ISBN;
            product.Author  = _product.Author;
            product.Description = _product.Description;
            product.CategoryId = _product.CategoryId;
            product.PublisherId = userId;
            product.Title = _product.Title;
            product.Id=_product.Id;
            product.Coantity = _product.Coantity;
            product.ProductUrl= _product.ProductUrl;
            product.Category= category;
            product.AppUser = user;
            context.Products.Add(product);
            return context.SaveChanges();
        }

        public int Update(int id, Product _product)
        {
            Product product = context.Products.FirstOrDefault(c => c.Id == id);

            if (product != null)
            {
                product.Title = _product.Title;
                product.Description = _product.Description;
                product.ISBN = _product.ISBN;
                product.Author = _product.Author;
                product.price = _product.price;
                product.price50 = _product.price50;
                product.price100 = _product.price100;
                product.CategoryId=_product.CategoryId;
                product.Coantity = _product.Coantity;
                product.ProductUrl = _product.ProductUrl;
            }
            return context.SaveChanges();
        }

        public async Task<List<Product>> GetAllToProd()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userManager.FindByIdAsync(userId);

            if ((await userManager.GetRolesAsync(user)).ToString() == Roles.RoleAdmin)
                return context.Products.Include(p => p.Category).Include(p => p.Category).ToList();
            else return context.Products.Where(p => p.PublisherId == userId).Include(p => p.Category).Include(p => p.Category).ToList();
        }

        public  List<Product> GetAll()
        {
         return context.Products.Include(p => p.Category).Include(p => p.Category).ToList();
        }

        public Product GetById(int id)
        {
            Product product = context.Products.Include(p => p.Category).FirstOrDefault(c => c.Id == id);
                return product;
        }

        public Product GetByTitle(string title)
        {
            Product product = context.Products.FirstOrDefault(c => c.Title == title);
            return product;
        }

        public List<Product> SearchTitle(string title)
        {
            var products = context.Products
                            .Where(p => p.Title.Contains(title))
                            .ToList();
            return products;
        }


        public int Delete(int id)
        {
            Product product = context.Products.FirstOrDefault(c => c.Id == id);
            context.Products.Remove(product);
            return context.SaveChanges();
        }

        public List<Product> GetForLanding()
        {
            return context.Products.OrderBy(p=>Guid.NewGuid()).Take(7).ToList();
        }
    }
}
