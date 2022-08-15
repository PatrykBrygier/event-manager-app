using EventManagerLibrary.Models;
using EventManagerLibrary.ViewModels;
using System.Collections.Generic;

namespace EventManagerLibrary.Services
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
