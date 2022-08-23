using System;

namespace EventManagerApp.ViewModels
{
    public class EventDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public int TicketPool { get; set; }
        public DateTime Date { get; set; }

        public EventDetailsViewModel(int id, string name, string place, int ticketPool, DateTime date)
        {
            Id = id;
            Name = name;
            Place = place;
            TicketPool = ticketPool;
            Date = date;
        }
    }
}