using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristGuider.Models;
using System.Data.Entity;

namespace TouristGuider.Controllers
{
    public class AdminController : Controller
    {
        private Entities db = new Entities();
        // GET: Admin
        public ActionResult Index()
        {
            var credentials = db.Credentials.Include(c => c.Role);
            return View(credentials.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleNm");
            return View();
        }

        // POST: Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVM create )
        {
            if (ModelState.IsValid)
            {
                Role rol = new Role();
                Credential cred = new Credential();
                cred.Email = create._credential.Email;
                cred.Password = create._credential.Password;
                cred.RoleID = 2;
                db.Credentials.Add(cred);
                db.SaveChanges();
                User usr = new User();
                usr.Name = create._user.Name;
                usr.CredID = cred.CredID;
                db.Users.Add(usr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleNm", create._credential.RoleID);
            return View(create);
        }
        public ActionResult CreateRstOwner()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleNm");
            return View();
        }

        // POST: Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRstOwner(CreateVM create)
        {
            if (ModelState.IsValid)
            {
                Role rol = new Role();
                Credential cred = new Credential();
                cred.Email = create._credential.Email;
                cred.Password = create._credential.Password;
                cred.RoleID = 3;
                db.Credentials.Add(cred);
                db.SaveChanges();
                User usr = new User();
                usr.Name = create._user.Name;
                usr.CredID = cred.CredID;
                db.Users.Add(usr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleNm", create._credential.RoleID);
            return View(create);
        }
        public ActionResult CreateHtlOwner()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleNm");
            return View();
        }

        // POST: Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHtlOwner(CreateVM create)
        { 
            if (ModelState.IsValid)
            {
                Role rol = new Role();
                Credential cred = new Credential();
                cred.Email = create._credential.Email;
                cred.Password = create._credential.Password;
                cred.RoleID = 4;
                db.Credentials.Add(cred);
                db.SaveChanges();
                User usr = new User();
                usr.Name = create._user.Name;
                usr.CredID = cred.CredID;
                db.Users.Add(usr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleNm", create._credential.RoleID);
            return View(create);
        }
        public ActionResult CreateRenttOwner()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleNm");
            return View();
        }

        // POST: Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRenttOwner(CreateVM create)
        {
            if (ModelState.IsValid)
            {
                Role rol = new Role();
                Credential cred = new Credential();
                cred.Email = create._credential.Email;
                cred.Password = create._credential.Password;
                cred.RoleID = 5;
                db.Credentials.Add(cred);
                db.SaveChanges();
                User usr = new User();
                usr.Name = create._user.Name;
                usr.CredID = cred.CredID;
                db.Users.Add(usr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleNm", create._credential.RoleID);
            return View(create);
        }
    }
}