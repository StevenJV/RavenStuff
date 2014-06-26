using System;
using System.ComponentModel;

namespace Article
{
  public class Article
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
  }
}
