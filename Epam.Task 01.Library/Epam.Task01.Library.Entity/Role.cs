using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public override string ToString()
        {
            return $"{RoleName}";
        }
    }
}
