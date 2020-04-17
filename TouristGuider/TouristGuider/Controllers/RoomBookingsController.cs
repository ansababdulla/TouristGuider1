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
    public class RoomBookingsController : Controller
    {
        private Entities db = new Entities();

        // GET: RoomBookings
        public ActionResult Index()
        {
            var cred = Convert.ToInt32(Session["id"]);
            Hotel htl = db.Hotels.Where(x => x.CredID == cred).FirstOrDefault();
            var roomBookings = db.RoomBookings.Include(r => r.User);
            return View(roomBookings.ToList());
        }
        public ActionResult Userview()
        {
            var id = Convert.ToInt32(Session["Usrid"]);
            var RoomBkng = db.RoomBookings.Include(f => f.User).Where(x => x.UserID == id);
            return View(RoomBkng.ToList());
        }


        public ActionResult Paid(int id)
        {
            RoomBooking rmbk = db.RoomBookings.Where(x => x.RmBkID == id).FirstOrDefault();
            rmbk.isCheckout = true;
            rmbk.ChlOtDt = DateTime.Now;
            db.Entry(rmbk).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "RoomBookings");

        }
        public ActionResult AddRoom(int rmid,int htlid)
        {

            Room rm = db.Rooms.Where(x => x.RmID == rmid).FirstOrDefault();
            var usrid = Convert.ToInt32(Session["Usrid"]);
            User usr = db.Users.Where(x => x.UserID == usrid).FirstOrDefault();
            RoomBooking rmbk = new RoomBooking();
            RoomBooking rmbkng = db.RoomBookings.Where(x => x.UserID == usr.UserID && x.isCheckout == false).FirstOrDefault();
            if (rmbkng != null )
            {
                rmbkng.TtlRt = rmbkng.TtlRt + Convert.ToInt32(rm.RmRt);
                db.Entry(rmbkng).State = EntityState.Modified;
                db.SaveChanges();
                return View();
            }
            else
            {
                rmbkng = new RoomBooking();
                rmbkng.UserID = usr.UserID;
                rmbkng.RmID = rmid;
                rmbkng.ChkInDt = DateTime.Now;
                rmbkng.ChlOtDt = null;
                rmbkng.TtlRt = Convert.ToInt32(rm.RmRt);
                rmbkng.isCheckout = false;
                rmbkng.HtlID = htlid;
                db.RoomBookings.Add(rmbkng);
                db.SaveChanges();
                return View();
            }
        }

        // GET: RoomBookings/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            return View(roomBooking);
        }

        // GET: RoomBookings/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name");
            return View();
        }

        // POST: RoomBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RmBkID,RmID,TtlRt,ChkInDt,ChlOtDt,isCheckout,UserID")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                db.RoomBookings.Add(roomBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", roomBooking.UserID);
            return View(roomBooking);
        }

        // GET: RoomBookings/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", roomBooking.UserID);
            return View(roomBooking);
        }

        // POST: RoomBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RmBkID,RmID,TtlRt,ChkInDt,ChlOtDt,isCheckout,UserID")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", roomBooking.UserID);
            return View(roomBooking);
        }

        // GET: RoomBookings/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            return View(roomBooking);
        }

        // POST: RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            db.RoomBookings.Remove(roomBooking);
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
