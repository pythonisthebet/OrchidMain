using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;

public class AppUser
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public bool IsPremium { get; set; }

    public bool IsAdmin { get; set; }

    public AppUser() { }
    public AppUser(Models.AppUser model)
    {
        this.Id = model.Id;
        this.UserName = model.UserName;
        this.UserEmail = model.UserEmail;
        this.UserPassword = model.UserPassword;
        this.IsAdmin = model.IsAdmin;
        this.IsPremium = model.IsPremium;
    }

    public Models.AppUser GetModel()///////////user id does not link to the respectiv user in db need fix!!!!!!
    {
        Models.AppUser newModel = new Models.AppUser();
        newModel.Id = this.Id;
        newModel.UserName = this.UserName;
        newModel.UserEmail = this.UserEmail;
        newModel.UserPassword = this.UserPassword;
        newModel.IsPremium = this.IsPremium;
        newModel.IsAdmin = this.IsAdmin;
        return newModel;
    }
}
