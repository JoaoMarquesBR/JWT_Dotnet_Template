using JWT_Authentication.Entity;
using JWT_Authentication.Repository;
using JWT_Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace JWT_Authentication.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class LoginController : ControllerBase
    {
        public static readonly TimeSpan TokenLifeTime = TimeSpan.FromMinutes(10);

        private readonly TokenGeneratorService _tokenService;

        public LoginController(TokenGeneratorService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
            {
                return NotFound();
            }

            var token = _tokenService.GenerateToken(user);

            return token;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("TokenGenerationAdmin")]
        public async Task<ActionResult<dynamic>> TokenGenerate()
        {
            User user = new User()
            {
                Role = "admin",
                Username = "adminDev"
            };

            var token = _tokenService.GenerateToken(user);

            return token;
        }

        [HttpGet]
        [Authorize]
        [Route("AuthorizedOnly")]
        public string AuthorizedOnly()
        {
            return " authenticated " + User.Identity.Name;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("Admin")]
        public string AdminAccess()
        {
            return " authenticated " + User.Identity.Name;
        }

        [HttpGet]
        [Authorize(Roles = "admin,developer")]
        [Route("Developer")]
        public string DeveloperAccess()
        {
            return "User authenticated " + User.Identity.Name;
        }

        [HttpGet]
        [Authorize(Roles = "admin,manager")]
        [Route("Manager")]
        public string Manager()
        {
            return "User authenticated " + User.Identity.Name;
        }

    }
}