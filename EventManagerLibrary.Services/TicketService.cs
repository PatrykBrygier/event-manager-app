using EventManagerLibrary.Models;
using EventManagerLibrary.Repositories;
using EventManagerLibrary.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagerLibrary.Services
{
    public class TicketService : ITicketService
    {
        private ITicketRepository _ticketRepository;
        private IEventRepository _eventRepository;


        public TicketService(ITicketRepository ticketRepository, IEventRepository eventRepository)
        {
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
        }

        public IEnumerable<TicketListModel> TicketsToList(int id)
        {
            var dtos = _ticketRepository.GetTicketListById(id);
            return dtos.Select(dto => new TicketListModel(
                dto.Id,
                dto.Customer,
                dto.Event));
        }

        public IEnumerable<TicketListModel> UserTicketList(string email)
        {
            var dtos = _ticketRepository.GetTicketListByEmail(email);
            return dtos.Select(dto => new TicketListModel(
                dto.Id,
                dto.Customer,
                dto.Event));
        }

        public void SaveTicketLoggedUser(UserModel userModel)
        {
            var evnt = _eventRepository.GetEventById(userModel.EventId);
            var customer = _ticketRepository.GetCustomerByEmail(userModel.Email);
            var ticket = new Ticket
            {
                Event = evnt,
                Customer = customer
            };

            _ticketRepository.CreateTicketWithLoggedUser(ticket);
        }

        public void SaveTicket(TicketModel ticketModel)
        {
            var evnt = _eventRepository.GetEventById(ticketModel.EventId);
            var ticket = new Ticket
            {
                Event = evnt,
                Customer = new Customer
                {
                    FirstName = ticketModel.FirstName,
                    LastName = ticketModel.LastName,
                    Email = ticketModel.Email
                }
            };

            _ticketRepository.CreateTicket(ticket);
        }

        public void RemoveTicket(ReturnTicketModel returnTicketModel)
        {
            var ticket = _ticketRepository.GetTicketById(returnTicketModel.Id);

            if (returnTicketModel.FirstName == ticket.Customer.FirstName &&
                returnTicketModel.LastName == ticket.Customer.LastName &&
                returnTicketModel.Email == ticket.Customer.Email)
            {
                _ticketRepository.DeleteTicket(ticket);
            }
            else
            {
                throw new Exception("There is no such event");
            }
        }
    }
}