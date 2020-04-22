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
    public class FoodsController : Controller
    {
        private Entities db = new Entities();

        // GET: Foods
        public ActionResult Index()
        {
            String role = Session["Role"].ToString();
            if (role != "Restaurant Owner")
            {
                return RedirectToAction("Signin", "Account");
            }
            var id = Convert.ToInt32(Session["id"]);
            Restaurant res = db.Restaurants.Where(x => x.CredID == id).FirstOrDefault();
            var foods = db.Foods.Include(f => f.Restaurant).Where(f => f.RstID == res.RstID);
            return View(foods.ToList());
        }
        public ActionResult Viewfood(int id)
        {

            var fd = db.Foods.Include(f => f.Restaurant).Where(f => f.RstID == id).ToList();
            var view = new CreateVM()
            {
                _food = fd
            };
            TempData["message"] = "Food Added to Plate Successfully";
            return View(fd);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Viewfood(Food createVM)
        {
            //    FoodOrderDetail fdodrdtls = new FoodOrderDetail();
            //    fdodrdtls.FdID = id;
            //    fdodrdtls.Qty = qty;
            //    db.FoodOrderDetails.Add(fdodrdtls);
            //    db.SaveChanges();
            return View();
        }
        //public ActionResult Addfood()
        //{
        //    return View();
        //}


        // GET: Foods/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            ViewBag.RstID = new SelectList(db.Restaurants, "RstID", "RstNm");
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Food food)
        {
            var id = Convert.ToInt32(Session["id"]);
            Restaurant res1 = db.Restaurants.Where(x => x.CredID == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                Restaurant res = new Restaurant();
                Food fd = new Food();
                fd.FdNm = food.FdNm;
                fd.FdRt = food.FdRt;
                fd.FdImg = food.FdImg;
                fd.RstID = res1.RstID;
                db.Foods.Add(fd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RstID = new SelectList(db.Restaurants, "RstID", "RstNm", food.RstID);
            return View(food);
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.RstID = new SelectList(db.Restaurants, "RstID", "RstNm", food.RstID);
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FdID,RstID,FdNm,FdRt")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RstID = new SelectList(db.Restaurants, "RstID", "RstNm", food.RstID);
            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
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
