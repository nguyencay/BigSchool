using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BigSchool.Controllers
{
    public class CourseController : Controller
    {
        BigSchoolContext context = new BigSchoolContext();
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            
            Coursee objCoursee = new Coursee();
            objCoursee.ListCategory = context.Categories.ToList();
            return View(objCoursee);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Coursee objCouse)
        {
            ModelState.Remove("LecturerId");
            if (!ModelState.IsValid)
            {
                objCouse.ListCategory = context.Categories.ToList();
                return View("Create", objCouse);
            }

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objCouse.LecturerId = user.Id;
                context.Coursees.Add(objCouse);
                context.SaveChanges();
                


            return RedirectToAction("Index", "Home");
        }
    }
}