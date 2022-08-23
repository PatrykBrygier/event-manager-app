using EventManagerLibrary.Services.Models;
using System.Collections.Generic;

namespace EventManagerLibrary.Services
{
    public interface ITicketService
    {
        IEnumerable<TicketListModel> TicketsToList(int id);
        IEnumerable<TicketListModel> UserTicketList(string email);
        void SaveTicket(TicketModel ticketModel);
        //void SaveTicketLoggedUser(int id, string email);
        void SaveTicketLoggedUser(UserModel userModel);
        void RemoveTicket(ReturnTicketModel returnTicketModel);
    }
}
