using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using First_MVC_Project.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace First_MVC_Project.Models
{
    [ModelMetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
        
    }
    public class ProductMetaData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        //[Remote( action: "ProductExist", controller: "Home", ErrorMessage = "this title is already exist", AdditionalFields = "Id")]

        //[Remote(action: "ProductExist", controller: "product", ErrorMessage = "this title is already exist", AdditionalFields = "Id")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required, Display(Name = "Price for 1-50")]
        public double price { get; set; }
        [Required, Display(Name = "Price for 51-100")]
        public double price50 { get; set; }
        [Required, Display(Name = "Price for +100")]
        public double price100 { get; set; }
        [Required,Display(Name ="Product Image")]
        [ValidateNever]
        public string ProductUrl { get; set; }

    }
}
