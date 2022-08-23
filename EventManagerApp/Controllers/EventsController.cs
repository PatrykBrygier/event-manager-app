using EventManagerApp.ViewModels;
using EventManagerLibrary.Services;
using EventManagerLibrary.Services.Models;
using System.Linq;
using System.Web.Mvc;

namespace EventManagerApp.Controllers
{
    public class EventsController : Controller
    {
        private IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: Events
        public ActionResult Index()
        {
            var events = _eventService.ReturnEventsList();
            var viewModel = events.Select(e => new EventListViewModel(
                e.Id,
                e.Name,
                e.Place,
                e.TicketPool,
                e.Date));

            return View(viewModel);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(AddEventViewModel addEventViewModel)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                return View("New");
            }

            var viewModel = new EventModel(
                addEventViewModel.Id,
                addEventViewModel.Name,
                addEventViewModel.Place,
                addEventViewModel.TicketPool,
                addEventViewModel.Date);

            _eventService.SaveEvent(viewModel);

            return RedirectToAction("Index", "Events");
        }

        public ActionResult Edit(int id)
        {
            var evnt = _eventService.ReturnEventById(id);
            var viewModel = new EditEventViewModel(
                evnt.Id,
                evnt.Name,
                evnt.Place,
                evnt.TicketPool,
                evnt.Date);

            return View("Edit", viewModel);
        }

        public ActionResult Delete(int id)
        {
            _eventService.DeleteEvent(id);

            return RedirectToAction("Index", "Events");
        }

        public ActionResult Details(int id)
        {
            var evnt = _eventService.ReturnEventById(id);

            var viewModel = new EventDetailsViewModel(
                evnt.Id,
                evnt.Name,
                evnt.Place,
                evnt.TicketPool,
                evnt.Date);

            return View(viewModel);
        }
    }
}