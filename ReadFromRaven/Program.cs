using System;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using RavenStuff.Things;

namespace ReadFromRaven
{
  class Program
  {
    static void Main(string[] args) {
      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        //read from the database
        documentStore.Initialize();
        using (IDocumentSession session = documentStore.OpenSession()) {
          //just one, by name
          Movie movieInfo = session.Query<Movie>().FirstOrDefault(a => a.Title == "BladeRunner");
          if (movieInfo != null) movieInfo.DumpToConsole();

          //all
          //var movies = session.Advanced.LuceneQuery<Movie>().ToList();
          //movies.ForEach(article => article.DumpToConsole());

          Console.ReadKey();

        }
      }
    }
  }
}
