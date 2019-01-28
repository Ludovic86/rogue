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
    public class PersonnagesController : Controller
    {
        private rogueContext db = new rogueContext();

        // GET: Personnages
        public ActionResult Index()
        {
            return View(db.Personnage.ToList());
        }

        // GET: Personnages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnage personnage = db.Personnage.Find(id);
            if (personnage == null)
            {
                return HttpNotFound();
            }
            return View(personnage);
        }

        // GET: Personnages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personnages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPersonnage,NomPersonnage,Classe,SpeedPerso,HpPeso,DescriptionPerso,AtkPerso")] Personnage personnage)
        {
            if (ModelState.IsValid)
            {
                db.Personnage.Add(personnage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personnage);
        }

        // GET: Personnages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnage personnage = db.Personnage.Find(id);
            if (personnage == null)
            {
                return HttpNotFound();
            }
            return View(personnage);
        }

        // POST: Personnages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPersonnage,NomPersonnage,Classe,SpeedPerso,HpPeso,DescriptionPerso,AtkPerso")] Personnage personnage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personnage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personnage);
        }

        // GET: Personnages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnage personnage = db.Personnage.Find(id);
            if (personnage == null)
            {
                return HttpNotFound();
            }
            return View(personnage);
        }

        // POST: Personnages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personnage personnage = db.Personnage.Find(id);
            db.Personnage.Remove(personnage);
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
