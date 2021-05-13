using AutoMapper;
using Library.Contracts;
using Library.Entities.DataTransferObjects;
using Library.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private ApplicationSettings _appSettings;
        public AuthenticationController(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IOptions<ApplicationSettings> appSettings)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }


        [HttpPost]

        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if(userForRegistration == null)
            {
                _logger.LogError("UserForRegistrationDto object sent from client is null.");
                return BadRequest("UserForRegistrationDto object is null");
            }

            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserForAuthenticationDto model)
        {
            if (model == null)
            {
                _logger.LogError("Login Model sent from client is null.");
                return BadRequest("Empty User Cannot Be Logged In");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Login");
                return UnprocessableEntity(ModelState);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return BadRequest("Username or Password is incorrect");
            }

            var userRole = await _userManager.GetRolesAsync(user);
            var userRoles = new List<string> { "Administrator", "Manager" };

            foreach (var role in userRole)
            {
                var conditionForAuthority = userRoles.Contains(role.ToString());
                if (!conditionForAuthority)
                {
                    return Unauthorized("Unauthorized");
                }
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                Console.WriteLine("Hello");
                var tokenDescriptor = new SecurityTokenDescriptor();
                Claim[] myClaims = new Claim[]
                {

                        new Claim("UserID", user.Id.ToString()),
                        new Claim("UserName", user.UserName.ToString()),
                        new Claim("UserEmail", user.Email.ToString()),
                        new Claim(ClaimTypes.Role, userRole.FirstOrDefault())
                    
                };
                tokenDescriptor.Subject = new ClaimsIdentity(myClaims);
                tokenDescriptor.Expires = DateTime.UtcNow.AddDays(1);
                tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("`!`)$^`%&`*@issu31r@ck3r!|¬9+*a-T-s|¬`(")), SecurityAlgorithms.HmacSha256Signature);
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}
