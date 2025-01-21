using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;
{
    public class ProficienciesSkill
    {
        // Properties
        public int Id { get; set; }

        public bool Acrobatics { get; set; }

        public bool AnimalHandling { get; set; }

        public bool Arcana { get; set; }

        public bool Athletics { get; set; }

        public bool Deception { get; set; }

        public bool History { get; set; }

        public bool Insight { get; set; }

        public bool Intimidation { get; set; }

        public bool Investigation { get; set; }

        public bool Medicine { get; set; }

        public bool Nature { get; set; }

        public bool Perception { get; set; }

        public bool Performance { get; set; }

        public bool Persuasion { get; set; }

        public bool Religion { get; set; }

        public bool SleightOfHand { get; set; }

        public bool Stealth { get; set; }

        public bool Survival { get; set; }

        // Constructor
        public ProficienciesSkill() { }

        public ProficienciesSkill(Models.ProficienciesSkill model)
        {
            this.Id = model.Id;
            this.Acrobatics = model.Acrobatics;
            this.AnimalHandling = model.AnimalHandling;
            this.Arcana = model.Arcana;
            this.Athletics = model.Athletics;
            this.Deception = model.Deception;
            this.History = model.History;
            this.Insight = model.Insight;
            this.Intimidation = model.Intimidation;
            this.Investigation = model.Investigation;
            this.Medicine = model.Medicine;
            this.Nature = model.Nature;
            this.Perception = model.Perception;
            this.Performance = model.Performance;
            this.Persuasion = model.Persuasion;
            this.Religion = model.Religion;
            this.SleightOfHand = model.SleightOfHand;
            this.Stealth = model.Stealth;
            this.Survival = model.Survival;
        }

        // Method to return model
        public Models.ProficienciesSkill GetModel() ///////////user id does not link to the respective user in db need fix!!!!!!
        {
            Models.ProficienciesSkill newModel = new Models.ProficienciesSkill();
            newModel.Id = this.Id;
            newModel.Acrobatics = this.Acrobatics;
            newModel.AnimalHandling = this.AnimalHandling;
            newModel.Arcana = this.Arcana;
            newModel.Athletics = this.Athletics;
            newModel.Deception = this.Deception;
            newModel.History = this.History;
            newModel.Insight = this.Insight;
            newModel.Intimidation = this.Intimidation;
            newModel.Investigation = this.Investigation;
            newModel.Medicine = this.Medicine;
            newModel.Nature = this.Nature;
            newModel.Perception = this.Perception;
            newModel.Performance = this.Performance;
            newModel.Persuasion = this.Persuasion;
            newModel.Religion = this.Religion;
            newModel.SleightOfHand = this.SleightOfHand;
            newModel.Stealth = this.Stealth;
            newModel.Survival = this.Survival;
            return newModel;
        }
    }
}
