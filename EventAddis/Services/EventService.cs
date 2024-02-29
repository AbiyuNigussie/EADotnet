using AutoMapper;
using EventAddis.Entity;
using EventAddis.Models;
using EventAddis.Repository;
using WebService.API.Data;

namespace EventAddis.Services
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public EventService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
            
        }
        public bool CreateEvent(RegisterEvent eventCreate)
        {

            
            var eventMap = _mapper.Map<Event>(eventCreate);
            var category = _context.Categories.Where(c => c.Name.TrimEnd().ToLower() == eventCreate.CategoryName.Trim().ToLower()).FirstOrDefault();

            var location = _mapper.Map<Location>(eventCreate);

            _context.Locations.Add(location);
            _context.SaveChanges();

            location = _context.Locations.Where(l =>
            l.Region.Trim().ToLower() == eventCreate.Region.Trim().ToLower() &&
            l.City.Trim().ToLower() == eventCreate.City.Trim().ToLower() &&
            l.StreetName.Trim().ToLower() == eventCreate.StreetName.Trim().ToLower() &&
            l.PostalCode.Trim().ToLower() == eventCreate.PostalCode.Trim().ToLower()
            ).FirstOrDefault();

            eventMap.CategoryId = category.CategoryId;
            eventMap.LocationId = location.LocationId;

            _context.Events.Add(eventMap);


            return Save();

        }

        public bool DeleteEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetEvents()
        {
            var AllEvents = _context.Events.ToList();

            return AllEvents;
        }

        public Event GetEventById(int eventId)
        {
            throw new NotImplementedException();
        }

        public Event GetEventsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetEventsByLocation(int locationId)
        {
            throw new NotImplementedException();
        }

        public Event UpdateEvent(int eventId, Event eventUpdate)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
