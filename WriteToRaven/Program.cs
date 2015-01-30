using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using RavenStuff.Things;


namespace WriteToRaven
{
  class Program
  {
    static void Main(string[] args) {
      var happyFeetMovie = new Movie {
        Title = "Happy Feet",
        Quote = "Don't push me 'cause I am close to the edge. I'm trying not to lose my head.",
        Director = "George Miller",
        CreatedDate = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
        ReleaseYear = "2006",
        ActorList = new Dictionary<string, string>
        {
          {"Carlos Alazraui", "Nestor"},
          {"Lombardo Byar", "Raul"},
          {"Robin Williams", "Ramon"},
          {"Famke Janssen", "Memphis"}
        }
      };

      var museum2Movie = new Movie {
        Title = "Night at the Museum: Secret of the Tomb",
        Quote = "Smile, my boy. It's sunrise.",
        Director = "Shawn Levy",
        CreatedDate = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
        ReleaseYear = "2014",
        ActorList = new Dictionary<string, string>
        {
          {"Ben Stiller","Larry Daley"},
          {"Robin Williams", "Teddy Roosevelt"},
          {"Owen Wilson", "Jedediah"},
          {"Steve Coogan","Octavius"}
        }
      };

      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        CreateMovieDocument(documentStore, happyFeetMovie);
        CreateMovieDocument(documentStore, museum2Movie);
        CreateActorsFromMovies();
      }
    }

    private static void CreateMovieDocument(IDocumentStore documentStore, Movie movie) {
      var movieInfo = movie;
      documentStore.Initialize();
      using (IDocumentSession session = documentStore.OpenSession()) {
        Console.WriteLine("creating record for " + movieInfo.Title);
        session.Store(movieInfo);
        foreach (KeyValuePair<string, string> actor in movieInfo.ActorList) {
          var actorInfo = new Actor {
            Name = actor.Key,
            MovieList = new Dictionary<string, string>()
            {
              { movieInfo.Title + ", "+movieInfo.ReleaseYear, actor.Value} 
            }
          };
          session.Store(actorInfo);
        }
        session.SaveChanges();
      }
    }

    private static void CreateActorsFromMovies() {
      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        documentStore.Initialize();
        using (IDocumentSession movieSession = documentStore.OpenSession()) {
          var movies = movieSession.Advanced.LuceneQuery<Movie>().ToList();

          movies.ForEach(movie => {
            Console.WriteLine("----> " + movie.Title);
            using (IDocumentSession actorSession = documentStore.OpenSession()) {
              Dictionary<string, string> actorList = movie.ActorList;
              foreach (KeyValuePair<string, string> actor in actorList) {
                Actor actorInfo = LoadOrCreateActor(actorSession, actor.Key);
                actorInfo.EnsureMovieExists(movie.Title + ", " + movie.ReleaseYear, actor.Value);
              }
              actorSession.SaveChanges();
            }
          });
          movieSession.SaveChanges();

        }
      }

    }

    private static Actor LoadOrCreateActor(IDocumentSession session, string actorName) {
      var actor = session.Load<Actor>(ActorId(actorName));
      if (actor != null) {
        Console.WriteLine("a document for " + actorName + " exists already.");
        return actor;
      }
      var pActor = new Actor { Name = actorName };
      session.Store(pActor);
      session.SaveChanges();
      Console.WriteLine(actorName + " created.");
      return pActor;
    }

    private static string ActorId(string actorName) {
      //TODO find a way to use the Actor object type's id field instead 
      const int maxLength = 1023;
      var id = actorName.Replace(" ", string.Empty);
      id = id.Replace("\\", string.Empty);
      id = id.Replace("'", string.Empty);
      id = id.Replace(".", string.Empty);
      id = id.Replace(":", string.Empty);
      return id.Length <= maxLength ? id : id.Substring(0, maxLength);
    }


  }
}
