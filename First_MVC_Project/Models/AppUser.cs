using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_MVC_Project.Models
{
    public class AppUser:IdentityUser
    {
        public string? FirstName {  get; set; }
        public string? LastName { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
