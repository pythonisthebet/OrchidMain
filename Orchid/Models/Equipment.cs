using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Equipment
{
    public int Id { get; set; }
    public bool IsWeapon { get; set; }
    public bool IsArmor { get; set; }
    public bool IsShield { get; set; }
    public bool IsAttunment { get; set; }

}
