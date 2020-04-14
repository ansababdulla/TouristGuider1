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
    public class CarsController : Controller
    {
        private Entities db = new Entities();
        
        // GET: Cars
        public ActionResult Index()
        {
            String role = Session["Role"].ToString();
            if(role !="Rent Owner")
                {
                return RedirectToAction("Signin", "Account");
                }
            var id = Convert.ToInt32(Session["id"]);
            RentCar rtcr = db.RentCars.Where(r => r.CredID == id).FirstOrDefault();
            var cars = db.Cars.Include(c => c.RentCar).Where(c => c.RtID == rtcr.RtID);
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.RtID = new SelectList(db.RentCars, "RtID", "RtNm");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Car car)
        {
            if (ModelState.IsValid)
            {
                var id = Convert.ToInt32(Session["id"]);
                RentCar rt = db.RentCars.Where(r => r.CredID == id).FirstOrDefault();
                Car cr = new Car();
                cr.CarNm = car.CarNm;
                cr.CarRt = car.CarRt;
                cr.RtID = rt.RtID;
                db.Cars.Add(cr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RtID = new SelectList(db.RentCars, "RtID", "RtNm", car.RtID);
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.RtID = new SelectList(db.RentCars, "RtID", "RtNm", car.RtID);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarID,RtID,CarNm,CarRt")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RtID = new SelectList(db.RentCars, "RtID", "RtNm", car.RtID);
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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
