using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Epam.Task01.Library.WebAPI_PL.Providers
{
    public class WebApiRoleProvider : RoleProvider
    {
        private readonly IUserLogic _userLogic;

        public WebApiRoleProvider()
        {
            this._userLogic = DependencyResolver.Current.GetService<IUserLogic>();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            User user = _userLogic.GetUsers().FirstOrDefault(u => u.Login == username);
            if (user != null && user.Roles != null)
            {
                roles = user.Roles.Select(i => i.RoleName).ToArray();
            }

            return roles;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = _userLogic.GetUsers().FirstOrDefault(x => x.Login == username);
            if (user != null)
            {
                return user.Roles.Any(i => i.RoleName == roleName);
            }

            return false;
        }


        #region NotImplement
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion NotImplement
    }
}