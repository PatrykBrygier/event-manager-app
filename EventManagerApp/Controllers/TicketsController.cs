using EventManagerApp.ViewModels;
using EventManagerLibrary.Services;
using EventManagerLibrary.Services.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace EventManagerApp.Controllers
{
    public class TicketsController : Controller
    {
        private ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: Tickets
        public ActionResult Index(int id)
        {
            var tickets = _ticketService.TicketsToList(id);
            var viewModel = tickets.Select(t => new TicketListViewModel(
                t.Id,
                t.Customer,
                t.Event));

            return View(viewModel);
        }

        //public ActionResult Buy(int id)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        var viewModel = new BuyTicketViewModel
        //        {
        //            EventId = id
        //        };

        //        return View(viewModel);
        //    }
        //    else
        //    {
        //        var email = User.Identity.GetUserName();
        //        _ticketService.SaveTicketLoggedUser(id, email);
        //        return RedirectToAction("MyTickets", "Tickets");
        //    }
        //}

        public ActionResult Buy(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                var viewModel = new BuyTicketViewModel
                {
                    EventId = id
                };

                return View(viewModel);
            }
            else
            {
                var email = User.Identity.GetUserName();
                var userModel = new UserModel
                {
                    EventId = id,
                    Email = email
                };

                _ticketService.SaveTicketLoggedUser(userModel);
                return RedirectToAction("MyTickets", "Tickets");
            }
        }

        [Authorize]
        public ActionResult MyTickets()
        {
            var email = User.Identity.GetUserName();
            var tickets = _ticketService.UserTicketList(email);
            var viewModel = tickets.Select(t => new TicketListViewModel(
                t.Id,
                t.Customer,
                t.Event));

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BuyTicketViewModel buyTicketViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Buy", buyTicketViewModel);
            }

            var ticketModel = new TicketModel(
                buyTicketViewModel.EventId,
                buyTicketViewModel.FirstName,
                buyTicketViewModel.LastName,
                buyTicketViewModel.Email);

            _ticketService.SaveTicket(ticketModel);

            return RedirectToAction("Index", "Events");
        }

        public ActionResult Return()
        {
            return View("Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ReturnTicketViewModel returnTicketViewModel)
        {
            var ticketModel = new ReturnTicketModel(
                returnTicketViewModel.Id,
                returnTicketViewModel.FirstName,
                returnTicketViewModel.LastName,
                returnTicketViewModel.Email
                );

            _ticketService.RemoveTicket(ticketModel);
            return RedirectToAction("Index", "Events");
        }
    }
}