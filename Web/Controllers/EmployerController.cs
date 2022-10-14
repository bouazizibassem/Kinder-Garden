using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    public class EmployerController : Controller
    {
        private const string SessionIsAdmin = "IsAdmin";
        private const string SessionID = "UserID";
        private readonly DataContext _db;
        private readonly IUnitOfWork<Employer> _employer;
#pragma warning disable CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        private readonly IHostingEnvironment _hosting;
#pragma warning restore CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'

#pragma warning disable CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        public EmployerController(IUnitOfWork<Employer> employerr, IHostingEnvironment hosting, DataContext db)
#pragma warning restore CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        {
            _employer = employerr;
            _hosting = hosting;
            _db = db;
        }

       // GET: PortfolioItems
       

        //GET: PortfolioItems/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = _employer.Entity.GetById(id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
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
        public IActionResult Create(EmployerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                Employer employer = new Employer
                {

                    First_Name = model.First_Name,
                    Last_Name = model.Last_Name,
                    Age = model.Age,
                    Date_of_Birth = model.Date_of_Birth,
                    Date_Of_Recuit = model.Date_Of_Recuit,
                    salaire = model.salaire,
                    NumTel = model.NumTel,
                    Gender = model.Gender,
                    ImageUrl = model.File.FileName,


                };

                _employer.Entity.Insert(employer);
                _employer.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        //GET: PortfolioItems/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = _employer.Entity.GetById(id);
            if (employer == null)
            {
                return NotFound();
            }

            EmployerViewModel employerViewModel = new EmployerViewModel
            {
                Id = employer.Id,

                First_Name = employer.First_Name,
                Last_Name = employer.Last_Name,
                Age = employer.Age,
                Date_of_Birth = employer.Date_of_Birth,
                Date_Of_Recuit = employer.Date_Of_Recuit,
                salaire = employer.salaire,
                NumTel = employer.NumTel,
                Gender = employer.Gender,


            };

            return View(employerViewModel);
        }

       // POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployerViewModel model)
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

                    Employer employer = new Employer
                    {
                        First_Name = model.First_Name,
                        Last_Name = model.Last_Name,
                        Age = model.Age,
                        Date_of_Birth = model.Date_of_Birth,
                        Date_Of_Recuit = model.Date_Of_Recuit,
                        salaire = model.salaire,
                        NumTel = model.NumTel,
                        Gender = model.Gender,
                        ImageUrl = model.File.FileName,
                    };

                    _employer.Entity.Update(employer);
                    _employer.Save();
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

      //  GET: PortfolioItems/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = _employer.Entity.GetById(id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        //POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _employer.Entity.Delete(id);
            _employer.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(int id)
        {
            return _employer.Entity.GetAll().Any(e => e.Id == id);
        }

        public IActionResult Index()
        {


            

         
                var Porfoilios = _employer.Entity.GetAll();
                return View(Porfoilios.Where(x => x.Id.Equals(this.HttpContext.Session.GetInt32(SessionID))));
     





          



        }
      [HttpGet]
        public async Task<IActionResult> Index(string Tearm)
        {

          //  ViewData["EmplDetails"] = Tearm;
            var xD = from x in _db.Employers select x;

            if (!string.IsNullOrEmpty(Tearm))
            {
                xD = xD.Where(x => x.Date_of_Birth.Contains(Tearm) || x.NumTel.Contains(Tearm) || x.First_Name.Contains(Tearm) || x.Age.ToString().Contains(Tearm));


            }
            return View(await xD.AsNoTracking().ToListAsync());



        }
       
       


    }
}
