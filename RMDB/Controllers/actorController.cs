﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Xml.Linq;
using Microsoft.Ajax.Utilities;
using Raven.Client;
using Raven.Client.Document;
using RavenStuff.Things;

namespace RMDB.Controllers
{
  public class ActorController : Controller
  {
    //
    // GET: /actor/ - return a list of actors
    public ActionResult Index()
        {
          using (IDocumentStore documentStore = new DocumentStore() {ConnectionStringName = "MyRavenConStr"})
          {
            documentStore.Initialize();
            using (IDocumentSession session = documentStore.OpenSession())
            {
              var actorList = new List<string>();
              var actors = session.Advanced.LuceneQuery<Actor>().OrderBy("Name").ToList();
              actors.ForEach(actor => actorList.Add(actor.HtmlRow()));
              ViewBag.data = actorList;
              return View();
            }
          }
        }

    // GET: /actor/details/CarrieFisher - returns details of a specific actor
    public ActionResult Details(string id)
    {
      if (id.IsNullOrWhiteSpace() || id.IsEmpty()) { return RedirectToAction("Index", "Actor"); }
      using (IDocumentStore documentStore = new DocumentStore() {ConnectionStringName = "MyRavenConStr"}) {
        documentStore.Initialize();
        using (IDocumentSession session = documentStore.OpenSession()) {
          var actorInfo = session.Load<Actor>(id);
          if (null == actorInfo) { return RedirectToAction("Index", "Actor"); }
          ViewBag.data = actorInfo;
          List<string> aListOfMovies = actorInfo.HtmlMovies();
          ViewBag.movieList = aListOfMovies;
          return View();
        }
      } 
    }

  }
}