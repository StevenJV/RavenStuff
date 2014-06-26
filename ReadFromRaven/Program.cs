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
          //  Article articleInfo = session.Query<Article>()
          //    .Where(a => a.Title == "Blade Runner")
          //    .First<Article>();

          //all
          var articles = session.Advanced.LuceneQuery<Movie>().ToList();
          articles.ForEach(article => article.DumpToConsole());

          Console.ReadKey();

        }
      }
    }
  }
}
