using EventManagerLibrary.Models;
using System.Collections.Generic;

namespace EventManagerLibrary.Repositories
{
    public interface IEventRepository
    {
        Event GetEventById(int id);
        List<Event> GetEventsToList();
        void CreateOrEditEvent(Event evnt);
        void RemoveEvent(int id);
    }
}
