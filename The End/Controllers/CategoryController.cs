using Microsoft.AspNetCore.Mvc;
using The_End.Context;
using The_End.Models;

namespace The_End.Controllers
{
    public class CategoryController : Controller
    {
        MyContext db = new MyContext();

        [HttpGet]
        public IActionResult Index()
        {
            var category = db.Categories;
            return View(category);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Categ = db.Categories.Find(id);
            if (Categ == null)
            {
                return RedirectToAction("Index");
            }
            return View(Categ);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            db.Categories.Update(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var Categ = db.Categories.Find(id);
            if (Categ == null)
            {
                return RedirectToAction("Index");
            }
            db.Categories.Remove(Categ);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

