using System;
using System.Collections.Generic;
// ReSharper disable once CheckNamespace
namespace RavenStuff.Things
{
  public class Actor : Things.RavenDocument
  {
    public string Id {
      get {
        return CreateId(Name);
      }
      set { _id = value; }
    }
    public string Name { get; set; }
    public Dictionary<string, string> MovieList { get; set; } // title, character 
    public DateTime BirthDate { get; set; }


    private List<string> _htmlMovieList;
    public List<string> HtmlMovieList {
      get { return GethHtmlMovieList(); }
      set { _htmlMovieList = value; }
    }

    private List<string> GethHtmlMovieList() {
      List<string> htmlMovieList = new List<string>();
      if (null != MovieList) {
        foreach (KeyValuePair<string, string> movie in MovieList) {
          var htmlLine = "<td><a href=\"/movie/details/" + CreateId(movie.Key) + "\">" + movie.Key + "</a></td><td>"+movie.Value+"</td>";
          htmlMovieList.Add(htmlLine);
        }
      }
      return htmlMovieList;
    }

    public void EnsureMovieExists(string movieName, string movieYear) {
      if (MovieList == null) MovieList = new Dictionary<string, string>();
      if (!MovieList.ContainsKey(movieName)) MovieList.Add(movieName, movieYear);
    }

    public string HtmlRow() {
      string output = "<a href=\"/Actor/Details/" + Id + "\">" + Name + "</a>";
      return output;
    }
  }
}
