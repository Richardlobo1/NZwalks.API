using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)    
        {
            this._userManager = userManager;
            this.tokenRepository= tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityresult=await _userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityresult.Succeeded) {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityresult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if(identityresult.Succeeded)
                    {
                        return Ok("User registered successfully with roles");
                    }
                    else
                    {
                        return BadRequest(identityresult.Errors);
                    }
                    //await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                }
            }
            return Ok("Something went wrong");
        }

        [HttpPost]
        [Route ("Login")]
        public async Task<IActionResult> Logic([FromBody] LoginRequestDto loginRequestDto)
        {
            var userr=await _userManager.FindByEmailAsync(loginRequestDto.Username);
            if (userr != null)
            {
              var checkPasswordResult=  await _userManager.CheckPasswordAsync(userr, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    // Get the roles of the user
                    var roles=await _userManager.GetRolesAsync(userr);

                    //Create JWT token and return
                    if(roles != null && roles.Any())
                    {
                        var token=tokenRepository.CreateJWTToken(userr, roles.ToList());
                        var  responce = new LoginResponseDto
                        {
                           JwtToken=token
                        };
                        return Ok(responce);
                    }
                   
                }

            }
            return BadRequest("Username or password is incorrect");

        }
    }
}
