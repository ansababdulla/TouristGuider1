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
    public class RentBookingsController : Controller
    {
        private Entities db = new Entities();

        // GET: RentBookings
        public ActionResult Index()
        {
            var rentBookings = db.RentBookings.Include(r => r.Car).Include(r => r.User);
            return View(rentBookings.ToList());
        }

        // GET: RentBookings/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentBooking rentBooking = db.RentBookings.Find(id);
            if (rentBooking == null)
            {
                return HttpNotFound();
            }
            return View(rentBooking);
        }

        // GET: RentBookings/Create
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "CarNm");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name");
            return View();
        }

        // POST: RentBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RtBkID,CarID,DateBooked,DateReturned,NoofDays,TtlRt,isReturned,UserID")] RentBooking rentBooking)
        {
            if (ModelState.IsValid)
            {
                db.RentBookings.Add(rentBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.Cars, "CarID", "CarNm", rentBooking.CarID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", rentBooking.UserID);
            return View(rentBooking);
        }

        // GET: RentBookings/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentBooking rentBooking = db.RentBookings.Find(id);
            if (rentBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "CarNm", rentBooking.CarID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", rentBooking.UserID);
            return View(rentBooking);
        }

        // POST: RentBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RtBkID,CarID,DateBooked,DateReturned,NoofDays,TtlRt,isReturned,UserID")] RentBooking rentBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "CarNm", rentBooking.CarID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", rentBooking.UserID);
            return View(rentBooking);
        }

        // GET: RentBookings/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentBooking rentBooking = db.RentBookings.Find(id);
            if (rentBooking == null)
            {
                return HttpNotFound();
            }
            return View(rentBooking);
        }

        // POST: RentBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RentBooking rentBooking = db.RentBookings.Find(id);
            db.RentBookings.Remove(rentBooking);
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
