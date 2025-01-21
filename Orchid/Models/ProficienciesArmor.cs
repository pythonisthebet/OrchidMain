using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;
{
    public class ProficienciesArmor
    {
        public int Id { get; set; }

        public bool AllArmor { get; set; }

        public bool HeavyArmor { get; set; }

        public bool MediumArmor { get; set; }

        public bool LightArmor { get; set; }

        public bool Shield { get; set; }

        public ProficienciesArmor() { }

        public ProficienciesArmor(Models.ProficienciesArmor model)
        {
            this.Id = model.Id;
            this.AllArmor = model.AllArmor;
            this.HeavyArmor = model.HeavyArmor;
            this.MediumArmor = model.MediumArmor;
            this.LightArmor = model.LightArmor;
            this.Shield = model.Shield;
        }

        public Models.ProficienciesArmor GetModel()///////////user id does not link to the respectiv user in db need fix!!!!!!
        {
            Models.ProficienciesArmor newModel = new Models.ProficienciesArmor();
            newModel.Id = this.Id;
            newModel.AllArmor = this.AllArmor;
            newModel.HeavyArmor = this.HeavyArmor;
            newModel.MediumArmor = this.MediumArmor;
            newModel.LightArmor = this.LightArmor;
            newModel.Shield = this.Shield;
            return newModel;
        }
    }
}
