using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.AbstractDAL
{
    public interface IUserDao
    {
        int AddUser(User user);

        IEnumerable<User> GetUsers();

        User GetUserById(int id);

        IEnumerable<Role> GetRoles();

        bool ChangeUserRoles(int userId, List<int> rolesId);
    }
}
