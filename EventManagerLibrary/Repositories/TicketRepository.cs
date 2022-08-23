using EventManagerLibrary.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EventManagerLibrary.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private ApplicationDbContext _context;
        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ticket GetTicketById(int id)
        {
            var ticket = _context.Tickets
                        .Include(t => t.Customer)
                        .Include(t => t.Event)
                        .FirstOrDefault(t => t.Id == id);

            return ticket;
        }

        public List<Ticket> GetTicketListById(int id)
        {
            var tickets = _context.Tickets
                          .Where(t => t.Event.Id == id)
                          .Include(t => t.Customer)
                          .Include(t => t.Event)
                          .ToList();

            return tickets;
        }

        public List<Ticket> GetTicketListByEmail(string email)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);

            var tickets = _context.Tickets
                          .Where(t => t.Customer.Id == customer.Id)
                          .Include(t => t.Customer)
                          .Include(t => t.Event)
                          .ToList();

            return tickets;
        }

        public void CreateTicket(Ticket ticket)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.Id == ticket.Event.Id);
            var customer = _context.Customers.FirstOrDefault(c => c.Email == ticket.Customer.Email);
            ticket.Event = evnt;

            if (customer == null)
            {
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

        //public void CreateTicketWithLoggedUser(int id, string email)
        //{
        //    var evnt = _context.Events.FirstOrDefault(e => e.Id == id);

        //    var customer = _context.Customers.FirstOrDefault(c => c.Email == email);

        //    var ticket = new Ticket
        //    {
        //        Event = evnt,
        //        Customer = customer
        //    };

        //    evnt.TicketPool--;
        //    _context.Tickets.Add(ticket);
        //    _context.SaveChanges();
        //}

        public void CreateTicketWithLoggedUser(Ticket ticket)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.Id == ticket.Event.Id);
            var customer = _context.Customers.FirstOrDefault(c => c.Email == ticket.Customer.Email);

            ticket.Event = evnt;
            ticket.Customer = customer;

            ticket.Event.TicketPool--;
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }

        public void DeleteTicket(Ticket ticket)
        {
            ticket.Event.TicketPool++;
            _context.Customers.Remove(ticket.Customer);
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
        }

        public Customer GetCustomerByEmail(string email)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);
            return customer;
        }
    }
}
