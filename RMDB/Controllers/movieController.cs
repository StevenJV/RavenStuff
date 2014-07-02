using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
          var movies = session.Advanced.LuceneQuery<Movie>().ToList();
          movies.ForEach(movie => movieList.Add(movie.HtmlRow()));
          ViewBag.data = movieList;
          return View();
        }
      }
    }


    //
    // GET: /movie/details/StarWars1977 - returns details of specific movie
    public ActionResult Details(string id) {

      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        documentStore.Initialize();
        if (null != id)
          using (IDocumentSession session = documentStore.OpenSession()) {
            var movieInfo = session.Load<Movie>(id);
            ViewBag.data = movieInfo;
          }
        return View();
      }
    }



  }
}