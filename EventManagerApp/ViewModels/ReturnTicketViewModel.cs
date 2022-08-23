namespace EventManagerApp.ViewModels
{
    public class ReturnTicketViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ReturnTicketViewModel(int id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public ReturnTicketViewModel()
        {

        }
    }
}