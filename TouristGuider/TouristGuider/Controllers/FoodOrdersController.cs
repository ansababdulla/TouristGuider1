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
    public class FoodOrdersController : Controller
    {
        private Entities db = new Entities();

        // GET: FoodOrders
        public ActionResult Index()
        {
            var cred = Convert.ToInt32(Session["id"]);
            Restaurant res = db.Restaurants.Where(x => x.CredID == cred).FirstOrDefault();
            var foodOrders = db.FoodOrders.Include(f => f.User).Where(x => x.RstID == res.RstID);
            return View(foodOrders.ToList());
        }
        public ActionResult Userview()
        {
            var id = Convert.ToInt32(Session["Usrid"]);
            var foodOrders = db.FoodOrders.Include(f => f.User).Where(x => x.UserID == id);
            return View(foodOrders.ToList());
        }


        public ActionResult Paid(int id)
        {
            FoodOrder fdodr = db.FoodOrders.Where(x => x.FdOdrID == id).FirstOrDefault();
            fdodr.isPaid = true;
            db.Entry(fdodr).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index","FoodOrders");

        }

        // GET: FoodOrders/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            if (foodOrder == null)
            {
                return HttpNotFound();
            }
            return View(foodOrder);
        }
        public ActionResult Addfood(Food createVM)
        {
            var userid = Convert.ToInt32(Session["Usrid"]);
            FoodOrderDetail fdodrdtls = new FoodOrderDetail();
            Food fd = db.Foods.Where(x => x.FdID == createVM.FdID).FirstOrDefault();
            FoodOrder fdodrobj = db.FoodOrders.Where(f => f.UserID == userid && f.isPaid == false).FirstOrDefault();
            if(fdodrobj!=null)
            {
                fdodrobj.Ttl = fdodrobj.Ttl + Convert.ToInt64(fd.FdRt) * Convert.ToInt64(createVM.FdRt);//qty
                db.Entry(fdodrobj).State = EntityState.Modified;
                db.SaveChanges();
                fdodrdtls.FdID = fd.FdID;
                fdodrdtls.Qty = Convert.ToInt64(createVM.FdRt);
                fdodrdtls.FdOdrID = fdodrobj.FdOdrID;
                db.FoodOrderDetails.Add(fdodrdtls);
                db.SaveChanges();
            }
            else
            {
                fdodrobj = new FoodOrder();
                fdodrobj.Date = DateTime.Now;
                fdodrobj.isPaid = false;
                fdodrobj.UserID = userid;
                fdodrobj.RstID = createVM.RstID;
                fdodrobj.Ttl = Convert.ToInt64(fd.FdRt) * Convert.ToInt64(createVM.FdRt);
                db.FoodOrders.Add(fdodrobj);
                db.SaveChanges();
                fdodrdtls.FdID = fd.FdID;
                fdodrdtls.Qty = Convert.ToInt64(createVM.FdRt);
                fdodrdtls.FdOdrID = fdodrobj.FdOdrID;
                db.FoodOrderDetails.Add(fdodrdtls);
                db.SaveChanges();

            }
            var food = db.FoodOrderDetails.Where(f => f.FdOdrID == fdodrobj.FdOdrID);
            //FoodOrderDetail fd2 = db.FoodOrderDetails.Where(f => f.FdOdrID == fdodrobj.FdOdrID);
            //if (fd.FdNm != null && fdodrobj.Date == null )
            //{
            //    fdodr.Date = DateTime.Now;
            //    fdodr.isPaid = false;
            //    fdodr.UserID = userid;
            //    fdodr.Ttl = 0;
            //    db.FoodOrders.Add(fdodr);
            //    db.SaveChanges();
            //    fdodrdtls.FdID = fd.FdID;
            //    fdodrdtls.Qty = 1;
            //    fdodrdtls.FdOdrID = fdodrobj.FdOdrID;
            //    db.FoodOrderDetails.Add(fdodrdtls);
            //    db.SaveChanges();
            //    return View();
            //}
            //else
            //{
            //    fdodrdtls.Qty = fdodrdtls.Qty + 1;
            //    db.Entry(fdodrdtls).State = EntityState.Modified;
            //    db.SaveChanges();
            //    fdodr.Ttl = 100;
            //    db.Entry(fdodr).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return View();
            //}
            return View();
        }

        // GET: FoodOrders/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name");
            return View();
        }

        // POST: FoodOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FdOdrID,Date,isPaid,UserID,Ttl")] FoodOrder foodOrder)
        {
            if (ModelState.IsValid)
            {
                db.FoodOrders.Add(foodOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", foodOrder.UserID);
            return View(foodOrder);
        }

        // GET: FoodOrders/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            if (foodOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", foodOrder.UserID);
            return View(foodOrder);
        }

        // POST: FoodOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FdOdrID,Date,isPaid,UserID,Ttl")] FoodOrder foodOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", foodOrder.UserID);
            return View(foodOrder);
        }

        // GET: FoodOrders/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            if (foodOrder == null)
            {
                return HttpNotFound();
            }
            return View(foodOrder);
        }

        // POST: FoodOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            db.FoodOrders.Remove(foodOrder);
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
