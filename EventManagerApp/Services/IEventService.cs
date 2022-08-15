using EventManagerApp.Models;
using EventManagerApp.ViewModels;
using System.Collections.Generic;

namespace EventManagerApp.Services
{
    public interface IEventService
    {
        void Save(AddEventViewModel addEventViewModel);
        void Delete(int id);
        Event GetEventById(int id);
        List<Event> EventsToList();
        AddEventViewModel NewViewModel(Event evnt);
    }
}
