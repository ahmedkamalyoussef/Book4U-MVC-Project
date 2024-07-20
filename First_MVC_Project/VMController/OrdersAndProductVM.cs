using First_MVC_Project.Models;
using sib_api_v3_sdk.Model;

namespace First_MVC_Project.VMController
{
    public class OrdersAndProductVM
    {
        public int OrderId { get; set; }
        public int ProductsCount { get; set; }
        public double Productprice { get; set; }

        public string ProductUrl { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CategoryName { get; set; }
    }
}
