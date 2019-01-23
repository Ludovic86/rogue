using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using rogue.models;
using rogueManagement;

namespace rogueManagement.Controllers
{
    public class DonjonsController : Controller
    {
        private BddContext db = new BddContext();

        // GET: Donjons
        public ActionResult Index()
        {
            return View(db.Donjon.ToList());
        }

        // GET: Donjons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donjon donjon = db.Donjon.Find(id);
            if (donjon == null)
            {
                return HttpNotFound();
            }
            return View(donjon);
        }

        // GET: Donjons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donjons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDonjon,NomDonjon,ImageDonjon")] Donjon donjon)
        {
            if (ModelState.IsValid)
            {
                db.Donjon.Add(donjon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donjon);
        }

        // GET: Donjons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donjon donjon = db.Donjon.Find(id);
            if (donjon == null)
            {
                return HttpNotFound();
            }
            return View(donjon);
        }

        // POST: Donjons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDonjon,NomDonjon,ImageDonjon")] Donjon donjon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donjon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donjon);
        }

        // GET: Donjons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donjon donjon = db.Donjon.Find(id);
            if (donjon == null)
            {
                return HttpNotFound();
            }
            return View(donjon);
        }

        // POST: Donjons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donjon donjon = db.Donjon.Find(id);
            db.Donjon.Remove(donjon);
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
