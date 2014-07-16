using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace RavenStuff.Things
{
  public class Movie : RavenStuff.Things.RavenDocument
  {

    public string Id {
      get { return CreateId(Title, ReleaseYear); }
      set { _id = value; }
    }
    public string Title { get; set; }
    public string ReleaseYear { get; set; }
    public string Quote { get; set; }
    public string CreatedDate { get; set; }
    public string Director { get; set; }
    public Dictionary<string, string> ActorList { get; set; } // actor name, character name

    public List<string> HtmlActors()
    {
      List<string> thisList = new List<string>();
      foreach (KeyValuePair<string, string> actor in ActorList) {
        thisList.Add(HtmlActorRow(actor.Key, actor.Value));
      }
      return thisList;
    }

    public string HtmlActorRow(string actorName, string characterName) {
      var htmlActorLine = "<td><a href=\"/actor/details/" +
        CreateId(actorName) +
        "\">" +
        actorName +
        "</a></td><td>" +
        characterName +
        "</td>";
      return htmlActorLine;
    }

    public string HtmlRow() {
      string output = "<a href=\"/Movie/Details/" + Id + "\">" + Title + "</a>, " + ReleaseYear;
      return output;
    }



  }
}
