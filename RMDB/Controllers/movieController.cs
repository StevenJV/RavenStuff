using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using Raven.Client;
using Raven.Client.Document;
using RavenStuff.Things;

namespace RMDB.Controllers
{
  public class MovieController : Controller
  {

    //
    // GET: /movie/ - returns a list of movies
    public ActionResult Index() {
      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        documentStore.Initialize();
        using (IDocumentSession session = documentStore.OpenSession()) {
          var movieList = new List<string>();
          var movies = session.Advanced.LuceneQuery<Movie>().OrderBy("Title").ToList();
          movies.ForEach(movie => movieList.Add(movie.HtmlRow()));
          ViewBag.data = movieList;
          return View();
        }
      }
    }


    //
    // GET: /movie/details/StarWars1977 - returns details of specific movie
    public ActionResult Details(string id) {
      if (id.IsNullOrWhiteSpace() || id.IsEmpty()) { return RedirectToAction("Index", "Movie"); }
      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        documentStore.Initialize();
        using (IDocumentSession session = documentStore.OpenSession()) {
          var movieInfo = session.Load<Movie>(id);
          if (null == movieInfo) { return RedirectToAction("Index", "Movie"); }
          ViewBag.data = movieInfo;
          return View();
        }
      }
    }



  }
}