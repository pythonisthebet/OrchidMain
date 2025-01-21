using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Character
{
    public int Id { get; set; }
    public string CharacterName { get; set; } = null!;
    public int? UserId { get; set; }
    public int LevelValue { get; set; }
    public string? ImgId { get; set; }

    public Character() { }
}

