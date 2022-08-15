using EventManagerLibrary.Models;
using EventManagerLibrary.ViewModels;
using System.Collections.Generic;

namespace EventManagerLibrary.Services
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
