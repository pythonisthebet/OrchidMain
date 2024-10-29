using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Orchid.Models;

public class Spell
{
    public int Id { get; set; }

    public string SName { get; set; } = null!;

    public int SLevel { get; set; }

    public string SDescription { get; set; } = null!;

    public bool IsOfficial { get; set; }

}
