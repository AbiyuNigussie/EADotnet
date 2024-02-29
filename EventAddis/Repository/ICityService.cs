using EventAddis.Entity;

namespace EventAddis.Repository
{
    public interface ICityService
    {
        public ICollection<City> GetCities();

        public bool CreateCity(City cityCreate);

        public bool Save();
    }
}
