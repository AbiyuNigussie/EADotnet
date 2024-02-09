using EventAddis.Dto;
using EventAddis.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EventAddis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        public UserController(IUserService user)
        {
            _user = user;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var allUser = await _user.GetUserInfos();
            return Ok(allUser);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> CreateUser(UserDetailsDto userDetails)
        {
            if (userDetails == null)
            {
                return BadRequest(ModelState);
            }

            var existingUser = (await _user.GetUserInfos().ConfigureAwait(false))
                .Where(u => u.Email.ToLower() == userDetails.Email.ToLower())
                .FirstOrDefault();

            if (existingUser != null)
            {
                ModelState.AddModelError("", "Email Already Exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _user.CreateUser(userDetails))
            {
                ModelState.AddModelError("", "Something Went Wrong While Creating User!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created!");
        }
    }
}
