using System;
using System.Collections.Generic;
using System.ComponentModel;
// ReSharper disable once CheckNamespace
namespace RavenStuff.Things
{
  public class Movie
  {
    public string Id { get; set; }
    public string Quote { get; set; }
    public string CreatedDate { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public string ReleaseYear { get; set; }

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
      string output = "<a href=\"" + this.Id + "\">" + this.Title + "</a>, " + this.ReleaseYear;
      return output;
    }
  }
}
