using First_MVC_Project.Models;

namespace First_MVC_Project.ReposInterfaces
{
    public interface IOrderRepository
    {
        List<Orders> GetAll(string id);
        Orders GetByPIdUId(int Pid,string Uid);
        int Create(Orders order);
        int Update(int id, Orders order);
        int Delete(int id);
    }
}
