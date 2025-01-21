using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Party
{
    public int Id { get; set; }

    public int? DmId { get; set; }

    public string Pname { get; set; } = null!;

    public string? CurrentCampain { get; set; }

    public virtual ICollection<AppUser> AppUsers { get; set; } = new List<AppUser>();

    public Party() { }

}
