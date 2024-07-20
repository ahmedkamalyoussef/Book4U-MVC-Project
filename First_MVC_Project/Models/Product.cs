    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace First_MVC_Project.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? PublisherId {  get; set; }
        public string ISBN { get; set; }

        public string Author { get; set; }
        public double price { get; set; }
        public double price50 { get; set; }
        public double price100 { get; set; }
        public string? ProductUrl { get; set; }
        [Required]
        public int CategoryId {  get; set; }
        public int Coantity { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }
        [ForeignKey("PublisherId")]
        public AppUser? AppUser { get; set; }
     }
}
