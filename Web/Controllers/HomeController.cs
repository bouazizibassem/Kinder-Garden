using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.ViewModels;
using User = Core.Entities.User;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Abonner> _abn;
        private const string SessionID = "UserID";

        private readonly IUnitOfWork<User> _User;
        private readonly IUnitOfWork<PortfolioItem> _portfolio;
        private readonly IUnitOfWork<Event> _event;
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _db;

        public HomeController(
            IUnitOfWork<User> user,
             IUnitOfWork<Event> events,
            IUnitOfWork<PortfolioItem> portfolio, ILogger<HomeController> logger ,DataContext db)
        {
            _User = user;
            _portfolio = portfolio;
            _event = events;
            _logger = logger;
            _db = db;

        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {

                PortfolioItems = _portfolio.Entity.GetAll().ToList(),


            };

            return View(homeViewModel);
        }
       
      /*  public async Task<IActionResult> Index(string Tearm)
        {
            var Porfoilios = _portfolio.Entity.GetAll();

            var homeViewModel = new HomeViewModel
            {

                PortfolioItems = (List<PortfolioItem>)Porfoilios.Where(x => x.NomJardin.Contains(Tearm))
                


            };

           
            return View(homeViewModel);

        }
        */

       
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Faq()
        {
            return View();
        }



        public IActionResult IndexEvent()
        {
            var eventViewModel = new EventViewModel
            {


                Events = _event.Entity.GetAll().ToList(),
            };
            return View(eventViewModel);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        private bool EventItemExists(int id)
        {
            return _event.Entity.GetAll().Any(e => e.Id == id);
        }

        
    }
    
}