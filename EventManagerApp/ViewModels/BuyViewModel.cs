using System.ComponentModel.DataAnnotations;

namespace EventManagerApp.ViewModels
{
    public class BuyViewModel
    {
        public int EventId { get; set; }
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "E-mail Adress")]
        public string Email { get; set; }
    }
}