using EventManagerLibrary.Models;
using EventManagerLibrary.Repositories;
using EventManagerLibrary.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace EventManagerLibrary.Services
{
    public class EventService : IEventService
    {
        private IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public EventModel ReturnEventById(int id)
        {
            var dto = _eventRepository.GetEventById(id);
            return new EventModel(
                dto.Id,
                dto.Name,
                dto.Place,
                dto.TicketPool,
                dto.Date);
        }
        public IEnumerable<EventModel> ReturnEventsList()
        {
            var dtos = _eventRepository.GetEventsToList();

            return dtos.Select(dto => new EventModel(
                dto.Id,
                dto.Name,
                dto.Place,
                dto.TicketPool,
                dto.Date
                ));
        }

        public void SaveEvent(EventModel eventModel)
        {
            var evnt = new Event
            {
                Id = eventModel.Id,
                Name = eventModel.Name,
                Place = eventModel.Place,
                Date = eventModel.Date,
                TicketPool = eventModel.TicketPool
            };

            _eventRepository.CreateOrEditEvent(evnt);
        }

        public void DeleteEvent(int id)
        {
            _eventRepository.RemoveEvent(id);
        }
    }
}