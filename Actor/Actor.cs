using System;
using System.Collections.Generic;
using System.ComponentModel;
// ReSharper disable once CheckNamespace
namespace RavenStuff.Things
{
    public class Actor
    {
      private string _id;
      public string Id {
        get {
          const int maxLength = 1023;
          _id = this.Name.Replace(" ", string.Empty);
          _id = _id.Replace("\\", string.Empty);
          _id = _id.Replace("'", string.Empty);
          return _id.Length <= maxLength ? _id : _id.Substring(0, maxLength);
        }
        set { this._id = value; }
      }
      public string Name { get; set; }
      public Dictionary<string, string> MovieList { get; set; } // title, year 
      public DateTime BirthDate { get; set; }

      public void EnsureMovieExists(string movieName, string movieYear) {
        if (MovieList == null) MovieList = new Dictionary<string, string>();
        if (!MovieList.ContainsKey(movieName)) MovieList.Add(movieName, movieYear);
      }
    }
}
