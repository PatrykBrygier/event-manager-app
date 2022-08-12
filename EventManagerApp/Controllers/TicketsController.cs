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