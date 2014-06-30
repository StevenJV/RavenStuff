using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using RavenStuff.Things;

namespace IRDB.Controllers
{
  public class HomeController : RavenController
  {
    public HomeController(IDocumentStore store) : base(store)
    {
      
    }
    public HomeController() {

    }

    public ActionResult Index() {
      return View(Session.Query<Movie>().ToList());
    }

    public ViewResult Details() {
      return View(Session.Query<Movie>().ToList());
    }
    public ViewResult Details(string id) {
      Movie movie = Session.Load<Movie>(id);
      return View(movie);
    }

    //public ActionResult Edit(string id)
    //{
    //  Movie movie = Session.Load<Movie>(id);
    //  return View(movie);
    //}
    //[HttpPost]
    //public ActionResult Edit(Movie movie) {
    //  if (ModelState.IsValid) {
    //    Movie currentMovie = Session.Load<Movie>(movie.Id);
    //    currentMovie.Title = movie.Title;
    //    currentMovie.Quote = movie.Quote;
    //    currentMovie.Director = movie.Director;
    //    currentMovie.ReleaseYear = movie.ReleaseYear;
    //    Session.Store(currentMovie);
    //    return RedirectToAction("Index");
    //  }
    //  return View(movie);
    //}

    public ActionResult About() {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact() {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}