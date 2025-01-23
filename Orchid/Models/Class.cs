using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Class
{
    public int Id { get; set; }
    public string ClassName { get; set; } = null!;
    public string SubclassName { get; set; } = null!;
    public int LevelValue { get; set; }

    public Class(int id, string className)
    {
        Id = id;
        ClassName = className;
        SubclassName = "";
        LevelValue = 0;
    }

}
