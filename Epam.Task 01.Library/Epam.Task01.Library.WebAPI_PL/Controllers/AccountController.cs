using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.WebAPI_PL.Filters;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.User;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Security;

namespace Epam.Task01.Library.WebAPI_PL.Controllers
{
    [Route("api/account")]
    public class AccountController : ApiController
    {
        private readonly IUserLogic _userLogic;

        /// <summary>
        /// Controller for working with accounts in WebAPI
        /// </summary>
        /// <param name="userLogic">Linking to the logic of working with accounts</param>
        public AccountController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        /// <summary>
        /// Login in catalog (authentication)
        /// </summary>
        /// <param name="loginmodel">Login's model for authentication in catalog</param>
        /// <returns>If authentication is successful, Ok is returned.
        /// Otherwise, either a validation error or an error in the username and password is returned</returns>
        [Route("login")]
        [LogAuthenticationUserAttribute]
        [HttpPost]
        public IHttpActionResult Login([FromBody]WebApiLoginModel loginmodel)
        {
            if (ModelState.IsValid)
            {
                if (_userLogic.VerifyUser(loginmodel.Login, loginmodel.Password))
                {
                    FormsAuthentication.SetAuthCookie(loginmodel.Login, createPersistentCookie: true);
                    var token = new FormsAuthenticationTicket(loginmodel.Login, true, 20);
                    User = new GenericPrincipal(new FormsIdentity(token), roles: null);
                    return Ok();
                }
                else
                {
                    return BadRequest("Login or password is invalid");
                }
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Logout from catalog (authentication)
        /// </summary>
        /// <returns>Returns a successful response on exit</returns>
        [HttpPost]
        public IHttpActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Ok();
        }
    }
}
