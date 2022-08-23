namespace EventManagerLibrary.Services.Models
{
    public class TicketModel
    {
        public int EventId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public TicketModel(int eventId, string firstName, string lastName, string email)
        {
            EventId = eventId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
