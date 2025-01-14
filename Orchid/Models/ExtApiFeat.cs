using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models
{
    //parsed from jsonfrom external api https://www.dnd5eapi.co/api/

    public class FeatRootobject
    {
        public int count { get; set; }
        public FeatRootResults[] results { get; set; }
    }

    public class FeatRootResults
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class ExtApifeat
    {
        public string index { get; set; }
        public string name { get; set; }
        public Prerequisite[] prerequisites { get; set; }
        public string[] desc { get; set; }
        public string url { get; set; }
    }

    public class Feat_Prerequisite
    {
        public Ability_Score ability_score { get; set; }
        public int minimum_score { get; set; }
    }

    public class Feat_Ability_Score
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }


}
