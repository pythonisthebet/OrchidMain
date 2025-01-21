using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Feat
{
    public int Id { get; set; }
    public string FeatName { get; set; } = null!;
    public int LevelTaken { get; set; }

    public Feat() { }

}
