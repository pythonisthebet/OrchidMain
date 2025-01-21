using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class ProficienciesWeapon
{
    // Properties
    public int Id { get; set; }

    public bool SimpleWeapons { get; set; }

    public bool MartialWeapons { get; set; }

    public bool Club { get; set; }

    public bool Dagger { get; set; }

    public bool Greatclub { get; set; }

    public bool Handaxe { get; set; }

    public bool Javelin { get; set; }

    public bool LightHammer { get; set; }

    public bool Mace { get; set; }

    public bool Quarterstaff { get; set; }

    public bool Sickle { get; set; }

    public bool Spear { get; set; }

    public bool LightCrossbow { get; set; }

    public bool Dart { get; set; }

    public bool Shortbow { get; set; }

    public bool Sling { get; set; }

    public bool Battleaxe { get; set; }

    public bool Flail { get; set; }

    public bool Glaive { get; set; }

    public bool Greataxe { get; set; }

    public bool Greatsword { get; set; }

    public bool Halberd { get; set; }

    public bool Lance { get; set; }

    public bool Longsword { get; set; }

    public bool Maul { get; set; }

    public bool Morningstar { get; set; }

    public bool Pike { get; set; }

    public bool Rapier { get; set; }

    public bool Scimitar { get; set; }

    public bool Shortsword { get; set; }

    public bool Trident { get; set; }

    public bool WarPick { get; set; }

    public bool Warhammer { get; set; }

    public bool Whip { get; set; }

    public bool Blowgun { get; set; }

    public bool HandCrossbow { get; set; }

    public bool HeavyCrossbow { get; set; }

    public bool Longbow { get; set; }

    public bool Net { get; set; }

    // Constructor
    public ProficienciesWeapon() { }

    public ProficienciesWeapon(Models.ProficienciesWeapon model)
    {
        this.Id = model.Id;
        this.SimpleWeapons = model.SimpleWeapons;
        this.MartialWeapons = model.MartialWeapons;
        this.Club = model.Club;
        this.Dagger = model.Dagger;
        this.Greatclub = model.Greatclub;
        this.Handaxe = model.Handaxe;
        this.Javelin = model.Javelin;
        this.LightHammer = model.LightHammer;
        this.Mace = model.Mace;
        this.Quarterstaff = model.Quarterstaff;
        this.Sickle = model.Sickle;
        this.Spear = model.Spear;
        this.LightCrossbow = model.LightCrossbow;
        this.Dart = model.Dart;
        this.Shortbow = model.Shortbow;
        this.Sling = model.Sling;
        this.Battleaxe = model.Battleaxe;
        this.Flail = model.Flail;
        this.Glaive = model.Glaive;
        this.Greataxe = model.Greataxe;
        this.Greatsword = model.Greatsword;
        this.Halberd = model.Halberd;
        this.Lance = model.Lance;
        this.Longsword = model.Longsword;
        this.Maul = model.Maul;
        this.Morningstar = model.Morningstar;
        this.Pike = model.Pike;
        this.Rapier = model.Rapier;
        this.Scimitar = model.Scimitar;
        this.Shortsword = model.Shortsword;
        this.Trident = model.Trident;
        this.WarPick = model.WarPick;
        this.Warhammer = model.Warhammer;
        this.Whip = model.Whip;
        this.Blowgun = model.Blowgun;
        this.HandCrossbow = model.HandCrossbow;
        this.HeavyCrossbow = model.HeavyCrossbow;
        this.Longbow = model.Longbow;
        this.Net = model.Net;
    }

    // Method to return model
    public Models.ProficienciesWeapon GetModel()
    {
        Models.ProficienciesWeapon newModel = new Models.ProficienciesWeapon();
        newModel.Id = this.Id;
        newModel.SimpleWeapons = this.SimpleWeapons;
        newModel.MartialWeapons = this.MartialWeapons;
        newModel.Club = this.Club;
        newModel.Dagger = this.Dagger;
        newModel.Greatclub = this.Greatclub;
        newModel.Handaxe = this.Handaxe;
        newModel.Javelin = this.Javelin;
        newModel.LightHammer = this.LightHammer;
        newModel.Mace = this.Mace;
        newModel.Quarterstaff = this.Quarterstaff;
        newModel.Sickle = this.Sickle;
        newModel.Spear = this.Spear;
        newModel.LightCrossbow = this.LightCrossbow;
        newModel.Dart = this.Dart;
        newModel.Shortbow = this.Shortbow;
        newModel.Sling = this.Sling;
        newModel.Battleaxe = this.Battleaxe;
        newModel.Flail = this.Flail;
        newModel.Glaive = this.Glaive;
        newModel.Greataxe = this.Greataxe;
        newModel.Greatsword = this.Greatsword;
        newModel.Halberd = this.Halberd;
        newModel.Lance = this.Lance;
        newModel.Longsword = this.Longsword;
        newModel.Maul = this.Maul;
        newModel.Morningstar = this.Morningstar;
        newModel.Pike = this.Pike;
        newModel.Rapier = this.Rapier;
        newModel.Scimitar = this.Scimitar;
        newModel.Shortsword = this.Shortsword;
        newModel.Trident = this.Trident;
        newModel.WarPick = this.WarPick;
        newModel.Warhammer = this.Warhammer;
        newModel.Whip = this.Whip;
        newModel.Blowgun = this.Blowgun;
        newModel.HandCrossbow = this.HandCrossbow;
        newModel.HeavyCrossbow = this.HeavyCrossbow;
        newModel.Longbow = this.Longbow;
        newModel.Net = this.Net;
        return newModel;
    }
}

