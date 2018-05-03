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
    public class ReviewsController : Controller
    {
        private MyMovieListContext db = new MyMovieListContext();

        // GET: Reviews
        public ActionResult Index(string filter,string sortBy)
        {
            Review model = new Review();
            model.CheckBoxGenre = new List<EnumModel>();
            foreach (Genre genre in Enum.GetValues(typeof(Genre)))
            {
                model.CheckBoxGenre.Add(new EnumModel() { Genre = genre, IsSelected = false });
            }

            var reviews = db.Reviews.Include(r => r.Movie);

            if (!String.IsNullOrEmpty(filter))
            {
                Genre genre = (Genre)Enum.Parse(typeof(Genre), filter);
                reviews = reviews.Where(p => p.FavoriteGenre == genre);
                ViewBag.FilterSearch = filter;
            }
            var Genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().OrderBy(x => x.ToString());
            ViewBag.Genre = new SelectList(Genres);

            ViewBag.SortBy = sortBy;

            switch (sortBy)
            {
                case "Name":
                    reviews = reviews.OrderBy(p => p.Movie.MovieName);
                    break;
                case "L_Score":
                    reviews = reviews.OrderBy(p => p.Score);
                    break;
                case "H_Score":
                    reviews = reviews.OrderByDescending(p => p.Score);
                    break;
                default:
                    break;
            }
            ViewBag.Sorts = new Dictionary<string, string>
            {
                {"Movie Title", "Name" },
                {"Lowest to Highest Rating", "L_Score" },
                {"Highest to Lowest Rating", "H_Score" }
            };

            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.MovieID = new SelectList(db.Movies, "ID", "MovieName");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MovieID,ReviewName,Score,ReviewContent,FavoriteGenre")] Review review)
        {
            if (ModelState.IsValid)
            {
                Review matchingReview = db.Reviews.Where(cm => string.Compare(cm.MovieID.ToString(), review.MovieID.ToString(), true) == 0).FirstOrDefault();
                if (review == null)
                {
                    return HttpNotFound();
                }
                if (matchingReview != null)
                {
                    ModelState.AddModelError("Movie Name", "Movie name cannot be reviewed twice");
                    return View(review);
                }

                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieID = new SelectList(db.Movies, "ID", "MovieName", review.MovieID);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieID = new SelectList(db.Movies, "ID", "MovieName", review.MovieID);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MovieID,ReviewName,Score,ReviewContent,FavoriteGenre")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieID = new SelectList(db.Movies, "ID", "MovieName", review.MovieID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
