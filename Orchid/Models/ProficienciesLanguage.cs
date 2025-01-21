using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;
{
    public class ProficienciesLanguage
    {
        // Properties
        public int Id { get; set; }

        public bool Common { get; set; }

        public bool Dwarvish { get; set; }

        public bool Elvish { get; set; }

        public bool Giant { get; set; }

        public bool Gnomish { get; set; }

        public bool Goblin { get; set; }

        public bool Halfling { get; set; }

        public bool OrcLanguage { get; set; }

        public bool Abyssal { get; set; }

        public bool Celestial { get; set; }

        public bool Draconic { get; set; }

        public bool DeepSpeech { get; set; }

        public bool Infernal { get; set; }

        public bool Primordial { get; set; }

        public bool Sylvan { get; set; }

        public bool Undercommon { get; set; }

        public bool Druidic { get; set; }

        public bool ThievesCant { get; set; }

        // Constructor
        public ProficienciesLanguage() { }

        public ProficienciesLanguage(Models.ProficienciesLanguage model)
        {
            this.Id = model.Id;
            this.Common = model.Common;
            this.Dwarvish = model.Dwarvish;
            this.Elvish = model.Elvish;
            this.Giant = model.Giant;
            this.Gnomish = model.Gnomish;
            this.Goblin = model.Goblin;
            this.Halfling = model.Halfling;
            this.OrcLanguage = model.OrcLanguage;
            this.Abyssal = model.Abyssal;
            this.Celestial = model.Celestial;
            this.Draconic = model.Draconic;
            this.DeepSpeech = model.DeepSpeech;
            this.Infernal = model.Infernal;
            this.Primordial = model.Primodial;
            this.Sylvan = model.Sylvan;
            this.Undercommon = model.Undercommon;
            this.Druidic = model.Druidic;
            this.ThievesCant = model.ThievesCant;
        }

        // Method to return model
        public Models.ProficienciesLanguage GetModel()
        {
            Models.ProficienciesLanguage newModel = new Models.ProficienciesLanguage();
            newModel.Id = this.Id;
            newModel.Common = this.Common;
            newModel.Dwarvish = this.Dwarvish;
            newModel.Elvish = this.Elvish;
            newModel.Giant = this.Giant;
            newModel.Gnomish = this.Gnomish;
            newModel.Goblin = this.Goblin;
            newModel.Halfling = this.Halfling;
            newModel.OrcLanguage = this.OrcLanguage;
            newModel.Abyssal = this.Abyssal;
            newModel.Celestial = this.Celestial;
            newModel.Draconic = this.Draconic;
            newModel.DeepSpeech = this.DeepSpeech;
            newModel.Infernal = this.Infernal;
            newModel.Primodial = this.Primordial;
            newModel.Sylvan = this.Sylvan;
            newModel.Undercommon = this.Undercommon;
            newModel.Druidic = this.Druidic;
            newModel.ThievesCant = this.ThievesCant;
            return newModel;
        }
    }
}
