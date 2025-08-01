using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using The_End.Context;
using The_End.Models;

namespace The_End.Controllers
{
    public class ProductController : Controller
    {
        MyContext db = new MyContext();

        [HttpGet]
        public IActionResult Index()
        {
            var product = db.Products.Include(e => e.Category);
            return View(product);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = db.Products.Include(e => e.Category).FirstOrDefault(e => e.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name" , "Description");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var Title = db.Products.FirstOrDefault(e => e.Title == product.Title);
            if (Title != null)
            {
                ModelState.AddModelError("", "Title Already Exist");
                ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name", "Description");
                return View();
            }

            ModelState.Remove("Category");
            if (product != null && ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "All Fields required");
            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name", "Description");
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var product = db.Products.Include(e => e.Category).FirstOrDefault(e => e.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name", "Description");
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            ModelState.Remove("Category");
            if (product != null && ModelState.IsValid)
            {
                db.Products.Update(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name", "Description");
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
