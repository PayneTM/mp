using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using WannaTravel.Api.Helpers.Extensions;
using WannaTravel.Api.Managers;
using WannaTravel.Api.Models;
using WannaTravel.Api.Security;

namespace WannaTravel.Api.Controllers
{
    public class AccountController : ApiController
    {
        private readonly int _saltLenght = int.Parse(ConfigurationManager.AppSettings["SaltLenght"]);
        private readonly ApplicationUserManager _userManager;

        public AccountController()
        {
            _userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register([FromBody] RegisterUserModel registerUser)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = registerUser.ToApplicationUser();

            var salt = PasswordSalter.GetSalt(_saltLenght);
            user.Salt = salt;

            var saltedPassword = PasswordSalter.SaltPassword(registerUser.Password, salt);
            
            var result = await _userManager.CreateAsync(user, saltedPassword);

            if(!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("login")]
        //public async Task<IHttpActionResult> Login([FromBody] LoginModel registerUser)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var user = registerUser.ToApplicationUser();

        //    var salt = PasswordSalter.GetSalt(_saltLenght);
        //    user.Salt = salt;

        //    var saltedPassword = PasswordSalter.SaltPassword(registerUser.Password, salt);

        //    var result = await _userManager.CreateAsync(user, saltedPassword);

        //    if (!result.Succeeded)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok();
        //}
    }
}
