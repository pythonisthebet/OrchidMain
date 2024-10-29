using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Orchid.Models;


public class SubClass
{
    public int Id { get; set; }

    public int? ClassId { get; set; }

    public string SubCName { get; set; } = null!;

    public string SubCDescription { get; set; } = null!;

    public bool IsOfficial { get; set; }

}
