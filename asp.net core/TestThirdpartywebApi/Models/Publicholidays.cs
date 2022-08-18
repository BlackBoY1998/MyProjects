using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestThirdpartywebApi.Models
{
    public class Publicholidays
    {
    public string Name { get; set; }
    public string LocalName { get; set; }
    public DateTime? Date { get; set; }
    public string CountryCode { get; set; }
    public bool Global { get; set; }
    public string type { get; set; }
    public string Countries { get; set; }
    public bool Fixed { get; set; }

    }
}