using EventManagerLibrary.Services;
using EventManagerLibrary.ViewModels;
using Microsoft.AspNet.Identity;
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
            return View(tickets);
        }

        public ActionResult Buy(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                var viewModel = _ticketService.NewBuyViewModel(id);
                return View(viewModel);
            }
            else
            {
                var email = User.Identity.GetUserName();
                _ticketService.SaveTicketLoggedUser(id, email);
                return RedirectToAction("MyTickets", "Tickets");
            }
        }

        [Authorize]
        public ActionResult MyTickets()
        {
            var email = User.Identity.GetUserName();
            var tickets = _ticketService.UserTicketsToList(email);
            return View(tickets);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BuyViewModel buyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Buy", buyViewModel);
            }

            _ticketService.SaveTicket(buyViewModel);

            return RedirectToAction("Index", "Events");
        }

        public ActionResult Return()
        {
            return View("Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ReturnViewModel returnViewModel)
        {
            _ticketService.RemoveTicket(returnViewModel);
            return RedirectToAction("Index", "Events");
        }
    }
}