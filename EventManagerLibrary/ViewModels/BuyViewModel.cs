using System.ComponentModel.DataAnnotations;

namespace EventManagerLibrary.ViewModels
{
    public class BuyViewModel
    {
        public int EventId { get; set; }
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-mail Adress is required")]
        [EmailAddress(ErrorMessage = "Invalid E-mail Address")]
        [Display(Name = "E-mail Adress")]
        public string Email { get; set; }
    }
}