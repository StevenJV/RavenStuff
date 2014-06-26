using System;
using Raven.Client;
using Raven.Client.Document;
using RavenStuff.Things;

namespace WriteToRaven
{
  class Program
  {
    static void Main(string[] args) {
      using (IDocumentStore documentStore = new DocumentStore() { ConnectionStringName = "MyRavenConStr" }) {
        //write to the database
        var ravenIntro = new Article() {
          Title = "Blade Runner",
          Quote = "'More human than human' is our motto.",
          Director = "Ridley Scott",
          CreatedDate = DateTime.UtcNow.ToString(),
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
  }
}
