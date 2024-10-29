using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Equipment
{
    public int Id { get; set; }

    public int? TypeId { get; set; }

    public int? RarityId { get; set; }

    public string EDescription { get; set; } = null!;

    public bool IsOfficial { get; set; }


}
