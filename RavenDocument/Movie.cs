using System.Collections.Generic;

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

    private List<string> _htmlActorList;
    public List<string> HtmlActorList {
      get { return GethHtmlActorList(); }
      set { _htmlActorList = value; }
    }

    private List<string> GethHtmlActorList() {
      List<string> htmlActorList = new List<string>();
      if (null != ActorList) {
        foreach (KeyValuePair<string, string> actor in ActorList) {
          var htmlLine = "<td><a href=\"/actor/details/" + CreateId(actor.Key) + "\">" + actor.Key + "</a></td><td>" + actor.Value+"</td>";
          htmlActorList.Add(htmlLine);
        }
      }
      return htmlActorList;
    }


    public string HtmlRow() {
      string output = "<a href=\"/Movie/Details/" + Id + "\">" + Title + "</a>, " + ReleaseYear;
      return output;
    }



  }
}
