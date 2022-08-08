using EventManagerApp.Models;
using EventManagerApp.Services;
using EventManagerApp.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EventManagerApp.Controllers
{
    public class TicketsController : Controller
    {
        private ITicketService _ticketService;
        private ApplicationDbContext _context;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = _context.Tickets
                .Include(t => t.Customer)
                .Include(t => t.Event)
                .ToList();

            return View(tickets);
        }

        public ActionResult Buy(int id)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.Id == id);
            var ticket = new Ticket
            {
                Event = evnt,
                Customer = new Customer()
            };

            return View(ticket);
        }

        [HttpPost]
        public ActionResult Save(Ticket ticket)
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

            return RedirectToAction("Index", "Tickets");
        }

        public ActionResult Return()
        {
            return View("Delete");
        }

        [HttpPost]
        public ActionResult Delete(ReturnViewModel returnViewModel)
        {
            if (returnViewModel == null)
                return HttpNotFound();

            var ticket = _ticketService.GetTicketById(returnViewModel.Id);

            if (ticket == null)
                return HttpNotFound();

            if (returnViewModel.FirstName == ticket.Customer.FirstName &&
                returnViewModel.LastName == ticket.Customer.LastName &&
                returnViewModel.Email == ticket.Customer.Email)
            {
                ticket.Event.TicketPool++;
                _context.Customers.Remove(ticket.Customer);
                _context.Tickets.Remove(ticket);
                _context.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index", "Tickets");
        }
    }
}