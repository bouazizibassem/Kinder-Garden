using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Infrastructure;

namespace Web.Controllers
{
    public class PortfolioItemsController : Controller

    {
        private readonly DataContext _db;
        private const string SessionIsAdmin = "IsAdmin";
        private const string SessionID = "UserID";
        private readonly IUnitOfWork<PortfolioItem> _portfolio;
        private readonly IUnitOfWork<Abonner> _abn;
        private readonly IUnitOfWork<Image> _image;




#pragma warning disable CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        private readonly IHostingEnvironment _hosting;
      
#pragma warning restore CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'

#pragma warning disable CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        public PortfolioItemsController(IUnitOfWork<PortfolioItem> portfolio, IHostingEnvironment hosting,IUnitOfWork<Abonner>abn,DataContext db, IUnitOfWork<Image> image)
#pragma warning restore CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        {
            _portfolio = portfolio;
            _hosting = hosting;
            _abn = abn;
            _db = db;
            _image = image;
        }

        // GET: PortfolioItems
        public IActionResult Index(int id)
        {


            if (this.HttpContext.Session.GetString(SessionIsAdmin).Equals("true"))
            {
                var Porfoilios = _portfolio.Entity.GetAll();

                return View(Porfoilios);
            }
            else


            {
                var Porfoilios = _portfolio.Entity.GetAll();
                return View(Porfoilios.Where(x => x.ResponsableID.Equals(this.HttpContext.Session.GetInt32(SessionID))));
            }

        }
        [HttpGet]
        public async Task<IActionResult> Index(string Tearm){

            ViewData["GetGardinDetails"] = Tearm;
            var xD = from x in _db.portfolioItems select x;

            if (!string.IsNullOrEmpty(Tearm))
            {
                xD = xD.Where(x => x.NomJardin.Contains(Tearm)|| x.AddressJ.Contains(Tearm));


            }
            return View(await xD.AsNoTracking().ToListAsync());



        }



        [HttpGet]

        // GET: PortfolioItems/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }


        // GET: PortfolioItems/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: PortfolioItems/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PortfolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                PortfolioItem portfolioItem = new PortfolioItem
                {
                    ResponsableID = (int)HttpContext.Session.GetInt32(SessionID),
                    NumTel = model.NumTel,
                    NomJardin = model.NomJardin,
                    AddressJ = model.AddressJ,
                    Description = model.Description,
                    ImageUrl = model.File.FileName,


                };

                _portfolio.Entity.Insert(portfolioItem);
                _portfolio.Save();
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

            var portfolioItem = _portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            PortfolioViewModel portfolioViewModel = new PortfolioViewModel
            {
                Id = portfolioItem.Id,
                ResponsableID = portfolioItem.Id,
                Description = portfolioItem.Description,
                ImageUrl = portfolioItem.ImageUrl,
                NumTel = portfolioItem.NumTel,
                NomJardin = portfolioItem.NomJardin,
                AddressJ = portfolioItem.AddressJ,

            };

