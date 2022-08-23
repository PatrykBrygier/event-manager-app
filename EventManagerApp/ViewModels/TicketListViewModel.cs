using EventManagerLibrary.Models;

namespace EventManagerApp.ViewModels
{
    public class TicketListViewModel
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public Customer Customer { get; set; }

        public TicketListViewModel(int id, Customer customer, Event evnt)
        {
            Id = id;
            Event = evnt;
            Customer = customer;
        }
    }
}