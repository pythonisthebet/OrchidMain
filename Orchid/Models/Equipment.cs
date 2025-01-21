using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class Equipment
{
    public int Id { get; set; }
    public bool IsWeapon { get; set; }
    public bool IsArmor { get; set; }
    public bool IsShield { get; set; }
    public bool IsAttunment { get; set; }

    public Equipment() { }
    public Equipment(Models.Equipment model)
    {
        this.Id = model.Id;
        this.IsWeapon = model.IsWeapon;
        this.IsArmor = model.IsArmor;
        this.IsShield = model.IsShield;
        this.IsAttunment = model.IsAttunment;
    }

    public Models.Equipment GetModel()///////////user id does not link to the respectiv user in db need fix!!!!!!
    {
        Models.Equipment newModel = new Models.Equipment();
        newModel.Id = this.Id;
        newModel.IsWeapon = this.IsWeapon;
        newModel.IsArmor = this.IsArmor;
        newModel.IsShield = this.IsShield;
        newModel.IsAttunment = this.IsAttunment;
        return newModel;
    }
}
