using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models
{
    public class ChPlusFilters
    {
        public Character Character { get; set; }

        public List<Filter> Filters { get; set; }

        public ChPlusFilters(Character character, List<Filter> LFilters) 
        {
            Character = character;
            Filters = LFilters;
        }

    }
}
