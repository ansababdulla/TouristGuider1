using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristGuider.Models;
using TouristGuider.ViewModel;

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
                return RedirectToAction("Index");
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
            var res = db.Credentials.Where(x => x.Email == regvm.email && x.Password == regvm.password).FirstOrDefault();
            if (res.Email == regvm.email && res.Password == regvm.password)
            {
                if (res.RoleID == 1)
                {
                    return RedirectToAction("Index", "Home");
                    
                }
                if(res.RoleID == 2)
                {
                    return RedirectToAction("Index","Admin");
                }
                if (res.RoleID == 3)
                {
                    return RedirectToAction("Index", "Admin");
                }
                if (res.RoleID == 4)
                {
                    return RedirectToAction("Index", "Admin");
                }
                if (res.RoleID == 5)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();



        }
    }
}