using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models
{
    public class ExtApiSpellRoot
    {
        public int count { get; set; }
        public ExtApiSpell[] spells { get; set; }
    }

    public class ExtApiSpell
    {
        public string index { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public string url { get; set; }
    }
    public class MyExtApiSpell
    {
        public string index { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public string desc { get; set; }
    }


    public class SpellInfo
    {
        public string index { get; set; }
        public string name { get; set; }
        public string[] desc { get; set; }
        public string[] higher_level { get; set; }
        public string range { get; set; }
        public string[] components { get; set; }
        public string material { get; set; }
        public bool ritual { get; set; }
        public string duration { get; set; }
        public bool concentration { get; set; }
        public string casting_time { get; set; }
        public int level { get; set; }
        public string attack_type { get; set; }
        public Damage damage { get; set; }
        public School school { get; set; }
        public Class1[] classes { get; set; }
        public Subclass[] subclasses { get; set; }
        public string url { get; set; }
    }

    public class Damage
    {
        public Damage_Type damage_type { get; set; }
        public Damage_At_Slot_Level damage_at_slot_level { get; set; }
    }

    public class Damage_Type
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Damage_At_Slot_Level
    {
        public string _2 { get; set; }
        public string _3 { get; set; }
        public string _4 { get; set; }
        public string _5 { get; set; }
        public string _6 { get; set; }
        public string _7 { get; set; }
        public string _8 { get; set; }
        public string _9 { get; set; }
    }

    public class School
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Class1
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Subclass
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

}
