using EventManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagerApp.Services
{
    public class EventService : IEventService
    {
        private ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save(Event evnt)
        {
            if (evnt.Id == 0)
            {
                _context.Events.Add(evnt);
            }
            else
            {
                var eventInDb = _context.Events.Single(e => e.Id == evnt.Id);

                eventInDb.Name = evnt.Name;
                eventInDb.Place = evnt.Place;
                eventInDb.Date = evnt.Date;
                eventInDb.TicketPool = evnt.TicketPool;
            }

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var evnt = _context.Events.SingleOrDefault(e => e.Id == id);

            if (evnt == null)
            {
                throw new NullReferenceException("Event cannot be null");
            }

            _context.Events.Remove(evnt);
            _context.SaveChanges();
        }

        public Event GetEventById(int id)
        {
            var evnt = _context.Events.SingleOrDefault(e => e.Id == id);

            return evnt;
        }

        public List<Event> EventsToList()
        {
            var events = _context.Events.ToList();
            return events;
        }

    }
}