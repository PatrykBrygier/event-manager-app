using EventManagerLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace EventManagerLibrary.Repositories
{
    public class EventRepository : IEventRepository
    {
        private ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Event GetEventById(int id)
        {
            var evnt = _context.Events.SingleOrDefault(e => e.Id == id);
            return evnt;
        }

        public List<Event> GetEventsToList()
        {
            var events = _context.Events.ToList();
            return events;
        }

        public void CreateOrEditEvent(Event evnt)
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

        public void RemoveEvent(int id)
        {
            var evnt = GetEventById(id);
            var tickets = _context.Tickets
                                  .Where(t => t.Event.Id == evnt.Id)
                                  .ToList();

            foreach (var ticket in tickets)
            {
                _context.Tickets.Remove(ticket);
            }

            _context.Events.Remove(evnt);
            _context.SaveChanges();
        }
    }
}
