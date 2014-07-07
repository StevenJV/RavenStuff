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
        CreateMovieDocument(documentStore);
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
        Title = "Happy Feet",
        Quote = "Don't push me 'cause I am close to the edge. I'm trying not to lose my head.",
        Director = "George Miller",
        CreatedDate = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
        ReleaseYear = "2006",
        ActorList = new Dictionary<string, string>
        {
          {"Carlos Alazraui", "Nestor"},
          {"Lombardo Byar", "Raul"},
          {"Robin WIlliams", "Ramon"},
          {"Famke Janssen", "Memphis"}
        }
      };
      documentStore.Initialize();
      using (IDocumentSession session = documentStore.OpenSession())
      {
        Console.WriteLine("creating record for "+movieInfo.Title);
        session.Store(movieInfo);
        session.SaveChanges();
      }
    }
  }
}
