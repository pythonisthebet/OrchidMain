using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Orchid.Models;

public class Race
{
    public int Id { get; set; }

    public string RName { get; set; } = null!;

    public string RDescription { get; set; } = null!;

    public bool IsOfficial { get; set; }


}
