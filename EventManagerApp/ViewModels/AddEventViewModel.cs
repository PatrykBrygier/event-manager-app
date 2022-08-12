using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagerApp.ViewModels
{
    public class AddEventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name of the Event is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location of the Event is required")]
        public string Place { get; set; }

        [Required(ErrorMessage = "Number of tickets is required")]
        [Display(Name = "Ticket Pool")]
        public int TicketPool { get; set; }

        [Required(ErrorMessage = "Event date is required")]
        public DateTime Date { get; set; }
    }
}