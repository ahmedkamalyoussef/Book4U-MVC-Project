using First_MVC_Project.Context;
using First_MVC_Project.Models;
using First_MVC_Project.ReposInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Security.Claims;

namespace First_MVC_Project.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController(ILogger<HomeController> logger, IProductRepository _product, MyContext myContext, IOrderRepository _order, ICategoryRepository _category) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IProductRepository product = _product;
        private readonly ICategoryRepository category = _category;
        private readonly IOrderRepository order = _order;
        private readonly MyContext myContext = myContext;

        public IActionResult Index(string? productSearsh = null)
        {
            IEnumerable<Product> products;
            if (productSearsh == null)
            {
                products = product.GetAll();
            }
            else
            {
                products = product.SearchTitle(productSearsh);
            }
            ViewData["categories"] = category.GetAll();
            ViewBag.landingProd = product.GetForLanding();
            return View(products);
        }
        public IActionResult Details(int id)
        {
            Product prod = product.GetById(id);
            return View(prod);
        }
        [Authorize]
        public IActionResult AddToCard(int id, int count)
        {
            var prod = product.GetById(id);
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUser = myContext.Users.FirstOrDefault(u => u.Id == userId);
            if (currentUser != null)
            {
                if (!(count == 0))
                {
                    if (prod != null)
                    {

                        var currentOrder = order.GetByPIdUId(id, userId);
                        if (currentOrder != null)
                        {
                            currentOrder.ProductsCount += count;
                            order.Update(currentOrder.Id, currentOrder);
                        }
                        else
                        {
                            Orders _order = new Orders();
                            _order.state = false;
                            _order.ProductsCount = count;
                            _order.Product = prod;
                            _order.User = currentUser;
                            order.Create(_order);
                        }

                        TempData["success"] = "Order has been added successfully";
                        return RedirectToAction("Details", prod);
                    }
                }
                TempData["fail"] = "Please Add Your Order Agian";
                return RedirectToAction("Details", prod);
            }
            TempData["fail"] = "fail to add Order";
            return RedirectToAction("Details", prod);
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
