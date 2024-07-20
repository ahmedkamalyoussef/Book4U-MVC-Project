using First_MVC_Project.Context;
using First_MVC_Project.Models;
using First_MVC_Project.ReposInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_MVC_Project.Services
{
    public class OrderRepository : IOrderRepository
    {
        private MyContext context;
        public OrderRepository(MyContext _context) 
        {
            context = _context;
        }

        public int Create(Orders order)
        {
            context.Orders.Add(order);
            return context.SaveChanges();
        }

        public int Delete(int id)
        {
            var order = context.Orders.FirstOrDefault(o => o.Id == id);
            context.Orders.Remove(order);
            return context.SaveChanges();
        }

        public List<Orders> GetAll(string id)
        {
            return context.Orders.Where(o=> o.UserId == id).Include(o=>o.Product).ThenInclude(p=>p.Category).ToList();
        }

        public Orders GetByPIdUId(int Pid, string Uid)
        {
            return context.Orders.FirstOrDefault(o=>o.ProdId == Pid&&o.UserId==Uid);
        }

        public int Update(int id, Orders _order)
        {
            var order = context.Orders.FirstOrDefault(c => c.Id == id);
            if (order != null)
            {
                order.state=_order.state;
                order.UserId=_order.UserId;
                order.ProdId=_order.ProdId;
                order.ProductsCount=_order.ProductsCount;
            }
            return context.SaveChanges();
        }
    }
}
