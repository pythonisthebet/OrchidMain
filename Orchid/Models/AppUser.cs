using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public bool IsPremium { get; set; }

        public bool IsAdmin { get; set; }

        public virtual List<ChList> ChList { get; set; } = new List<ChList>();

    }
}
