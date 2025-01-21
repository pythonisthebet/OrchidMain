using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Race
{
    public int Id { get; set; }
    public string RaceName { get; set; } = null!;
    public string SubraceName { get; set; } = null!;

    public Race() { }

    public Race(Models.Race model)
    {
        this.Id = model.Id;
        this.RaceName = model.RaceName;
        this.SubraceName = model.SubraceName;
    }

    public Models.Race GetModel()///////////user id does not link to the respectiv user in db need fix!!!!!!
    {
        Models.Race newModel = new Models.Race();
        newModel.Id = this.Id;
        newModel.RaceName = this.RaceName;
        newModel.SubraceName = this.SubraceName;
        return newModel;
    }
}
