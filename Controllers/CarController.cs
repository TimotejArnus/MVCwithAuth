using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using NapredniObrazec.DataBase;
using NapredniObrazec.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;


namespace NapredniObrazec.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return View("~/Views/Car/PreviewCar.cshtml");
        //}

        //private readonly DataBase.Database _context;

        //public CarController(DataBase.Database context)
        //{
        //    _context = context;
        //}


         
        
        [Authorize(Roles = "User,Admin")]
        public IActionResult Index()
        {            
            return View("~/Views/Car/PreviewCar.cshtml");
        }

        [HttpGet]

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View("~/Views/Car/AddCar.cshtml");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAll()
        {
            if (ModelState.IsValid)
            {
                using (Database db = new Database())
                {
                    Database.Clear(db.Cars);
                    db.SaveChanges();

                }
                
            }
            return View("~/Views/Car/PreviewCar.cshtml");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Car car, Database db2)
        {
            Car c;
           Database db = new Database();
            
                try
                {
                    c = db.Cars.First(c => c.Id == car.Id);
                    db.Cars.Remove(c);
                    db.SaveChanges();
                }
                catch (System.Exception)
                {

                    
                }                
            

            return View("~/Views/Car/PreviewCar.cshtml");
        }

        [HttpGet]
        //[Authorize(Roles = "User,Admin")]
        public IActionResult Show(Car car)
        {
            Car c;
            using (Database db = new Database())
            {
                c = db.Cars.First(c => c.Id == car.Id);
            }
            return View("~/Views/Car/ReviewCar.cshtml", c);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Car car)
        {
            Car c;
            using (Database db = new Database())
            {
                c = db.Cars.First(c => c.Id == car.Id);
            }
            return View("~/Views/Car/EditCar.cshtml",c);
        }

        [HttpPost]
        [ActionName("Edit")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditinDB(Car car)
        {

            if (ModelState.IsValid)
            {
                Car c;
                using (Database db = new Database())
                {
                    
                    c = db.Cars.First(c => c.Id == car.Id);
                    db.Entry(c).CurrentValues.SetValues(car);
                    db.SaveChanges();

                }
                return View("~/Views/Car/ReviewCar.cshtml", car);
            }
            return View("~/Views/Car/EditCar.cshtml", car);


        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(Car car)
        {
            if (ModelState.IsValid)
            {
                using (Database db = new Database())
                {
                    db.Cars.Add(car);
                    db.SaveChanges();
                    
                }
                return View("~/Views/Car/ReviewCar.cshtml", car);
            }
            return View("~/Views/Car/AddCar.cshtml");
        }
    }
}