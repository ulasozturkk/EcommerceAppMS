using ECommerceAppMS.IdentityServer.Dtos;
using ECommerceAppMS.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace ECommerceAppMS.IdentityServer.Controllers {

  [Route("api/[controller]/[action]")]
  [Authorize(LocalApi.PolicyName)]
  [ApiController]
  public class UserController : ControllerBase {
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager) {
      _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Signup(SignupDto signupDto) {
      var user = new ApplicationUser {
        UserName = signupDto.UserName,
        Email = signupDto.Email,
        City = signupDto.City,
      };
      var result = await _userManager.CreateAsync(user, signupDto.Password);
      if (!result.Succeeded) {
        return BadRequest(ResponseDTO<NoDataDTO>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
      }
      return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetUser() {
      var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

      if (userIdClaim == null) {
        return BadRequest();
      }

      var user = await _userManager.FindByIdAsync(userIdClaim.Value);
      if (user == null) return BadRequest();

      return Ok(new ApplicationUser { Id = user.Id, UserName = user.UserName, Email = user.Email, City = user.City });
    }
  }
}