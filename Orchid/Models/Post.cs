using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Post
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = null!;

    public int? Forum { get; set; }

    public string PostBody { get; set; } = null!;

    public int? Likes { get; set; }

    public int? Pviews { get; set; }

    public Post() { }

}
