using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models;
public class LoginInfo
{
    public string Email { get; set; }
    public string Password { get; set; }

    public LoginInfo() { }

    public LoginInfo(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
