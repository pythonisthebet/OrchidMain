using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;
public class Comment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PostId { get; set; }

    public string CommentBody { get; set; } = null!;

    public int? Likes { get; set; }
}
