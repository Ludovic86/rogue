using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using rogue.models;
using rogueManagement;

namespace rogueManagement.Controllers
{
    public class EnnemisController : Controller
    {
        private BddContext db = new BddContext();

        // GET: Ennemis
        public ActionResult Index()
        {
            return View(db.Ennemi.ToList());
        }

        // GET: Ennemis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ennemi ennemi = db.Ennemi.Find(id);
            if (ennemi == null)
            {
                return HttpNotFound();
            }
            return View(ennemi);
        }

        // GET: Ennemis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ennemis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEnnemi,NomEnemi,AtkEnnemi,SpeedEnnemi,PvEnnemi,Isboss")] Ennemi ennemi)
        {
            if (ModelState.IsValid)
            {
                db.Ennemi.Add(ennemi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ennemi);
        }

        // GET: Ennemis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ennemi ennemi = db.Ennemi.Find(id);
            if (ennemi == null)
            {
                return HttpNotFound();
            }
            return View(ennemi);
        }

        // POST: Ennemis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEnnemi,NomEnemi,AtkEnnemi,SpeedEnnemi,PvEnnemi,Isboss")] Ennemi ennemi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ennemi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ennemi);
        }

        // GET: Ennemis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ennemi ennemi = db.Ennemi.Find(id);
            if (ennemi == null)
            {
                return HttpNotFound();
            }
            return View(ennemi);
        }

        // POST: Ennemis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ennemi ennemi = db.Ennemi.Find(id);
            db.Ennemi.Remove(ennemi);
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
