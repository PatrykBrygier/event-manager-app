using EventManagerLibrary.Services;
using EventManagerLibrary.ViewModels;
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
            var events = _eventService.EventsToList();
            return View(events);
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
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();

                return View("New");
            }

            _eventService.Save(addEventViewModel);

            return RedirectToAction("Index", "Events");
        }

        public ActionResult Edit(int id)
        {
            var evnt = _eventService.GetEventById(id);
            var viewModel = _eventService.NewViewModel(evnt);

            if (evnt == null)
            {
                return HttpNotFound();
            }

            return View("New", viewModel);
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