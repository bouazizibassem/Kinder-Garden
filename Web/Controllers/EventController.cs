using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    public class EventController : Controller

    {
        private const string SessionID = "UserID";

        private readonly IUnitOfWork<Event> _event;
#pragma warning disable CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        private readonly IHostingEnvironment _hosting;
#pragma warning restore CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'

#pragma warning disable CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        public EventController(IUnitOfWork<Event> eventt, IHostingEnvironment hosting)
#pragma warning restore CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        {
            _event = eventt;
            _hosting = hosting;
        }

       // GET: PortfolioItems
        public IActionResult Index(int id)
        {
            var Porfoilios = _event.Entity.GetAll();



      
            return View(Porfoilios);


        }

        //GET: PortfolioItems/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventItem = _event.Entity.GetById(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            return View(eventItem);
        }


        //GET: PortfolioItems/Create
        public IActionResult Create()
        {

            return View();
        }

        //POST: PortfolioItems/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                Event eventItem = new Event
                {
                    Id = (int)HttpContext.Session.GetInt32(SessionID),
                    EventName = model.EventName,
                    DateEvent = model.DateEvent,
                    DescriptionE = model.DescriptionE,
                    ImageEvent = model.File.FileName


                };

                _event.Entity.Insert(eventItem);
                _event.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

       // GET: PortfolioItems/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventItem = _event.Entity.GetById(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            EventViewModel portfolioViewModel = new EventViewModel
            {
                Id = eventItem.Id,
                DescriptionE = eventItem.DescriptionE,
                ImageEvent = eventItem.ImageEvent,
                EventName = eventItem.EventName,
                DateEvent = eventItem.DateEvent

            };

            return View(portfolioViewModel);
        }

        //POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EventViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    Event eventItem = new Event
                    {
                        Id = model.Id,
                        EventName = model.EventName,
                        DateEvent = model.DateEvent,
                        DescriptionE = model.DescriptionE,
                        ImageEvent = model.File.FileName
                    };

                    _event.Entity.Update(eventItem);
                    _event.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

       // GET: PortfolioItems/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventItem = _event.Entity.GetById(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            return View(eventItem);
        }

       // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _event.Entity.Delete(id);
            _event.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(int id)
        {
            return _event.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
