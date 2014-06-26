using System;
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
        documentStore.Conventions.RegisterIdConvention<Movie>(
       (dbname, commands, movie) => CreateMovieId(movie.Title, movie.ReleaseYear));
        //write to the database
        var ravenIntro = new Movie() {
          Title = "Test Title",
          Quote = "we don't need no stinkin' quotes",
          Director = "Steven",
          CreatedDate = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
          ReleaseYear = "1982"
        };
        documentStore.Initialize();
        using (IDocumentSession session = documentStore.OpenSession())
        {
          session.Store(ravenIntro);
          session.SaveChanges();
        }
        
      }

    }

    //TODO: move this to the Movie class? 
    private static string CreateMovieId(string title, string year) {
      const int maxLength = 1023;
      var id = title.Replace(" ", string.Empty) + year;
      id = id.Replace("\\", string.Empty);
      return id.Length <= maxLength ? id : id.Substring(0, maxLength);
    }
  }
}
