using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task_01.Library.AbstactBLL.IValidators
{
    public interface IUserValidation
    {
        ValidationObject ValidationObject { get; set; }

        IUserValidation CheckLogin(User user);

        IUserValidation CheckPassword(User user);

        IUserValidation CheckEmptyLogin(User user);

        IUserValidation CheckEmptyPassword(User user);
    }
}
