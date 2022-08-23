using EventManagerLibrary.Models;

namespace EventManagerLibrary.Services.Models
{
    public class TicketListModel
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Event Event { get; set; }

        public TicketListModel(int id, Customer customer, Event evnt)
        {
            Id = id;
            Customer = customer;
            Event = evnt;
        }
    }
}
