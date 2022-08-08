using EventManagerApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace EventManagerApp.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Events
        public ActionResult Index()
        {
            var events = _context.Events.ToList();
            return View(events);
        }

        public ActionResult Details(int id)
        {
            var evnt = _context.Events.SingleOrDefault(e => e.Id == id);

            if (evnt == null)
                return HttpNotFound();

            return View(evnt);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Event evnt)
        {
            if (evnt.Id == 0)
                _context.Events.Add(evnt);
            else
            {
                var eventInDb = _context.Events.Single(e => e.Id == evnt.Id);

                eventInDb.Name = evnt.Name;
                eventInDb.Place = evnt.Place;
                eventInDb.Date = evnt.Date;
                eventInDb.TicketPool = evnt.TicketPool;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Events");
        }

        public ActionResult Edit(int id)
        {
            var evnt = _context.Events.SingleOrDefault(e => e.Id == id);

            if (evnt == null)
                return HttpNotFound();

            return View("New", evnt);
        }

        public ActionResult Delete(int id)
        {
            var evnt = _context.Events.Single(e => e.Id == id);

            if (evnt == null)
                return HttpNotFound();

            _context.Events.Remove(evnt);
            _context.SaveChanges();

            return RedirectToAction("Index", "Events");
        }
    }
}