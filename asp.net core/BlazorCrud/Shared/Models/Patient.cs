using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorCrud.Shared.Models
{
   public class Patient
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public float age { get; set; }
        public string gender { get; set; }
    }
}
