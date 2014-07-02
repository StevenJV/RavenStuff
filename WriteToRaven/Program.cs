using System;
using System.Collections.Generic;
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
        var movieInfo = new Movie
        {
          Title = "Ender's Game",
          Quote = "The enemy's gate is down.",
          Director = "Gavin Hood",
          CreatedDate = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
          ReleaseYear = "2013",
          ActorList = new Dictionary<string, string>
          {
            {"Harrison Ford", "Colnel Graff"},
            {"Asa Butterfield", "Ender Wiggin"},
            {"Hailee Steinfield", "Petra Arkanian"},
            {"Abigail Breslin", "Valentine Wiggin"},
            {"Ben Kingsley", "Mazer Rackham"}
          }
        };
        documentStore.Initialize();
        using (IDocumentSession session = documentStore.OpenSession())
        {
          session.Store(movieInfo);
          session.SaveChanges();
        }
        
      }

    }


  }
}
