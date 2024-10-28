using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models
{
    public class ChList
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? MainClass { get; set; }

        public int? Race { get; set; }

        public int? Subclass { get; set; }

        public int LevelValue { get; set; }

        public bool IsMultiClass { get; set; }

        public string ChImagePath { get; set; } = "";
    }
}
