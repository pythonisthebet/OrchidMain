using System.Collections.Generic;

namespace Orchid.Models
{
    public class CharacterData
    {
        public JsonCharacter character { get; set; }
        public int Uid { get; set; }
        public int Cid { get; set; }
    }

    public class JsonCharacter
    {
        public List<string> Classes { get; set; } = new List<string>();
        public List<string> ClassLevels { get; set; } = new List<string>();
        public List<string> Spells { get; set; } = new List<string>();
        public List<string> equipment { get; set; } = new List<string>();
        public List<Score> Scores { get; set; } = new List<Score>();
    }

    public class Score
    {
        public int Key { get; set; }
        public int Value { get; set; }
    }
}
