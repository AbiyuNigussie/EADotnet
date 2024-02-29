using AutoMapper;
using EventAddis.Models;
using Microsoft.AspNetCore.Mvc;
using WebService.API.Repository;

namespace EventAddis.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userservice;
        private readonly IMapper _mapper;

        public ProfileController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userservice = userService;
        }

        [HttpGet("{id}")]
        public IActionResult GetProfile(Guid id)
        {
            var user = _userservice.GetUserbyId(id);
            if(user == null || user.Role != "User")
            {
                return NotFound();
            }
           
            var userMap = _mapper.Map<UserProfile>(user);

            return Ok(userMap);
            

        }
    }
}
