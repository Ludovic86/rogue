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
    public class TypeItemsController : Controller
    {
        private BddContext db = new BddContext();

        // GET: TypeItems
        public ActionResult Index()
        {
            return View(db.TypeItem.ToList());
        }

        // GET: TypeItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeItem typeItem = db.TypeItem.Find(id);
            if (typeItem == null)
            {
                return HttpNotFound();
            }
            return View(typeItem);
        }

        // GET: TypeItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdType,NomType")] TypeItem typeItem)
        {
            if (ModelState.IsValid)
            {
                db.TypeItem.Add(typeItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeItem);
        }

        // GET: TypeItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeItem typeItem = db.TypeItem.Find(id);
            if (typeItem == null)
            {
                return HttpNotFound();
            }
            return View(typeItem);
        }

        // POST: TypeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdType,NomType")] TypeItem typeItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeItem);
        }

        // GET: TypeItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeItem typeItem = db.TypeItem.Find(id);
            if (typeItem == null)
            {
                return HttpNotFound();
            }
            return View(typeItem);
        }

        // POST: TypeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeItem typeItem = db.TypeItem.Find(id);
            db.TypeItem.Remove(typeItem);
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
