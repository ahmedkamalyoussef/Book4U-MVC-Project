using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace First_MVC_Project.Models
{
    public partial class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DesplayOrder { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
