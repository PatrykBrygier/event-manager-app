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

        public BuyViewModel NewTicket(int id)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.Id == id);

            var viewModel = new BuyViewModel
            {
                EventId = evnt.Id
            };

            return viewModel;
        }

        public void SaveTicket(BuyViewModel buyViewModel)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.Id == buyViewModel.EventId);

            var ticket = new Ticket
            {
                Event = evnt,
                Customer = new Customer()
                {
                    Id = buyViewModel.CustomerId,
                    FirstName = buyViewModel.FirstName,
                    LastName = buyViewModel.LastName,
                    Email = buyViewModel.Email,
                }
            };

            if (buyViewModel.CustomerId == 0)
            {
                _context.Customers.Add(ticket.Customer);
            }
            else
            {
                var customer = _context.Customers.FirstOrDefault(c => c.Id == buyViewModel.CustomerId);
                customer.Id = buyViewModel.CustomerId;
                customer.FirstName = buyViewModel.FirstName;
                customer.LastName = buyViewModel.LastName;
                customer.Email = buyViewModel.Email;
            }

            ticket.Event.TicketPool--;
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }

        public void RemoveTicket(ReturnViewModel returnViewModel)
        {
            var ticket = _context.Tickets
                    .Include(t => t.Customer)
                    .Include(t => t.Event)
                    .FirstOrDefault(t => t.Id == returnViewModel.Id);

            if (returnViewModel.FirstName == ticket.Customer.FirstName &&
                returnViewModel.LastName == ticket.Customer.LastName &&
                returnViewModel.Email == ticket.Customer.Email)
            {
                ticket.Event.TicketPool++;
                _context.Customers.Remove(ticket.Customer);
                _context.Tickets.Remove(ticket);
            }
            else
            {
                throw new Exception("There is no such event");
            }

            _context.SaveChanges();
        }

    }
}