            return View(portfolioViewModel);
        }

        // POST: PortfolioItems/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PortfolioViewModel model)
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

                    PortfolioItem portfolioItem = new PortfolioItem
                    {
                        Id = model.Id,
                        ResponsableID = model.Id,
                        NomJardin = model.NomJardin,
                        NumTel = model.NumTel,
                        AddressJ = model.AddressJ,
                        Description = model.Description,
                        ImageUrl = model.File.FileName,
                        EmailGaredn=model.EmailGaredn,

                    };

                    _portfolio.Entity.Update(portfolioItem);
                    _portfolio.Save();
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

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _portfolio.Entity.Delete(id);
            _portfolio.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(int id)
        {
            return _portfolio.Entity.GetAll().Any(e => e.Id == id);
        }



        //////////////subscribe
        ///
        public IActionResult Subscribe(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.Entity.GetById(id);

            Abonner abonner = new Abonner

            {
                userID = (int)HttpContext.Session.GetInt32(SessionID),
                PortfolioItemID = portfolioItem.Id,
                date = DateTime.UtcNow.ToString(),
                 NomJardin= portfolioItem.NomJardin



            };

            _abn.Entity.Insert(abonner);
            _abn.Save();
            return RedirectToAction("Index", "Home");


            return View(abonner);
        }


        public IActionResult SubIndex(int id)
        {

            var portfolioItem = _portfolio.Entity.GetById(id);

            var Porfoilios = _abn.Entity.GetAll();
            return View(Porfoilios.Where(x => x.PortfolioItemID.Equals(portfolioItem.Id)));
       

        }

        // GET: sUBS/Delete/5
        public IActionResult SubsDelete(int? id)
               {
                   if (id == null)
                   {
                       return NotFound();
                   }

                   var subs = _abn.Entity.GetById(id);
                   if (subs == null)
                   {
                       return NotFound();
                   }

                   return View(subs);
               }

               // POST: sUBSDelete/5
               [HttpPost, ActionName("DeleteS")]
               [ValidateAntiForgeryToken]
               public IActionResult SubsDelete(int id)
               {
                   _abn.Entity.Delete(id);
                   _abn.Save();
                   return RedirectToAction(nameof(SubIndex));
               }

       



              ///Galerie Insert
              ///


              public IActionResult Image(ImageViewModel model, int id)
              {
                  if (ModelState.IsValid)
                  {
                      if (model.File != null)
                      {
                          string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                          string fullPath = Path.Combine(uploads, model.File.FileName);
                          model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                      }

                      Image ImageItem = new Image
                      {
                          ResponsableID = (int)HttpContext.Session.GetInt32(SessionID),

                          NameImage = model.NameImage,
                          DateInsert = model.DateInsert,

                          ImageG_Url = model.File.FileName,


                      };

                      _image.Entity.Insert(ImageItem);
                      _image.Save();
                      return RedirectToAction(nameof(Index));
                  }

                  return View(model);
              }




              // GET: PortfolioItems/Edit/5
              public IActionResult EditImage(int? id)
              {
                  if (id == null)
                  {
                      return NotFound();
                  }

                  var ImageItem = _image.Entity.GetById(id);
                  if (ImageItem == null)
                  {
                      return NotFound();
                  }

                  ImageViewModel ImageViewModel = new ImageViewModel
                  {
                      id = ImageItem.Id,
                      ResponsableID = ImageItem.Id,
                      DateInsert = ImageItem.DateInsert,
                      ImageG_Url = ImageItem.ImageG_Url,


                  };

                  return View(ImageViewModel);
              }

              // POST: PortfolioItems/Edit/5

              [HttpPost]
              [ValidateAntiForgeryToken]
              public IActionResult EditImage(int id, ImageViewModel model)
              {
                  if (id != model.id)
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

                          Image ImageItem = new Image
                          {
                              Id = model.id,
                              ResponsableID = model.id,
                             ImageG_Url = model.ImageG_Url,
                              DateInsert = model.DateInsert,
                              NameImage = model.NameImage,

                          };

                          _image.Entity.Update(ImageItem);
                          _image.Save();
                      }
                      catch (DbUpdateConcurrencyException)
                      {
                          if (ImageExists(model.id))
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

             /* 

              // GET: ImageItems/Delete/5
             public IActionResult DeleteC(int? id)
               {
                   if (id == null)
                   {
                       return NotFound();
                   }

                   var imageItem = _image.Entity.GetById(id);
                   if (imageItem == null)
                   {
                       return NotFound();
                   }

                   return View(imageItem);
               }

               // POST: PortfolioItems/Delete/5
               [HttpPost, ActionName("Delete")]
               [ValidateAntiForgeryToken]
               public IActionResult DeleteC(int id)
               {
                   _image.Entity.Delete(id);
                   _image.Save();
                   return RedirectToAction(nameof(Index));
               }

               public async Task<IActionResult> IndexImage()
               {
                   var homeViewModel = new ImageViewModel
                   {

                       Images = _image.Entity.GetAll().ToList(),


                   };

                   return View(homeViewModel);
               }

               */



        public IActionResult DeleteIm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = _image.Entity.GetById(id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("DeletIm")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmedIm(int id)
        {
            _image.Entity.Delete(id);
            _image.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
            return _image.Entity.GetAll().Any(e => e.Id == id);
        }

    }

}
