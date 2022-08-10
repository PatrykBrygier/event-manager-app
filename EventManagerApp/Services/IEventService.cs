using EventManagerApp.Models;
using System.Collections.Generic;

namespace EventManagerApp.Services
{
    public interface IEventService
    {
        void Save(Event evnt);
        void Delete(int id);
        Event GetEventById(int id);
        List<Event> EventsToList();
    }
}
