using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFSecurityShell.Models;

namespace EFSecurityShell.Controllers
{
    public class SeensController : Controller
    {
        private MyMovieListContext db = new MyMovieListContext();

        // GET: Seens
        public ActionResult Index(string sortBy)
        {
            var seens = db.Seens.Include(s => s.Movie);
            ViewBag.SortBy = sortBy;

            switch (sortBy)
            {
                case "Name":
                    seens = seens.OrderBy(p => p.Movie.MovieName);
                    break;
                case "Oldest":
                    seens = seens.OrderBy(p => p.DateSeen);
                    break;
                case "Latest":
                    seens = seens.OrderByDescending(p => p.DateSeen);
                    break;
                case "L_Score":
                    seens = seens.OrderBy(p => p.Score);
                    break;
                case "H_Score":
                    seens = seens.OrderByDescending(p => p.Score);
                    break;
                default:
                    break;
            }
            ViewBag.Sorts = new Dictionary<string, string>
            {
                {"Movie Name", "Name" },
                {"Oldest date", "Oldest" },
                {"Latest date", "Latest" },
                {"Lowest to Highest Rating", "L_Score" },
                {"Highest to Lowest Rating", "H_Score" }
            };

            return View(seens.ToList());
        }

        // GET: Seens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seen seen = db.Seens.Find(id);
            if (seen == null)
            {
                return HttpNotFound();
            }
            return View(seen);
        }

        // GET: Seens/Create
        public ActionResult Create()
        {
            ViewBag.MovieID = new SelectList(db.Movies, "ID", "MovieName");
            return View();
        }

        // POST: Seens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MovieID,DateSeen,Score")] Seen seen)
        {
            if (ModelState.IsValid)
            {
                db.Seens.Add(seen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieID = new SelectList(db.Movies, "ID", "MovieName", seen.MovieID);
            return View(seen);
        }

        // GET: Seens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seen seen = db.Seens.Find(id);
            if (seen == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieID = new SelectList(db.Movies, "ID", "MovieName", seen.MovieID);
            return View(seen);
        }

        // POST: Seens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MovieID,DateSeen,Score")] Seen seen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieID = new SelectList(db.Movies, "ID", "MovieName", seen.MovieID);
            return View(seen);
        }

        // GET: Seens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seen seen = db.Seens.Find(id);
            if (seen == null)
            {
                return HttpNotFound();
            }
            return View(seen);
        }

        // POST: Seens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seen seen = db.Seens.Find(id);
            db.Seens.Remove(seen);
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
