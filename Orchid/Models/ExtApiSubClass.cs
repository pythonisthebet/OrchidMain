using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models
{
    //parsed from jsonfrom external api https://www.dnd5eapi.co/api/

    public class ExtApiSubClassRoot
    {
        public int count { get; set; }
        public SubClassResult[] results { get; set; }
    }

    public class SubClassResult
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }


    public class ExtApiSubClassDesc
    {
        public string index { get; set; }
        public Class1 _class { get; set; }
        public string name { get; set; }
        public string subclass_flavor { get; set; }
        public string[] desc { get; set; }
        public string subclass_levels { get; set; }
        public string url { get; set; }
        public object[] spells { get; set; }
    }


}
