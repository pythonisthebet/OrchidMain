using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Party
{
    public int Id { get; set; }

    public int? DmId { get; set; }

    public string Pname { get; set; } = null!;

    public string? CurrentCampain { get; set; }

    public virtual ICollection<AppUser> AppUsers { get; set; } = new List<AppUser>();

    public Party() { }

    public Party(Models.Party model)
    {
        this.Id = model.Id;
        this.Pname = model.Pname;
        this.DmId = model.DmId;
        this.CurrentCampain = model.CurrentCampain;
        //foreach (var user in model.) // add list of participents like ofer did with his appuser in Taskmenegmnt system
        //{
        //    this.UserTasks.Add(new UserTask(task));
        //}
        }

    public Models.Party GetModel()///////////user id does not link to the respectiv user in db need fix!!!!!!
    {
        Models.Party newModel = new Models.Party();
        newModel.Id = this.Id;
        newModel.Pname = this.Pname;
        newModel.DmId = this.DmId;
        newModel.CurrentCampain = this.CurrentCampain;
        return newModel;
    }
}
