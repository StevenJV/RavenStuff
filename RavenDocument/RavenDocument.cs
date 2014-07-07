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
    }
}
