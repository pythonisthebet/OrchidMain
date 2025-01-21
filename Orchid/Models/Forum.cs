using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Forum
{
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public Forum() { }

    public Forum(Models.Forum model)
    {
        this.Id = model.Id;
        this.Fname = model.Fname;
    }

    public Models.Forum GetModel()///////////user id does not link to the respectiv user in db need fix!!!!!!
    {
        Models.Forum newModel = new Models.Forum();
        newModel.Id = this.Id;
        newModel.Fname = this.Fname;
        return newModel;
    }
}
