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

    public Class() { }
    public Class(Models.Class model)
    {
        this.Id = model.Id;
        this.ClassName = model.ClassName;
        this.SubclassName = model.SubclassName;
        this.LevelValue = model.LevelValue;

    }

    public Models.Class GetModel()///////////user id does not link to the respectiv user in db need fix!!!!!!
    {
        Models.Class newModel = new Models.Class();
        newModel.Id = this.Id;
        newModel.ClassName = this.ClassName;
        newModel.SubclassName = this.SubclassName;
        newModel.LevelValue = this.LevelValue;


        return newModel;
    }
}
