using EventManagerApp.Models;
using EventManagerApp.ViewModels;
using System.Collections.Generic;

namespace EventManagerApp.Services
{
    public interface ITicketService
    {
        List<Ticket> TicketsToList(int id);
        BuyViewModel NewBuyViewModel(int id);
        void SaveTicket(BuyViewModel buyViewModel);
        void RemoveTicket(ReturnViewModel returnViewModel);
        void SaveTicketLoggedUser(int id, string email);
        List<Ticket> UserTicketsToList(string email);
    }
}
