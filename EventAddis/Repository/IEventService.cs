using EventAddis.Entity;
using EventAddis.Models;

namespace EventAddis.Repository
{
    public interface IEventService
    {
        Event GetEventById(int eventId);
        IEnumerable<Event> GetEvents();
        Event GetEventsByCategory(int categoryId);
        IEnumerable<Event> GetEventsByLocation(int locationId);
        bool CreateEvent(RegisterEvent eventCreate);
        Event UpdateEvent(int eventId, Event eventUpdate);
        bool DeleteEvent(int eventId);
        bool Save();
    }

}

