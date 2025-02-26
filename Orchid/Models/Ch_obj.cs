using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models
{
    public class Ch_obj_db
    {
        public int Id { get; set; }
        public string CharacterName { get; set; } = null!;
        public int? UserId { get; set; }
        public int LevelValue { get; set; }
        public string SubclassName { get; set; } = null!;
        public string? ImgId { get; set; }
    }

    public class Ch_obj_json
    {
        public int Id { get; set; }
        public Race_obj race_Obj { get; set; }
        public Stats stats { get; set; }
        public Saves saves { get; set; }
        public Class_obj[] class_Obj { get; set; }
        public Proficiency_obj[] proficiencies { get; set; }
        //public Proficiencies_equip[] proficiencies_equip { get; set; }
        //public Proficiencies_language[] proficiencies_languages { get; set; }
        public string[] proficiencies_equip {  get; set; }
        public string[] proficiencies_languages { get; set; }
        public Feat_obj[] feat_obj { get; set; }
        //public Background background { get; set; }
        //public Tags[] tag { get; set; }
        public string background { get; set; }
        public string[] tag { get; set; }

    }

    public class Race_obj
    {
        public string name { get; set; }
        public string subrace_name { get; set; }
        public string statboostbig { get; set; }
        public string statboostsmall { get; set; }
        public string statboost3 { get; set; }
    }
    public class Stats
    {
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Int { get; set; }
        public int Wis { get; set; }
        public int Cha { get; set; }

    }
    public class Saves
    {
        public bool Str { get; set; }
        public bool Dex { get; set; }
        public bool Con { get; set; }
        public bool Int { get; set; }
        public bool Wis { get; set; }
        public bool Cha { get; set; }

    }
    public class Class_obj
    {
        public string name {  get; set; }
        public string subclass { get; set; }
        public int level { get; set; }
    }

    public class Proficiency_obj
    {
        public string Name { get; set; }

        public bool Expert { get; set; }
    }
    public class Proficiencies_equip
    {
        public string Name { get; set; }
    }

    public class Proficiencies_language
    {
        public string Name { get; set; }
    }

    public class Feat_obj
    {
        public string feats { get; set; }
        public int level_taken {  get; set; }
    }

    public class Background
    {
        public string name { get; set; }
    }

    public class Tags
    {
        public string tag_name { get; set; }
    }

}
