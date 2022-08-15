using EventManagerLibrary.Models;
using EventManagerLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EventManagerLibrary.Services
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

        public List<Ticket> UserTicketsToList(string email)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);

            var tickets = _context.Tickets
                          .Where(t => t.Customer.Id == customer.Id)
                          .Include(t => t.Customer)
                          .Include(t => t.Event)
                          .ToList();

            return tickets;
        }

        public BuyViewModel NewBuyViewModel(int id)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.Id == id);

            var viewModel = new BuyViewModel
            {
                EventId = evnt.Id
            };

            return viewModel;
        }

        public void SaveTicketLoggedUser(int id, string email)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.Id == id);
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);
            var ticket = new Ticket
            {
                Event = evnt,
                Customer = customer
            };

            evnt.TicketPool--;
            _context.Tickets.Add(ticket);
            _context.SaveChanges();

        }

        public void SaveTicket(BuyViewModel buyViewModel)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.Id == buyViewModel.EventId);

            var ticket = new Ticket
            {
                Event = evnt,
                Customer = new Customer()
            };

            var customer = _context.Customers.FirstOrDefault(c => c.Email == buyViewModel.Email);

            if (customer == null)
            {
                ticket.Customer.Id = buyViewModel.CustomerId;
                ticket.Customer.FirstName = buyViewModel.FirstName;
                ticket.Customer.LastName = buyViewModel.LastName;
                ticket.Customer.Email = buyViewModel.Email;

                _context.Customers.Add(ticket.Customer);
            }
            else
            {
                ticket.Customer = customer;
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