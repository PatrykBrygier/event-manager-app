using System.ComponentModel.DataAnnotations;

namespace EventManagerLibrary.ViewModels
{
    public class ReturnViewModel
    {
        [Display(Name = "Ticket ID")]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "E-mail Adress")]
        public string Email { get; set; }
    }
}