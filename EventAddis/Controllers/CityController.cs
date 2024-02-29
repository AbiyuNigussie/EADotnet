using AutoMapper;
using EventAddis.Entity;
using EventAddis.Models;
using EventAddis.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventAddis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = _cityService.GetCities();
            return Ok(cities);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult CreateCity(RegisterCity cityCreate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cityMap = _mapper.Map<City>(cityCreate);
            if (!_cityService.CreateCity(cityMap))
            {
                ModelState.AddModelError("", "Something Went Wrong While Creating City");
                return StatusCode(500, ModelState);
            }

            return Ok("City Successfully Created!");

        }
    }
}
