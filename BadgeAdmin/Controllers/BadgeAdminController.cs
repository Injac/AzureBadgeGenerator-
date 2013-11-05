using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BadgeAdmin.Models;

namespace BadgeAdmin.Controllers
{
    [ValidateInput(false)]
    public class BadgeAdminController : Controller
    {
        private BadgesEntities db = new BadgesEntities();

        // GET: /BadgeAdmin/
        public ActionResult Index()
        {
            return View(db.Badges.ToList());
        }

        // GET: /BadgeAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Badges badges = db.Badges.Find(id);
            if (badges == null)
            {
                return HttpNotFound();
            }
            return View(badges);
        }

        // GET: /BadgeAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BadgeAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,BadgeName,Badge")] Badges badges)
        {
            if (ModelState.IsValid)
            {
                db.Badges.Add(badges);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(badges);
        }

        // GET: /BadgeAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Badges badges = db.Badges.Find(id);
            if (badges == null)
            {
                return HttpNotFound();
            }
            return View(badges);
        }

        // POST: /BadgeAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,BadgeName,Badge")] Badges badges)
        {
            if (ModelState.IsValid)
            {
                db.Entry(badges).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(badges);
        }

        // GET: /BadgeAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Badges badges = db.Badges.Find(id);
            if (badges == null)
            {
                return HttpNotFound();
            }
            return View(badges);
        }

        // POST: /BadgeAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Badges badges = db.Badges.Find(id);
            db.Badges.Remove(badges);
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
