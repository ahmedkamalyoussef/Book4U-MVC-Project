using First_MVC_Project.Models;
using First_MVC_Project.ReposInterfaces;
using First_MVC_Project.utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace First_MVC_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =Roles.RoleAdmin)]
    public class CategoryController : Controller
    {
        ICategoryRepository category;
        public CategoryController(ICategoryRepository _category)
        {
            category = _category;
        }

        //validation
        public IActionResult OrderExist(int DesplayOrder, int id)
        {

            if (id == 0)
            {
                var cat = category.GetBydesOrder(DesplayOrder);
                if (cat == null)
                    return Json(true);
                else
                    return Json(false);
            }
            else
            {
                var cat = category.GetBydesOrder(DesplayOrder);
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



        public IActionResult CatExist(string Name, int id)
        {

            if (id == 0)
            {
                var cat = category.GetByName(Name);
                if (cat == null)
                    return Json(true);
                else
                    return Json(false);
            }
            else
            {
                var cat = category.GetByName(Name);
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
        /////////////////////////////////////////////////////////

        public IActionResult GetAll()
        {
            return View(category.GetAll());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category _category)
        {
            if (ModelState.IsValid)
            {
                category.Create(_category);
                TempData["success"] = "Category has been added successfully";
                return RedirectToAction("Create");
            }
            TempData["fail"] = "failed to add Category";
            return RedirectToAction("create");
        }

        public IActionResult Edit(int id)
        {
            var cat = category.GetById(id);
            if (cat != null)
            {
                return View(cat);
            }
            TempData["fail"] = "category dosen't exist";
            return RedirectToAction("getall");
        }

        [HttpPost]
        public IActionResult Edit(int id, Category _category)
        {
            if (ModelState.IsValid)
            {
                category.Update(id, _category);
                TempData["success"] = "Category has been updated successfully";
                return RedirectToAction("getall");
            }
            TempData["fail"] = "failed to update Category";
            return RedirectToAction("Edit");
        }

        public IActionResult delete(int id)
        {
            if(category.Delete(id)>0)
            TempData["success"] = "Category has been deleted successfully";
            else TempData["fail"] = "failed to delete Category";
            return RedirectToAction("getall");
        }
    }
}
