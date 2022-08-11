using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagerApp.ViewModels
{
    public class AddEventViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        [Display(Name = "Ticket Pool")]
        public int TicketPool { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}