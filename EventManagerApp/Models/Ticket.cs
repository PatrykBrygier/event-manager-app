namespace EventManagerApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public Customer Customer { get; set; }
    }
}