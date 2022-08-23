using EventManagerLibrary.Models;
using System.Collections.Generic;

namespace EventManagerLibrary.Repositories
{
    public interface ITicketRepository
    {
        List<Ticket> GetTicketListById(int id);
        List<Ticket> GetTicketListByEmail(string email);
        void CreateTicket(Ticket ticket);
        //void CreateTicketWithLoggedUser(int id, string email);
        void CreateTicketWithLoggedUser(Ticket ticket);
        void DeleteTicket(Ticket ticket);
        Ticket GetTicketById(int id);
        Customer GetCustomerByEmail(string email);
    }
}
