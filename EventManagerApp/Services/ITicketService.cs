using EventManagerApp.Models;
using EventManagerApp.ViewModels;
using System.Collections.Generic;

namespace EventManagerApp.Services
{
    public interface ITicketService
    {
        List<Ticket> TicketsToList(int id);
        Ticket NewTicket(int id);
        void SaveTicket(Ticket ticket);
        Ticket GetTicketById(int id);
        bool IsTicketValid(Ticket ticket, ReturnViewModel returnViewModel);
        void RemoveTicket(Ticket ticket);
    }
}
