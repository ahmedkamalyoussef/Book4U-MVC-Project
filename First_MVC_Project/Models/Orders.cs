using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_MVC_Project.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public int ProdId { get; set; }
        public int ProductsCount { get; set; }
        public string UserId { get; set; }
        public bool state {  get; set; }
        //[Required]
        [ForeignKey("ProdId")]
        public Product Product { get; set; }
        //[Required]
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}
