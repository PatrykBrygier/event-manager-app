using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagerApp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Place { get; set; }

        [Display(Name = "Ticket pool")]
        public int TicketPool { get; set; }
        public DateTime Date { get; set; }
    }
}