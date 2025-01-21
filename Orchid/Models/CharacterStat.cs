using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class CharacterStat
{
    public int Id { get; set; }

    public int Strength { get; set; }

    public int Dexterity { get; set; }

    public int Constitution { get; set; }

    public int Intelligence { get; set; }

    public int Wisdom { get; set; }

    public int Charisma { get; set; }

    public CharacterStat() { }

    public CharacterStat(Models.CharacterStat model)
    {
        this.Id = model.Id;
        this.Strength = model.Strength;
        this.Dexterity = model.Dexterity;
        this.Constitution = model.Constitution;
        this.Intelligence = model.Intelligence;
        this.Wisdom = model.Wisdom;
        this.Charisma = model.Charisma;


    }

    public Models.CharacterStat GetModel()///////////user id does not link to the respectiv user in db need fix!!!!!!
    {
        Models.CharacterStat newModel = new Models.CharacterStat();
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

