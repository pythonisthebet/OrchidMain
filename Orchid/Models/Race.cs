using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Race
{
    public int Id { get; set; }
    public string RaceName { get; set; } = null!;
    public string SubraceName { get; set; } = null!;

    public Race(int id, string raceName)
    {
        Id = id;
        RaceName = raceName;
        SubraceName = "";
    }

}
