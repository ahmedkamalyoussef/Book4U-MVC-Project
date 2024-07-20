using First_MVC_Project.Context;
using First_MVC_Project.Models;
using First_MVC_Project.ReposInterfaces;
using First_MVC_Project.VMController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sib_api_v3_sdk.Model;
using System.Security.Claims;

namespace First_MVC_Project.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository order;
        private readonly IProductRepository product;
        private MyContext myContext;
        public OrdersController(IOrderRepository _order, IProductRepository _product, MyContext myContext)
        {
            order = _order;
            product = _product;
            this.myContext = myContext;
        }
        [Authorize]
        public IActionResult Shopping()
        {
            List<OrdersAndProductVM> OPVM = new List<OrdersAndProductVM>();
            List<Orders> orders = order.GetAll(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            foreach (var order in orders)
            {
                OrdersAndProductVM _OPVM = new OrdersAndProductVM();
                _OPVM.OrderId = order.Id;
                _OPVM.Author = order.Product.Author;
                _OPVM.Title = order.Product.Title;
                _OPVM.CategoryName = order.Product.Category.Name;
                _OPVM.ProductUrl = order.Product.ProductUrl;
                _OPVM.Author = order.Product.Author;
                _OPVM.Productprice = order.Product.price;
                _OPVM.ProductsCount = order.ProductsCount;
                OPVM.Add(_OPVM);
            }
            return View("Shopping", OPVM);
        }

        public IActionResult Delete(int id)
        {
            
            if(order.Delete(id)>0)
                TempData["success"] = "Order has been deleted successfully";
            else TempData["fail"] = "failed to delete order";

            return RedirectToAction("Shopping");
        }
    }

}
