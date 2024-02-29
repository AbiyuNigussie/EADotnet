using AutoMapper;
using EventAddis.Entity;
using EventAddis.Models;
using EventAddis.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebService.API.Repository;

namespace EventAddis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;
        private readonly ICategoryService _categoryService;
        private readonly ICityService _cityService;
        private readonly IUserService _userService;
        public EventController(IMapper mapper, IFileService fileService, IEventService eventService, ICategoryService categoryService, ICityService cityService, IUserService userService)
        {
            _mapper = mapper;
            _eventService = eventService;
            _categoryService = categoryService;
            _cityService = cityService;
            _userService = userService;
            _fileService = fileService;
        }


        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var AllEvents = _eventService.GetEvents();
            return Ok(AllEvents);
        }

        [HttpPost]
        public IActionResult CreateEvent([FromForm]RegisterEvent eventCreate)
        {

            var mappedEvent = _mapper.Map<Event>(eventCreate);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_categoryService.GetCategories().Where(c => c.Name.TrimEnd().ToLower() ==  eventCreate.CategoryName.ToLower()).Any()) {
                ModelState.AddModelError("", "Selected Category Doesn't Exist!");
                return StatusCode(404, ModelState); 
            }
            if(!_cityService.GetCities().Where(c => c.Name.Trim().ToLower() == eventCreate.City.Trim().ToLower()).Any())
            {
                ModelState.AddModelError("", "Selected City Doesn't Exist!");
                return StatusCode(404, ModelState);
            }

            if(!_userService.GetUsers().Where(u => u.UserId == eventCreate.UserId).Any())
            {
                ModelState.AddModelError("", "Selected User Doesn't Exist!");
                return StatusCode(404, ModelState);

            }

            if(eventCreate.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(eventCreate.ImageFile, "EventImages");
                if(fileResult.Item1 == 1)
                {
                    var hostUrl = "https://localhost:7160/uploads/EventImages/";
                    eventCreate.Image = hostUrl+fileResult.Item2;
                }

            }

            
            
            if(!_eventService.CreateEvent(eventCreate))
            {
                return BadRequest(ModelState);
            }

            return Ok("Successfully Created!");

            
            
        }
    }
}
