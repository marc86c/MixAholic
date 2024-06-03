using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MixAholicAPI.Service;
using MixAholicCommon.Model;

namespace MixAholicAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService AuthService { get; set; }
        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }

        [HttpPost("Register", Name = "Register")]
        public ActionResult<bool> Register([FromBody] AuthModel registerModel)
        {
            return Ok(AuthService.Register(registerModel.Username, registerModel.Password));
        }

        [HttpPost("Login", Name = "Login")]
        public ActionResult<string> Login(AuthModel loginModel)
        {
            var sessionKey = AuthService.Login(loginModel.Username, loginModel.Password);
            if (string.IsNullOrEmpty(sessionKey))
            {
                return BadRequest();
            }

            //return Ok(UserService.GetUserData(loginModel.Username));
            return Ok(sessionKey);
        }
	}
}
