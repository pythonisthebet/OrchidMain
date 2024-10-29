using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Orchid.Models;

public class Feat
{
    public int Id { get; set; }

    public string FName { get; set; } = null!;

    public string FDescription { get; set; } = null!;

    public bool IsOfficial { get; set; }


}
