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
            var id = Convert.ToInt32(Session["id"]);
            RentCar rtcr = db.RentCars.Where(x => x.CredID == id).FirstOrDefault();
            var rentBookings = db.RentBookings.Include(r => r.Car).Include(r => r.User).Where(x => x.RtID == rtcr.RtID);
            return View(rentBookings.ToList());
        }
        public ActionResult Paid(int id)
        {
            RentBooking rtbk = db.RentBookings.Where(x => x.RtBkID == id).FirstOrDefault();
            rtbk.isReturned = true;
            rtbk.DateReturned = DateTime.Now;
            db.Entry(rtbk).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index","RentBookings");
        }
        public ActionResult Userview()
        {
            var id = Convert.ToInt32(Session["Usrid"]);
            var RentBookings = db.RentBookings.Include(f => f.User).Where(x => x.UserID == id);
            return View(RentBookings.ToList());
        }

        public ActionResult Addcar(Car car)
     {
            var flag = false;
            var id = Convert.ToInt32(Session["Usrid"]);
            Car cr = db.Cars.Where(x => x.CarID == car.CarID).FirstOrDefault();
            RentBooking rtbk = db.RentBookings.Where(x => x.UserID == id && x.isReturned == false).FirstOrDefault();
            if (rtbk != null && flag == true)
            {
                rtbk.TtlRt = rtbk.TtlRt + Convert.ToInt32(cr.CarRt) * Convert.ToInt32(car.CarRt);
                db.Entry(rtbk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Viewcar", "Cars", new { id = car.RtID });
            }
            else
            {
                rtbk = new RentBooking();
                rtbk.CarID = car.CarID;
                rtbk.RtID = car.RtID;
                rtbk.DateBooked = DateTime.Now;
                rtbk.DateReturned = null;
                rtbk.NoofDays = Convert.ToInt32(car.CarRt);
                rtbk.TtlRt = Convert.ToInt32(cr.CarRt) * Convert.ToInt32(car.CarRt);
                rtbk.isReturned = false;
                rtbk.UserID = id;
                db.RentBookings.Add(rtbk);
                db.SaveChanges();
                flag = true;
                return RedirectToAction("Viewcar", "Cars", new { id = car.RtID });
            }

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
