using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Document;
using RavenStuff.Things;

namespace EditMovie
{
  class Program
  {
    static void Main(string[] args) {
      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        documentStore.Initialize();
        using (IDocumentSession session = documentStore.OpenSession()) {
          Movie movie = session.Load<Movie>("TestTitle1982");
          movie.DumpToConsole();

          var newYear = int.Parse(movie.ReleaseYear) + 1;
          movie.ReleaseYear = newYear.ToString(CultureInfo.InvariantCulture);
          session.SaveChanges();
          movie = session.Load<Movie>("TestTitle1982");
          movie.DumpToConsole();
          Console.ReadKey();

        }
      }
    }
  }
}
