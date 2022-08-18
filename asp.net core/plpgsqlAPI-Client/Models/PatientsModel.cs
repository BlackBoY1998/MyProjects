using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plpgsqlAPI_Client.Models
{
    public class PatientsModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
    }
}
