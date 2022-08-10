using EventManagerApp.Models;
using EventManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EventManagerApp.Services
{
    public class TicketService : ITicketService
    {
        private ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Ticket> TicketsToList(int id)
        {
            var tickets = _context.Tickets
                .Where(t => t.Event.Id == id)
                .Include(t => t.Customer)
                .Include(t => t.Event)
                .ToList();

            return tickets;
        }

        public Ticket NewTicket(int id)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.Id == id);
            var ticket = new Ticket
            {
                Event = evnt,
                Customer = new Customer()
            };

            return ticket;
        }

        public void SaveTicket(Ticket ticket)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.Id == ticket.Event.Id);

            if (evnt != null)
            {
                ticket.Event = evnt;

                if (ticket.Customer.Id == 0)
                {
                    _context.Customers.Add(ticket.Customer);
                }
                else
                {
                    var customer = _context.Customers.SingleOrDefault(c => c.Id == ticket.Customer.Id);
                    customer.Id = ticket.Customer.Id;
                    customer.FirstName = ticket.Customer.FirstName;
                    customer.LastName = ticket.Customer.LastName;
                    customer.Email = ticket.Customer.Email;
                }
            }
            else
            {
                throw new NullReferenceException("Event cannot be null");
            }

            ticket.Event.TicketPool--;
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }

        public Ticket GetTicketById(int id)
        {
            var ticket = _context.Tickets
                    .Include(t => t.Customer)
                    .Include(t => t.Event)
                    .FirstOrDefault(t => t.Id == id);

            return ticket;
        }

        public bool IsTicketValid(Ticket ticket, ReturnViewModel returnViewModel)
        {
            return returnViewModel.FirstName == ticket.Customer.FirstName &&
                   returnViewModel.LastName == ticket.Customer.LastName &&
                   returnViewModel.Email == ticket.Customer.Email;
        }

        public void RemoveTicket(Ticket ticket)
        {
            ticket.Event.TicketPool++;
            _context.Customers.Remove(ticket.Customer);
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
        }
    }
}