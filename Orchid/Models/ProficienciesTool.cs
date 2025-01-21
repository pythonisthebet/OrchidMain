using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;
{
    public class ProficienciesTool
    {
        // Properties
        public int Id { get; set; }

        public bool AlchemistsSupplies { get; set; }

        public bool BrewersSupplies { get; set; }

        public bool CalligraphersSupplies { get; set; }

        public bool CarpentersTools { get; set; }

        public bool CartographersTools { get; set; }

        public bool CobblersTools { get; set; }

        public bool CooksUtensils { get; set; }

        public bool GlassblowersTools { get; set; }

        public bool JewelersTools { get; set; }

        public bool LeatherworkersTools { get; set; }

        public bool MasonsTools { get; set; }

        public bool PaintersSupplies { get; set; }

        public bool PottersTools { get; set; }

        public bool SmithsTools { get; set; }

        public bool TinkersTools { get; set; }

        public bool WeaversTools { get; set; }

        public bool WoodcarversTools { get; set; }

        public bool GamingSets { get; set; }

        public bool DiceSet { get; set; }

        public bool PlayingCardSet { get; set; }

        public bool MusicalInstruments { get; set; }

        public bool Bagpipes { get; set; }

        public bool Drum { get; set; }

        public bool Dulcimer { get; set; }

        public bool Flute { get; set; }

        public bool Lute { get; set; }

        public bool Lyre { get; set; }

        public bool Horn { get; set; }

        public bool PanFlute { get; set; }

        public bool Shawm { get; set; }

        public bool Viol { get; set; }

        public bool NavigatorsTools { get; set; }

        public bool ThievesTools { get; set; }

        // Constructor
        public ProficienciesTool() { }

        public ProficienciesTool(Models.ProficienciesTool model)
        {
            this.Id = model.Id;
            this.AlchemistsSupplies = model.AlchemistsSupplies;
            this.BrewersSupplies = model.BrewersSupplies;
            this.CalligraphersSupplies = model.CalligraphersSupplies;
            this.CarpentersTools = model.CarpentersTools;
            this.CartographersTools = model.CartographersTools;
            this.CobblersTools = model.CobblersTools;
            this.CooksUtensils = model.CooksUtensils;
            this.GlassblowersTools = model.GlassblowersTools;
            this.JewelersTools = model.JewelersTools;
            this.LeatherworkersTools = model.LeatherworkersTools;
            this.MasonsTools = model.MasonsTools;
            this.PaintersSupplies = model.PaintersSupplies;
            this.PottersTools = model.PottersTools;
            this.SmithsTools = model.SmithsTools;
            this.TinkersTools = model.TinkersTools;
            this.WeaversTools = model.WeaversTools;
            this.WoodcarversTools = model.WoodcarversTools;
            this.GamingSets = model.GamingSets;
            this.DiceSet = model.DiceSet;
            this.PlayingCardSet = model.PlayingCardSet;
            this.MusicalInstruments = model.MusicalInstruments;
            this.Bagpipes = model.Bagpipes;
            this.Drum = model.Drum;
            this.Dulcimer = model.Dulcimer;
            this.Flute = model.Flute;
            this.Lute = model.Lute;
            this.Lyre = model.Lyre;
            this.Horn = model.Horn;
            this.PanFlute = model.PanFlute;
            this.Shawm = model.Shawm;
            this.Viol = model.Viol;
            this.NavigatorsTools = model.NavigatorsTools;
            this.ThievesTools = model.ThievesTools;
        }

        // Method to return model
        public Models.ProficienciesTool GetModel()
        {
            Models.ProficienciesTool newModel = new Models.ProficienciesTool();
            newModel.Id = this.Id;
            newModel.AlchemistsSupplies = this.AlchemistsSupplies;
            newModel.BrewersSupplies = this.BrewersSupplies;
            newModel.CalligraphersSupplies = this.CalligraphersSupplies;
            newModel.CarpentersTools = this.CarpentersTools;
            newModel.CartographersTools = this.CartographersTools;
            newModel.CobblersTools = this.CobblersTools;
            newModel.CooksUtensils = this.CooksUtensils;
            newModel.GlassblowersTools = this.GlassblowersTools;
            newModel.JewelersTools = this.JewelersTools;
            newModel.LeatherworkersTools = this.LeatherworkersTools;
            newModel.MasonsTools = this.MasonsTools;
            newModel.PaintersSupplies = this.PaintersSupplies;
            newModel.PottersTools = this.PottersTools;
            newModel.SmithsTools = this.SmithsTools;
            newModel.TinkersTools = this.TinkersTools;
            newModel.WeaversTools = this.WeaversTools;
            newModel.WoodcarversTools = this.WoodcarversTools;
            newModel.GamingSets = this.GamingSets;
            newModel.DiceSet = this.DiceSet;
            newModel.PlayingCardSet = this.PlayingCardSet;
            newModel.MusicalInstruments = this.MusicalInstruments;
            newModel.Bagpipes = this.Bagpipes;
            newModel.Drum = this.Drum;
            newModel.Dulcimer = this.Dulcimer;
            newModel.Flute = this.Flute;
            newModel.Lute = this.Lute;
            newModel.Lyre = this.Lyre;
            newModel.Horn = this.Horn;
            newModel.PanFlute = this.PanFlute;
            newModel.Shawm = this.Shawm;
            newModel.Viol = this.Viol;
            newModel.NavigatorsTools = this.NavigatorsTools;
            newModel.ThievesTools = this.ThievesTools;
            return newModel;
        }
    }
}
