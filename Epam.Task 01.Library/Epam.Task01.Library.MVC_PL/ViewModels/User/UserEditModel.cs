using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Epam.Task01.Library.MVC_PL.ViewModels.User
{
    public class UserEditModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string Login { get; set; }

        [ScaffoldColumn(false)]

        public List<Role> Roles { get; set; }

        [Display(Name = "Roles")]
        public List<int> RolesId { get; set; } = new List<int>();
    }
}