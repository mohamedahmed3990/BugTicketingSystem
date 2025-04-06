using System.Security.Claims;
using BugTicketingSystem.BLL.DTOs;
using BugTicketingSystem.BLL.Services.AuthService;
using BugTicketingSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;

        public AccountController(UserManager<AppUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.email);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _userManager.CheckPasswordAsync(user, loginDto.password);
            if (!result)
            {
                return Unauthorized();
            }

            var claims = await _userManager.GetClaimsAsync(user);

            var token = _authService.CreateToken(claims.ToList());
            return Ok(new UserDto(user.UserName, user.PhoneNumber, user.Email, token));


        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            var user = new AppUser()
            {
                Email = registerDto.email,
                PhoneNumber = registerDto.phoneNumber,
                UserName = registerDto.email.Split("@")[0]
            };


            var result = await _userManager.CreateAsync(user, registerDto.password);
            if (result.Succeeded is false)
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName , user.UserName),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.Role , registerDto.role),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
            };

            await _userManager.AddClaimsAsync(user, claims);

            var token = _authService.CreateToken(claims);
            return new UserDto(user.UserName, user.PhoneNumber, user.Email, token);
        }




    }
}
