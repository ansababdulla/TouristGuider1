using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TouristGuider.Models;

namespace TouristGuider.Controllers
{
    public class RentCarsController : Controller
    {
        private Entities db = new Entities();

        // GET: RentCars
        public ActionResult Index()
        {
            var rentCars = db.RentCars.Include(r => r.Credential);
            return View(rentCars.ToList());
        }

        // GET: RentCars/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentCar rentCar = db.RentCars.Find(id);
            if (rentCar == null)
            {
                return HttpNotFound();
            }
            return View(rentCar);
        }

        // GET: RentCars/Create
        public ActionResult Create()
        {
            ViewBag.CredID = new SelectList(db.Credentials, "CredID", "Email");
            return View();
        }

        // POST: RentCars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RtID,CredID,RtNm")] RentCar rentCar)
        {
            if (ModelState.IsValid)
            {
                db.RentCars.Add(rentCar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CredID = new SelectList(db.Credentials, "CredID", "Email", rentCar.CredID);
            return View(rentCar);
        }

        // GET: RentCars/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentCar rentCar = db.RentCars.Find(id);
            if (rentCar == null)
            {
                return HttpNotFound();
            }
            ViewBag.CredID = new SelectList(db.Credentials, "CredID", "Email", rentCar.CredID);
            return View(rentCar);
        }

        // POST: RentCars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RtID,CredID,RtNm")] RentCar rentCar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentCar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CredID = new SelectList(db.Credentials, "CredID", "Email", rentCar.CredID);
            return View(rentCar);
        }

        // GET: RentCars/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentCar rentCar = db.RentCars.Find(id);
            if (rentCar == null)
            {
                return HttpNotFound();
            }
            return View(rentCar);
        }

        // POST: RentCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RentCar rentCar = db.RentCars.Find(id);
            db.RentCars.Remove(rentCar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
