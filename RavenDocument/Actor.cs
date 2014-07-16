using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace RavenStuff.Things
{
  public class Actor : RavenStuff.Things.RavenDocument
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


    public List<string> HtmlMovies() {
      List<string> thisList = new List<string>();
      foreach (KeyValuePair<string, string> movie in MovieList) {
        thisList.Add(HtmlMovieRow(movie.Key, movie.Value));
      }
      return thisList;
    }

    private string HtmlMovieRow(string title, string character) {
      return "<td><a href=\"/movie/details/" + CreateId(title) + "\">" + title + "</a></td><td>" + character + "</td>";
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
