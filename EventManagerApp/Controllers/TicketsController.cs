using EventManagerApp.Models;
using EventManagerApp.Services;
using EventManagerApp.ViewModels;
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
            var ticket = _ticketService.NewTicket(id);

            return View(ticket);
        }

        [HttpPost]
        public ActionResult Save(Ticket ticket)
        {
            _ticketService.SaveTicket(ticket);

            return RedirectToAction("Index", "Tickets");
        }

        public ActionResult Return()
        {
            return View("Delete");
        }

        [HttpPost]
        public ActionResult Delete(ReturnViewModel returnViewModel)
        {
            if (returnViewModel == null)
                return HttpNotFound();

            var ticket = _ticketService.GetTicketById(returnViewModel.Id);

            if (ticket == null)
                return HttpNotFound();

            if (_ticketService.IsTicketValid(ticket, returnViewModel))
            {
                _ticketService.RemoveTicket(ticket);
            }
            else
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index", "Tickets");
        }
    }
}