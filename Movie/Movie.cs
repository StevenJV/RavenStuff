using System;
using System.Collections.Generic;
using System.ComponentModel;
// ReSharper disable once CheckNamespace
namespace RavenStuff.Things
{
  public class Movie
  {
    private string _id;
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

    private string CreateId(string name, string year = "") {
      const int maxLength = 1023;
      _id = name.Replace(" ", string.Empty) + year;
      _id = _id.Replace("\\", string.Empty);
      _id = _id.Replace("'", string.Empty);
      _id = _id.Replace(".", string.Empty);
      _id = _id.Replace(",", string.Empty);
      return _id.Length <= maxLength ? _id : _id.Substring(0, maxLength);
    }

    public void DumpToConsole() {
      foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this)) {
        string name = descriptor.Name;
        object value = descriptor.GetValue(this);
        Console.WriteLine("{0}={1}", name, value);
      }
    }

    public List<string> DumpToList() {
      List<string> propList = new List<string>();
      foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this)) {
        string name = descriptor.Name;
        object value = descriptor.GetValue(this);
        propList.Add(string.Format("{0}={1}", name, value));
      }
      return propList;
    }

    public string HtmlRow() {
      string output = "<a href=\"/Movie/Details/" + Id + "\">" + Title + "</a>, " + ReleaseYear;
      return output;
    }



  }
}
