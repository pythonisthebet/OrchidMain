using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;
{
    public class ProficienciesSafe
    {
        public int Id { get; set; }

        public bool Strength { get; set; }

        public bool Dexterity { get; set; }

        public bool Constitution { get; set; }

        public bool Intelligence { get; set; }

        public bool Wisdom { get; set; }

        public bool Charisma { get; set; }

        public ProficienciesSafe() { }

        public ProficienciesSafe(Models.ProficienciesSafe model)
        {
            this.Id = model.Id;
            this.Strength = model.Strength;
            this.Dexterity = model.Dexterity;
            this.Constitution = model.Constitution;
            this.Intelligence = model.Intelligence;
            this.Wisdom = model.Wisdom;
            this.Charisma = model.Charisma;


        }

        public Models.ProficienciesSafe GetModel()///////////user id does not link to the respectiv user in db need fix!!!!!!
        {
            Models.ProficienciesSafe newModel = new Models.ProficienciesSafe();
            newModel.Id = this.Id;
            newModel.Strength = this.Strength;
            newModel.Dexterity = this.Dexterity;
            newModel.Constitution = this.Constitution;
            newModel.Intelligence = this.Intelligence;
            newModel.Wisdom = this.Wisdom;
            newModel.Charisma = this.Charisma;
            return newModel;
        }
    }
}
