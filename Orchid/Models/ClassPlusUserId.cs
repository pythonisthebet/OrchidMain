using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models
{
    public class ClassPlusUserId
    {
        public int Id { get; set; }

        public Class name { get; set; }

        public ClassPlusUserId() { }

        public ClassPlusUserId(int id, Class name)
        {
            Id = id;
            this.name = name;
        }
    }


}
