using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Spell
{
    public int Id { get; set; }

    public int? Character_id {  get; set; }

    public string SpellName { get; set; } = null!;

    public int SpellLevel { get; set; }

}
