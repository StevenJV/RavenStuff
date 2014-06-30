using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using RavenStuff.Things;
using System.Web.Mvc;

namespace RMDB.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index() {

      {
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