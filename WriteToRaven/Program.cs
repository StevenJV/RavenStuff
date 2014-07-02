using System;
using System.Collections.Generic;
using System.Globalization;
using Raven.Client;
using Raven.Client.Document;
using RavenStuff.Things;

namespace WriteToRaven
{
  class Program
  {
    static void Main(string[] args) {
      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        //CreateMovieDocument(documentStore);
        //CreateActorDocument(documentStore);
      }

    }

    private static void CreateActorDocument(IDocumentStore documentStore)
    {
      var actorInfo = new Actor
      {
        Name = "Angelina Jolie",
        BirthDate = new DateTime(1975, 6, 4)
      };
      documentStore.Initialize();
      using (IDocumentSession session = documentStore.OpenSession())
      {
        session.Store(actorInfo);
        session.SaveChanges();
      }
    }

    private static void CreateMovieDocument(IDocumentStore documentStore)
    {
      var movieInfo = new Movie
      {
        Title = "Ender's Game",
        Quote = "The enemy's gate is down.",
        Director = "Gavin Hood",
        CreatedDate = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
        ReleaseYear = "2013",
        ActorList = new Dictionary<string, string>
        {
          {"Harrison Ford", "Colnel Graff"},
          {"Asa Butterfield", "Ender Wiggin"},
          {"Hailee Steinfield", "Petra Arkanian"},
          {"Abigail Breslin", "Valentine Wiggin"},
          {"Ben Kingsley", "Mazer Rackham"}
        }
      };
      documentStore.Initialize();
      using (IDocumentSession session = documentStore.OpenSession())
      {
        session.Store(movieInfo);
        session.SaveChanges();
      }
    }
  }
}
