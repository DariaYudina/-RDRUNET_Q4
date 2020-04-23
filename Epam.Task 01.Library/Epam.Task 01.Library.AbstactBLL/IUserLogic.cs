using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task_01.Library.AbstactBLL
{
    public interface IUserLogic
    {
        bool AddUser(out ValidationObject validationObjec, User user);

        IEnumerable<User> GetUsers();

        User GetUserById(int id);

        bool VerifyUser(string login, string password);

        IEnumerable<Role> GetRoles();

        bool ChangeUserRoles(int userId, List<int> rolesId);
    }
}
