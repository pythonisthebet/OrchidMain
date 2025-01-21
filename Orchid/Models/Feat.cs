using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Feat
{
    public int Id { get; set; }
    public string FeatName { get; set; } = null!;
    public int LevelTaken { get; set; }

    public Feat() { }

    public Feat(Models.Feat model)
    {
        this.Id = model.Id;
        this.FeatName = model.FeatName;
        this.LevelTaken = model.LevelTaken;
    }

    public Models.Feat GetModel()///////////user id does not link to the respectiv user in db need fix!!!!!!
    {
        Models.Feat newModel = new Models.Feat();
        newModel.Id = this.Id;
        newModel.FeatName = this.FeatName;
        newModel.LevelTaken = this.LevelTaken;
        return newModel;
    }
}
