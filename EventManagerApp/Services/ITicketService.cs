using EventManagerApp.Models;
using EventManagerApp.ViewModels;
using System.Collections.Generic;

namespace EventManagerApp.Services
{
    public interface ITicketService
    {
        List<Ticket> TicketsToList(int id);
        BuyViewModel NewTicket(int id);
        void SaveTicket(BuyViewModel buyViewModel);
        void RemoveTicket(ReturnViewModel returnViewModel);
    }
}
