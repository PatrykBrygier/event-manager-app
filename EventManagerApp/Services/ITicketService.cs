using EventManagerApp.Models;

namespace EventManagerApp.Services
{
    public interface ITicketService
    {
        Ticket GetTicketById(int id);
    }
}
