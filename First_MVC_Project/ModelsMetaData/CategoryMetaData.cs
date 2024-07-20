using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace First_MVC_Project.Models
{
    [ModelMetadataType(typeof(CategoryMetaData))]
    public partial class Category
    { }
    public class CategoryMetaData
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        //[RegularExpression(pattern: "^[A-Za-z\\s]{5,30}",
        //ErrorMessage ="Name must be only characters and must be less than 30 and more than 5")]
        [Remote(action: "CatExist", controller: "category", ErrorMessage = "this category is already exist", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Remote(action: "OrderExist", controller: "Category", ErrorMessage = "this order already taken", AdditionalFields = "Id")]
        [DisplayName("Display Order")]
        public int DesplayOrder { get; set; }
    }
}
