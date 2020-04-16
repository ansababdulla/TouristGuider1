using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristGuider.Models;
using TouristGuider.ViewModel;
using System.Data.Entity;

namespace TouristGuider.Controllers
{
    public class AccountController : Controller
    {
        private Entities db = new Entities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(RegistrationVM regvm )
        {
            if (ModelState.IsValid)
            {

                Role role = new Role();
                Credential cred = new Credential();
                cred.Email = regvm.email;
                cred.Password = regvm.password;
                cred.RoleID = 1;
                db.Credentials.Add(cred);
                db.SaveChanges();
                User user = new User();
                user.Name = regvm.name;
                user.CredID = cred.CredID;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Signin");
            }
            return View();
        }
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(RegistrationVM regvm)
        {

            Credential cred = db.Credentials.Where(x => x.Email == regvm.email && x.Password == regvm.password).FirstOrDefault();
            var user = db.Users.Where(u => u.CredID == cred.CredID).FirstOrDefault();
            if (cred != null)
            {
                if (cred.RoleID == 1)
                {
                    Session["Role"] = "User";
                    Session["id"] = cred.CredID;
                    Session["Usrid"] = user.UserID;
                    return RedirectToAction("Index", "Home");
                    
                }
                if(cred.RoleID == 2)
                {
                    Session["Role"] = "Admin";
                    Session["id"] = cred.CredID;
                    //Session["Usrid"] = user.UserID;
                    return RedirectToAction("Index","Admin");
                }
                if (cred.RoleID == 3)
                {
                    Session["Role"] = "Restaurant Owner";
                    Session["id"] = cred.CredID;
                    Session["Usrid"] = user.UserID;
                    return RedirectToAction("Index", "Foods");
                }
                if (cred.RoleID == 4)
                {
                    Session["Role"] = "Hotel Owner";
                    Session["id"] = cred.CredID;
                    Session["Usrid"] = user.UserID;
                    return RedirectToAction("Index", "Rooms");
                }
                if (cred.RoleID == 5)
                {
                    Session["Role"] = "Rent Owner";
                    Session["id"] = cred.CredID;
                    Session["Usrid"] = user.UserID;
                    return RedirectToAction("Index", "Cars");
                }
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Username or Password");
            }
            return View();



        }
    }
}