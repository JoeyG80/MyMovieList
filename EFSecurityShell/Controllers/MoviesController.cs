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
    public class MoviesController : Controller
    {
        private MyMovieListContext db = new MyMovieListContext();

        // GET: Movies
        public ActionResult Index(string search, string filter, string sortBy)
        {
            var Movies = from s in db.Movies
                           select s;

            if (!String.IsNullOrEmpty(search))
            {
                Movies = Movies.Where(p => p.MovieName.Contains(search) ||
                p.Director.Contains(search));
                ViewBag.Search = search;
            }

            if (!String.IsNullOrEmpty(filter))
            {
                Genre genre = (Genre)Enum.Parse(typeof(Genre), filter);
                Movies = Movies.Where(p => p.Genre == genre);
                ViewBag.FilterSearch = filter;
            }

            //List<Genre> Genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().OrderBy(x => x.ToString()).ToList();
             var Genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().OrderBy(x => x.ToString());
            //Movies = Movies.OrderBy(x => (int)(x.Genre));
            //ViewBag.Genre = new SelectList(Sorted);
            //var categories = Movies.OrderBy(p => p.MovieName).Select(p => p.MovieName).Distinct();
            ViewBag.Genre = new SelectList(Genres);

            ViewBag.SortBy = sortBy;

            switch (sortBy)
            {
                case "Oldest":
                    Movies = Movies.OrderBy(p => p.DateReleased);
                    break;
                case "Latest":
                    Movies = Movies.OrderByDescending(p => p.DateReleased);
                    break;
                default:
                    break;
            }
            ViewBag.Sorts = new Dictionary<string, string>
            {
                {"Oldest date", "Oldest" },
                {"Latest date", "Latest" }
            };

            return View(Movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MovieName,Director,DateReleased,Genre,Summary")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MovieName,Director,DateReleased,Genre,Summary")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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
