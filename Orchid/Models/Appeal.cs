using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;


public class Appeal
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Reason { get; set; } = null!;

    public string Explanation { get; set; } = null!;


}
