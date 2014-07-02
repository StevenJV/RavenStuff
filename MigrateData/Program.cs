using Raven.Client;
using Raven.Client.Document;
using System.Linq;
using RavenStuff.Things;

namespace MigrateData
{
  class Program
  {
    static void Main(string[] args) {

      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        documentStore.Initialize();

        using (IDocumentSession session = documentStore.OpenSession()) {
          var articles = session.Advanced.LuceneQuery<Article>().ToList();
          articles.ForEach(article => ConvertArticleToMovie(article, session));
          session.SaveChanges();
        }
      }
    }

    private static void ConvertArticleToMovie(Article obj, IDocumentSession session) {
      var movieInfo = new Movie {
        Quote = obj.Quote,
        CreatedDate = obj.CreatedDate,
        Title = obj.Title,
        Director = obj.Director,
        ReleaseYear = obj.ReleaseYear
      };
      session.Store(movieInfo);
    }
  }
}
