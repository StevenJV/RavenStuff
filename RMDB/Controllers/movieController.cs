using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Document;
using RavenStuff.Things;

namespace RMDB.Controllers
{
  public class MovieController : Controller
  {
    //
    // GET: /movie/
    public ActionResult Index() {
      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        documentStore.Initialize();
        List<string> movieList = new List<string>();
        using (IDocumentSession session = documentStore.OpenSession()) {
          var movies = session.Advanced.LuceneQuery<Movie>().ToList();
          movies.ForEach(movie => movieList.Add(movie.HtmlRow()));
          ViewBag.data = movieList;
          return View();
        }
      }
    }
  }
}