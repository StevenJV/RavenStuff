using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Document;

namespace EditMovie
{
  class Program
  {
    static void Main(string[] args) {
      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        documentStore.Initialize();
        using (IDocumentSession session = documentStore.OpenSession()) {
          Movie movie = session.Load<Movie>("BladeRunner1982");
          movie.DumpToConsole();
          //movie.Director = "Rob Reiner";
          //add a list of actors to the existing movie
          movie.ActorList = new Dictionary<string, string>
          {
            {"Rutger Hauer", "Roy Batty"},
            {"Harrison Ford", "Rick Deckard"},
            {"Sean Young", "Rachel"},
            {"Edward James Olmos", "Gaff"},
            {"M. Emmet Walsh", "Bryant"},
            {"Daryl Hannah","Pris"},
            {"Morgan Paull","Holden"},
            {"Joe Turkel","Dr. Eldon Tyell"}
          };
          session.SaveChanges();
          movie = session.Load<Movie>("BladeRunner1982");
          movie.DumpToConsole();
          Console.ReadKey();

        }
      }
    }
  }
}
