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
    public class RoomsController : Controller
    {
        private Entities db = new Entities();

        // GET: Rooms
        public ActionResult Index()
        {
            String role = Session["Role"].ToString();
            if (role != "Hotel Owner")
            {
                return RedirectToAction("Signin", "Account");
            }
            var id = Convert.ToInt32(Session["id"]);
            Hotel htl = db.Hotels.Where(x => x.CredID == id).FirstOrDefault();
            var rooms = db.Rooms.Include(f => f.Hotel).Where(r => r.HtlID == htl.HtlID);
            return View(rooms.ToList());
        }

        public ActionResult ViewRoom(int id)
        {

            var rooms = db.Rooms.Include(f => f.Hotel).Where(x => x.HtlID == id);
            return View(rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.HtlID = new SelectList(db.Hotels, "HtlID", "HtlNm");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room)
        {
            var id = Convert.ToInt32(Session["id"]);
            Hotel htl = db.Hotels.Where(x => x.CredID == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                Restaurant res = new Restaurant();
                Room rm = new Room();
                rm.RmNm = room.RmNm;
                rm.RmRt = room.RmRt;
                rm.HtlID = htl.HtlID;
                db.Rooms.Add(rm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.HtlID = new SelectList(db.Hotels, "HtlID", "HtlNm", room.HtlID);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.HtlID = new SelectList(db.Hotels, "HtlID", "HtlNm", room.HtlID);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RmID,HtlID,RmNm,RmRt")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HtlID = new SelectList(db.Hotels, "HtlID", "HtlNm", room.HtlID);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
