using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.MVC_PL.ViewModels.User
{
    public class UserDetailsModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        [UIHint("Collection")]
        public List<Role> Roles { get; set; }

        public override string ToString()
        {
            return Roles == null ? string.Join(", ", Roles) : "not defined";
        }
    }
}