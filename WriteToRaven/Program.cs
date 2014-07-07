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
        Title = "Star Trek: Generations",
        Quote = "I take it the odds are against us and the situation is grim?",
        Director = "David Carson",
        CreatedDate = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
        ReleaseYear = "1994",
        ActorList = new Dictionary<string, string>
        {
          {"Patrick Stewart", "Picard"},
          {"Johnathan Frakes", "Riker"},
          {"Brent Spiner", "Data"},
          {"LeVar Burton", "Geordi"},
          {"Michael Dorn", "Worf"},
          {"Gates McFadden", "Beverly"},
          {"Marina Sirtis", "Troi"},
          {"Malcolm McDowell", "Soran"},
          {"James Doohan", "Scotty"},
          {"Walter Koenig", "Chekov"},
          {"William Shatner", "Kirk"},
          {"Alan Ruck", "Capt. Harriman"},
          {"Jackqueline Kim", "Demora"}
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
