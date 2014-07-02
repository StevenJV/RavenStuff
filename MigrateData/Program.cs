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

    //TODO: move this to the Movie class? 
    private static string CreateMovieId(string title, string year) {
      const int maxLength = 1023;
      var ID = title.Replace(" ", string.Empty) + year;
      ID = ID.Replace("\\", string.Empty);
      return ID.Length <= maxLength ? ID : ID.Substring(0, maxLength);
    }
  }
}
