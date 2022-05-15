using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoAppSkeleton.Models
{
   public class Location
   {
      public Location() { }

      public Location(string name)
      {
         this.Name = name;
         this.ID = "050-" + DateTime.Now.Year + "-" + Name.PadLeft(4, '0');
      }

      public string ID { get; set; }
      public string Name { get; set; }
   }
}
