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
    public class FoodOrderDetailsController : Controller
    {
        private Entities db = new Entities();

        // GET: FoodOrderDetails
        public ActionResult Index()
        {
            var foodOrderDetails = db.FoodOrderDetails.Include(f => f.Food);
            return View(foodOrderDetails.ToList());
        }

        // GET: FoodOrderDetails/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrderDetail foodOrderDetail = db.FoodOrderDetails.Find(id);
            if (foodOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(foodOrderDetail);
        }

        // GET: FoodOrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.FdID = new SelectList(db.Foods, "FdID", "FdNm");
            return View();
        }

        // POST: FoodOrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FdOdrDtID,FdID,Qty")] FoodOrderDetail foodOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.FoodOrderDetails.Add(foodOrderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FdID = new SelectList(db.Foods, "FdID", "FdNm", foodOrderDetail.FdID);
            return View(foodOrderDetail);
        }

        // GET: FoodOrderDetails/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrderDetail foodOrderDetail = db.FoodOrderDetails.Find(id);
            if (foodOrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.FdID = new SelectList(db.Foods, "FdID", "FdNm", foodOrderDetail.FdID);
            return View(foodOrderDetail);
        }

        // POST: FoodOrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FdOdrDtID,FdID,Qty")] FoodOrderDetail foodOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodOrderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FdID = new SelectList(db.Foods, "FdID", "FdNm", foodOrderDetail.FdID);
            return View(foodOrderDetail);
        }

        // GET: FoodOrderDetails/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrderDetail foodOrderDetail = db.FoodOrderDetails.Find(id);
            if (foodOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(foodOrderDetail);
        }

        // POST: FoodOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FoodOrderDetail foodOrderDetail = db.FoodOrderDetails.Find(id);
            db.FoodOrderDetails.Remove(foodOrderDetail);
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
