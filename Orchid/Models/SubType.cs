using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models
{
    public class SubType
    {
        public string SubTypeName { get; set; }
        public string SelectionMode { get; set; }

        public IEnumerable<string> Options { get; set; }

        public SubType(string subTypeName, string selectionMode, IEnumerable<string> options)
        {
            SubTypeName = subTypeName;
            SelectionMode = selectionMode;
            Options = options;
        }
    }
}
