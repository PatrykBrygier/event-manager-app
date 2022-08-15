using EventManagerApp.Models;
using EventManagerApp.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void Save(AddEventViewModel addEventViewModel)
        {

            if (addEventViewModel.Id == 0)
            {
                var evnt = new Event
                {
                    Id = addEventViewModel.Id,
                    Name = addEventViewModel.Name,
                    Place = addEventViewModel.Place,
                    Date = addEventViewModel.Date,
                    TicketPool = addEventViewModel.TicketPool
                };

                _context.Events.Add(evnt);
            }
            else
            {
                var eventInDb = _context.Events.Single(e => e.Id == addEventViewModel.Id);
                eventInDb.Name = addEventViewModel.Name;
                eventInDb.Place = addEventViewModel.Place;
                eventInDb.Date = addEventViewModel.Date;
                eventInDb.TicketPool = addEventViewModel.TicketPool;
            }

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var evnt = GetEventById(id);
            var tickets = _context.Tickets.Where(t => t.Event.Id == evnt.Id).Include(t => t.Customer);

            foreach (var ticket in tickets)
            {
                //_context.Customers.Remove(ticket.Customer);
                _context.Tickets.Remove(ticket);
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

        public AddEventViewModel NewViewModel(Event evnt)
        {
            var viewModel = new AddEventViewModel
            {
                Id = evnt.Id,
                Name = evnt.Name,
                Place = evnt.Place,
                Date = evnt.Date,
                TicketPool = evnt.TicketPool
            };

            return viewModel;
        }
    }
}