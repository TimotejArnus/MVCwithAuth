using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NapredniObrazec.DataBase;
using NapredniObrazec.Models;

namespace NapredniObrazec.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/User/PreviewUser.cshtml");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("~/Views/User/AddUser.cshtml");
        }

        [HttpGet]
        public IActionResult DeleteAll()
        {
            if (ModelState.IsValid)
            {
                using (Database db = new Database())
                {
                    Database.Clear(db.Users);
                    db.SaveChanges();

                }

            }
            return View("~/Views/User/PreviewUser.cshtml");
        }

        [HttpGet]
        public IActionResult Delete(User user)
        {
            User u;
            using (Database db = new Database())
            {
                try
                {
                    u = db.Users.First(u => u.Id == user.Id);
                    db.Users.Remove(u);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    
                }
                
            }

            return View("~/Views/User/PreviewUser.cshtml");
        }

        [HttpGet]
        public IActionResult Show(User user)
        {
            User u;
            using (Database db = new Database())
            {
                u = db.Users.First(u => u.Id == user.Id);
            }
            return View("~/Views/User/ReviewUser.cshtml", u);
        }


        [HttpGet]
        public IActionResult Edit(User user)
        {
            User u;
            using (Database db = new Database())
            {
                u = db.Users.First(u => u.Id == user.Id);
            }
            return View("~/Views/User/EditUser.cshtml",u);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditinDB(User user)
        {

            if (ModelState.IsValid)
            {
                User u;
                using (Database db = new Database())
                {

                    u = db.Users.First(u => u.Id == user.Id);
                    db.Entry(u).CurrentValues.SetValues(user);
                    db.SaveChanges();

                }
                return View("~/Views/User/ReviewUser.cshtml", user);
            }
            return View("~/Views/User/EditUser.cshtml", user);


        }



        [HttpPost]
        public IActionResult Add(User user)
        {
            if (ModelState.IsValid)
            {
                using (Database db = new Database())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return View("~/Views/User/ReviewUser.cshtml",user);
            }
            return View("~/Views/User/AddUser.cshtml");
        }

       
    }
}