using EventManagerApp.Models;
using EventManagerApp.Services;
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
            var events = _eventService.EventsToList();

            return View(events);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Event evnt)
        {
            _eventService.Save(evnt);

            return RedirectToAction("Index", "Events");
        }

        public ActionResult Edit(int id)
        {
            var evnt = _eventService.GetEventById(id);

            if (evnt == null)
            {
                return HttpNotFound();
            }

            return View("New", evnt);
        }

        public ActionResult Delete(int id)
        {
            _eventService.Delete(id);

            return RedirectToAction("Index", "Events");
        }

        public ActionResult Details(int id)
        {
            var evnt = _eventService.GetEventById(id);

            if (evnt == null)
            {
                return HttpNotFound();
            }

            return View(evnt);
        }
    }
}