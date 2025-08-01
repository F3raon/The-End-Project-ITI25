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
            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var titleExists = db.Products.FirstOrDefault(e => e.Title == product.Title);
            if (titleExists != null)
            {
                ModelState.AddModelError("Title", "Title Already Exist");
                ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name");
                return View(product);
            }

            ModelState.Remove("Category");
            ModelState.Remove("ImagePath"); 
            if (product != null && ModelState.IsValid)
            {
                if (product.Image != null && product.Image.Length > 0)
                {
                   
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + product.Image.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.Image.CopyToAsync(fileStream);
                    }
                    product.ImagePath = "/images/" + uniqueFileName;
                }
                else
                {
                  
                    product.ImagePath = "/images/default.png";
                }

                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "All Fields required");
            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name");
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = db.Products.Include(e => e.Category).FirstOrDefault(e => e.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name");
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            ModelState.Remove("Category");
            ModelState.Remove("ImagePath");
            if (product != null && ModelState.IsValid)
            {
               
                if (product.Image != null && product.Image.Length > 0)
                {
                    var oldProduct = await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == product.ProductId);
                    if (oldProduct != null && !string.IsNullOrEmpty(oldProduct.ImagePath) && oldProduct.ImagePath != "/images/default.png")
                    {
                     
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldProduct.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                  
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + product.Image.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.Image.CopyToAsync(fileStream);
                    }
                    product.ImagePath = "/images/" + uniqueFileName;
                }
                else
                {
                    
                    var oldProduct = await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == product.ProductId);
                    product.ImagePath = oldProduct.ImagePath;
                }

                db.Products.Update(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag._Categories = new SelectList(db.Categories, "CategoryId", "Name");
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

            
            if (!string.IsNullOrEmpty(product.ImagePath) && product.ImagePath != "/images/default.png")
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
