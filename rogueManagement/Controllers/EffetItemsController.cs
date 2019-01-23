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
    public class EffetItemsController : Controller
    {
        private BddContext db = new BddContext();

        // GET: EffetItems
        public ActionResult Index()
        {
            return View(db.EffetItems.ToList());
        }

        // GET: EffetItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EffetItem effetItem = db.EffetItems.Find(id);
            if (effetItem == null)
            {
                return HttpNotFound();
            }
            return View(effetItem);
        }

        // GET: EffetItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EffetItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Iditem,AtkItem,SpeedItem,HpItem")] EffetItem effetItem)
        {
            if (ModelState.IsValid)
            {
                db.EffetItems.Add(effetItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(effetItem);
        }

        // GET: EffetItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EffetItem effetItem = db.EffetItems.Find(id);
            if (effetItem == null)
            {
                return HttpNotFound();
            }
            return View(effetItem);
        }

        // POST: EffetItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Iditem,AtkItem,SpeedItem,HpItem")] EffetItem effetItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(effetItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(effetItem);
        }

        // GET: EffetItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EffetItem effetItem = db.EffetItems.Find(id);
            if (effetItem == null)
            {
                return HttpNotFound();
            }
            return View(effetItem);
        }

        // POST: EffetItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EffetItem effetItem = db.EffetItems.Find(id);
            db.EffetItems.Remove(effetItem);
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
