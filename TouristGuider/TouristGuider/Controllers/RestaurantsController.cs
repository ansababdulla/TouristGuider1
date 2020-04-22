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

        public class RestaurantsController : Controller
        {
            private Entities db = new Entities();

            // GET: Restaurants
            public ActionResult Index()
            {
                var restaurants = db.Restaurants.Include(r => r.Credential);
                return View(restaurants.ToList());
            }

            // GET: Restaurants/Details/5
            public ActionResult Details(long? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Restaurant restaurant = db.Restaurants.Find(id);
                if (restaurant == null)
                {
                    return HttpNotFound();
                }
                return View(restaurant);
            }

            // GET: Restaurants/Create
            public ActionResult Create()
            {
                ViewBag.CredID = new SelectList(db.Credentials, "CredID", "Email");
                return View();
            }

            // POST: Restaurants/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "RstID,CredID,RstNm,RstLctn")] Restaurant restaurant)
            {
                if (ModelState.IsValid)
                {
                    db.Restaurants.Add(restaurant);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.CredID = new SelectList(db.Credentials, "CredID", "Email", restaurant.CredID);
                return View(restaurant);
            }

            // GET: Restaurants/Edit/5
            public ActionResult Edit(long? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Restaurant restaurant = db.Restaurants.Find(id);
                if (restaurant == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CredID = new SelectList(db.Credentials, "CredID", "Email", restaurant.CredID);
                return View(restaurant);
            }

            // POST: Restaurants/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "RstID,CredID,RstNm,RstLctn")] Restaurant restaurant)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(restaurant).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.CredID = new SelectList(db.Credentials, "CredID", "Email", restaurant.CredID);
                return View(restaurant);
            }

            // GET: Restaurants/Delete/5
            public ActionResult Delete(long? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Restaurant restaurant = db.Restaurants.Find(id);
                if (restaurant == null)
                {
                    return HttpNotFound();
                }
                return View(restaurant);
            }

            // POST: Restaurants/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(long id)
            {
                Restaurant restaurant = db.Restaurants.Find(id);
                db.Restaurants.Remove(restaurant);
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
