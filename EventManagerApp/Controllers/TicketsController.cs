using EventManagerApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EventManagerApp.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext _context;

        public TicketsController()
        {
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
            var evnt = _context.Events.SingleOrDefault(e => e.Id == id);
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
            var eventInDb = _context.Events.SingleOrDefault(e => e.Id == ticket.Event.Id);

            if (ticket.Customer.Id == 0)
            {
                _context.Customers.Add(ticket.Customer);
                _context.SaveChanges();
            }
            else
            {
                var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == ticket.Customer.Id);
                customerInDb.Id = ticket.Customer.Id;
                customerInDb.FirstName = ticket.Customer.FirstName;
                customerInDb.LastName = ticket.Customer.LastName;
                customerInDb.Email = ticket.Customer.Email;
            }

            _context.Tickets.Add(ticket);
            //eventInDb.TicketPool--;
            _context.SaveChanges();

            return RedirectToAction("Index", "Tickets");
        }
    }
}