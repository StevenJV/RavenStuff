using System;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;

namespace ReadFromRaven
{
  class Program
  {
    static void Main(string[] args) {
      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        //read from the database
        documentStore.Initialize();
        using (IDocumentSession session = documentStore.OpenSession()) {
          global::Article.Article articleInfo = session.Query<global::Article.Article>()
            .Where(a => a.Title == "Blade Runner")
            .First<global::Article.Article>();
          articleInfo.DumpToConsole();
          Console.ReadKey();
        }
      }

    }
  }
}
