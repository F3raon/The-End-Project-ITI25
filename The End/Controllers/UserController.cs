using Microsoft.AspNetCore.Mvc;
using The_End.Context;
using The_End.Models;

namespace The_End.Controllers
{
    public class UserController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.userId == id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

      
        [HttpPost]
        public IActionResult Register(User user)
        {
            var exEmail = db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (exEmail != null)
            {
                ModelState.AddModelError("", "Email already exists");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);
        }

       
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                return RedirectToAction("Index", "Product"); 
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }
        
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (user != null && ModelState.IsValid)
            {
                // Search With Id And Make Update
                db.Users.Update(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //if (ModelState.IsValid)
            //{
            //    db.Users.Update(user);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
