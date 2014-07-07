using System;
using System.Collections.Generic;
using Raven.Client;
using Raven.Client.Document;
using System.Linq;
using RavenStuff.Things;

namespace ActorsFromMovies
{
  internal class Program
  {
    private static void Main(string[] args) {

      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        documentStore.Initialize();
        using (IDocumentSession movieSession = documentStore.OpenSession()) {
          var movies = movieSession.Advanced.LuceneQuery<Movie>().ToList();
          movies.ForEach(movie => {
            Console.WriteLine("movie: "+ movie.Title);
                                    using (IDocumentSession actorSession = documentStore.OpenSession())
                                    {
                                      Dictionary<string, string> actorList = movie.ActorList;
                                      foreach (KeyValuePair<string, string> actor in actorList)
                                      {
                                        Actor actorInfo = LoadOrCreate(actorSession, actor.Key);
                                        actorInfo.EnsureMovieExists(movie.Title+", "+movie.ReleaseYear, actor.Value);
                                      }
                                      actorSession.SaveChanges();
                                    }
          });
          movieSession.SaveChanges();
        }
      }
    }

    private static Actor LoadOrCreate(IDocumentSession session, string actorName) {
      var actor = session.Load<Actor>(ActorId(actorName));
      if (actor != null)
      {
        Console.WriteLine(actorName + " exists.");
        return actor;
      }
      var pActor = new Actor { Name = actorName };
      session.Store(pActor);
      session.SaveChanges();
      Console.WriteLine(actorName + " created.");
      return pActor;
    }

    private static string ActorId(string actorName) {
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
