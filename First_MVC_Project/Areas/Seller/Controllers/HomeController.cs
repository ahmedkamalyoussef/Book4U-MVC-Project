using First_MVC_Project.Models;
using First_MVC_Project.ReposInterfaces;
using First_MVC_Project.utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace First_MVC_Project.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = Roles.RoleSeller)]
    public class HomeController : Controller
    {
        IProductRepository product;
        ICategoryRepository category;
        IWebHostEnvironment env;
        public HomeController(IProductRepository _product, ICategoryRepository _category, IWebHostEnvironment _env)
        {
            product = _product;
            category = _category;
            env = _env;
        }





        public IActionResult ProductExist(string Title, int id)
        {

            if (id == 0)
            {
                var cat = product.GetByTitle(Title);
                if (cat == null)
                    return Json(true);
                else
                    return Json(false);
            }
            else
            {
                var cat = product.GetByTitle(Title);
                if (cat == null)
                    return Json(true);
                else
                {
                    if (cat.Id == id)
                        return Json(true);
                    else
                        return Json(false);

                }
            }
        }
        //validation

        public async Task<IActionResult> GetAllAsync()
        {
            return View(await product.GetAllToProd());
        }
        public ActionResult Create()
        {

            IEnumerable<SelectListItem> categories = category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            ViewBag.categories = categories;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(Product _product, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string rootPath = env.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string proPath = Path.Combine(rootPath, @"images\products");
                    using (var fileStream = new FileStream(Path.Combine(proPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    _product.ProductUrl = @"\images\products\" + fileName;
                }
                await product.CreateAsync(_product);
                TempData["success"] = "Product has been added successfully";
                return RedirectToAction("Create");
            }
            TempData["fail"] = "failed to add Product";
            return RedirectToAction("Create", _product);
        }

        public IActionResult Edit(int id)
        {
            var product = this.product.GetById(id);
            if (product != null)
            {
                IEnumerable<SelectListItem> categories = category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                ViewBag.categories = categories;
                return base.View(product);
            }
            else
                return RedirectToAction("getall");
        }

        [HttpPost]
        public IActionResult Edit(int id, Product _product, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string rootPath = env.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string proPath = Path.Combine(rootPath, @"images\products");
                    if (!string.IsNullOrEmpty(_product.ProductUrl))
                    {
                        var oldImagePath = Path.Combine(rootPath, _product.ProductUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(proPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    _product.ProductUrl = @"\images\products\" + fileName;
                }
                TempData["success"] = "Product edited successfully";
                product.Update(id, _product);
            }
            else
                TempData["fail"] = "failed to edit product";
            return RedirectToAction("Edit");
        }

        public IActionResult delete(int id)
        {
            var pro = product.GetById(id);
            string rootPath = env.WebRootPath;
            var oldImagePath = Path.Combine(rootPath, pro.ProductUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);
            if (product.Delete(id) > 0)
                TempData["success"] = "product has been deleted successfully";
            else TempData["fail"] = "failed to delete the product";
            return RedirectToAction("getall");
        }
    }
}
