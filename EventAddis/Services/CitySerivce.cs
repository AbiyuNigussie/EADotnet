using EventAddis.Entity;
using EventAddis.Repository;
using WebService.API.Data;

namespace EventAddis.Services
{
    public class CitySerivce : ICityService
    {
        private readonly ApplicationDbContext _context;
        public CitySerivce(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateCity(City cityCreate)
        {
            _context.Cities.Add(cityCreate);
            return Save();
        }

        public ICollection<City> GetCities()
        {

            var cities = _context.Cities.ToList();

            return cities;
         
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
