using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RavenStuff.Things
{
  public class RavenDocument
  {
    protected string _id;
    protected string CreateId(string name, string year = "") {
      const int maxLength = 1023;
      _id = name.Replace(" ", string.Empty) + year ;
      _id = _id.Replace("\\", string.Empty);
      _id = _id.Replace("'", string.Empty);
      _id = _id.Replace(".", string.Empty);
      _id = _id.Replace(",", string.Empty);
      _id = _id.Replace(":", string.Empty);
      return _id.Length <= maxLength ? _id : _id.Substring(0, maxLength);
    }

    public void DumpToConsole() {
      foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this)) {
        string name = descriptor.Name;
        var value = descriptor.GetValue(this);
        if (value != null && value.GetType() == typeof(Dictionary<string, string>)) {
          foreach (KeyValuePair<string, string> item in (IEnumerable<KeyValuePair<string, string>>) value) {
            Console.WriteLine(" {0} ({1})", item.Key, item.Value);
          }
        } else {
          Console.WriteLine("{0} = {1}", name, value);
        }
      }
    }

  }
}
