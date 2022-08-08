using EventManagerApp.Models;
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

        public Ticket GetTicketById(int id)
        {
            var ticket = _context.Tickets
                    .Include(t => t.Customer)
                    .Include(t => t.Event)
                    .FirstOrDefault(t => t.Id == id);

            return ticket;
        }
    }
}