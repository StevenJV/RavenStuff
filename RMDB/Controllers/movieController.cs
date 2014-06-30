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
    // GET: /movie/ or /movie/StarWars1977
    public ActionResult Index(string id) {
      if (id.IsNullOrWhiteSpace()) {
        using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
          documentStore.Initialize();
          var movieList = new List<string>();
          using (IDocumentSession session = documentStore.OpenSession()) {
            var movies = session.Advanced.LuceneQuery<Movie>().ToList();
            movies.ForEach(movie => movieList.Add(movie.HtmlRow()));
            ViewBag.data = movieList;
            return View();
          }
        }
      }
      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        documentStore.Initialize();
        using (IDocumentSession session = documentStore.OpenSession()) {
          var movieInfo = session.Load<Movie>(id);
          ViewBag.data = movieInfo.DumpToList();
          return View();
        }
      }
    }
  }
}