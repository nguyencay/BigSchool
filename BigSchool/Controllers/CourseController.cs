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

        public ActionResult Edit(int id)
        {
            var b = context.Coursees.First(m => m.Id == id);
            b.ListCategory = context.Categories.ToList();
            return View(b);
        }
        //[HttpPost]
        [Authorize]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection, Coursee objCourse)
        {
            //objCourse.ListCategory = context.Categories.ToList();
            var b = context.Coursees.First(m => m.Id == id);
            if (b != null)
            {
                UpdateModel(b);
                context.SaveChanges();
                
                return RedirectToAction("Mine");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var b = context.Coursees.First(m => m.Id == id);
            return View(b);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var a = context.Attendances.Where(m => m.CourseId == id).First();
            context.Attendances.Remove(a);
            context.SaveChanges();
            //Coursee b = context.Coursees.FirstOrDefault(x => x.Id == id);
            var b = context.Coursees.Where(x => x.Id == id).First();
            context.Coursees.Remove(b);
            context.SaveChanges();
            return RedirectToAction("Mine");
        }


        public ActionResult Attending()
        {
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var listAttendances = context.Attendances.Where(p => p.Attendee == currentUser.Id).ToList();
            var courses = new List<Coursee>();
            foreach (Attendance temp in listAttendances)
            {
                Coursee objCourse = temp.Coursee;
                objCourse.LecturerName = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(objCourse.LecturerId).Name;
                courses.Add(objCourse);
            }
            return View(courses);
        }

        public ActionResult Mine()
        {
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var courses = context.Coursees.Where(c => c.LecturerId == currentUser.Id && c.DateTime > DateTime.Now).ToList();
            foreach (Coursee i in courses)
            {
                i.LecturerName = currentUser.Name;
            }
            return View(courses);
        }
        public ActionResult LectureIamGoing()
        {
            ApplicationUser currentUser =
            System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
            .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            BigSchoolContext context = new BigSchoolContext();
            var listFollowee = context.Followings.Where(p => p.FollowerId ==

            currentUser.Id).ToList();
            var listAttendances = context.Attendances.Where(p => p.Attendee ==

            currentUser.Id).ToList();

            var courses = new List<Coursee>();
            foreach (var course in listAttendances)

            {
                foreach (var item in listFollowee)
                {
                    if (item.FolloweeId == course.Coursee.LecturerId)
                    {
                        Coursee objCourse = course.Coursee;
                        objCourse.LecturerName =
                        System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                        .FindById(objCourse.LecturerId).Name;
                        courses.Add(objCourse);
                    }
                }
            }
            return View(courses);
        }
    }
}