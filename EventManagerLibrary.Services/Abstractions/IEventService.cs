using EventManagerLibrary.Services.Models;
using System.Collections.Generic;

namespace EventManagerLibrary.Services
{
    public interface IEventService
    {
        void SaveEvent(EventModel eventModel);
        void DeleteEvent(int id);
        EventModel ReturnEventById(int id);
        IEnumerable<EventModel> ReturnEventsList();
    }
}
