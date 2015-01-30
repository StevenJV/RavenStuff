using System;
using System.Collections.Generic;
using System.Linq;

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


    public List<string> HtmlMovies()
    {
      if (MovieList != null)
      {
        return MovieList.Select(movie => HtmlMovieRow(movie.Key, movie.Value)).ToList();
      }
      else return null;
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
