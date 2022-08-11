﻿using System;

namespace EventManagerApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public int TicketPool { get; set; }
        public DateTime Date { get; set; }
    }
}