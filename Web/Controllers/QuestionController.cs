using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    public class QuestionController : Controller
    {
        private const string SessionID = "UserID";

        private const string SessionUsername = "Username";

        private readonly IUnitOfWork<Questions> _question;
#pragma warning disable CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        private readonly IHostingEnvironment _hosting;
#pragma warning restore CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'

#pragma warning disable CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        public QuestionController(IUnitOfWork<Questions> questionn, IHostingEnvironment hosting)
#pragma warning restore CS0618 // 'IHostingEnvironment' est obsolète : 'This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.'
        {
            _question = questionn;
            _hosting = hosting;
        }

        // GET: QuestionItems
        public IActionResult Index(int id)
        {
            var Questionns = _question.Entity.GetAll();



            return View(Questionns);
            // return View(Questionns);


        }

        // GET: QuestionItems/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionItem = _question.Entity.GetById(id);
            if (questionItem == null)
            {
                return NotFound();
            }

            return View(questionItem);
        }


        // GET: QuestionItems/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: QuestionItems/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(QuestionsViewModel model)
        {
            if (ModelState.IsValid)
            {

                Questions questionItem = new Questions
                {

                   Id = model.id,
                    Question = model.Question,
                    QuestionIde = model.QuestionIde,
                    JardinID= (int)HttpContext.Session.GetInt32(SessionID),



                };

                _question.Entity.Insert(questionItem);
                _question.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: QuestionItems/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionItem = _question.Entity.GetById(id);
            if (questionItem == null)
            {
                return NotFound();
            }

            QuestionsViewModel portfolioViewModel = new QuestionsViewModel
            {
                id = questionItem.Id,
                Question = questionItem.Question,
                QuestionIde = questionItem.QuestionIde,



            };

            return View(portfolioViewModel);
        }

        // POST: QuestionItems/Edit Question/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, QuestionsViewModel model)
        {
            if (id != model.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    Questions questionItem = new Questions
                    {
                        Id = model.id,
                        Question = model.Question,
                        QuestionIde = model.QuestionIde,
                       
                    };

                    _question.Entity.Update(questionItem);
                    _question.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.id))
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

        // GET: QuestionItems/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionItem = _question.Entity.GetById(id);
            if (questionItem == null)
            {
                return NotFound();
            }

            return View(questionItem);
        }

        // POST: QuestionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _question.Entity.Delete(id);
            _question.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(int id)
        {
            return _question.Entity.GetAll().Any(e => e.Id == id);
        }




        ///////////////////////////////////////////////////////

        // GET: QuestionItems/Reponse/5
        public IActionResult Reponse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionItem = _question.Entity.GetById(id);
            if (questionItem == null)
            {
                return NotFound();
            }

            QuestionsViewModel portfolioViewModel = new QuestionsViewModel
            {
                id = questionItem.Id,
                Question = questionItem.Question,
                QuestionIde = questionItem.QuestionIde,
                Parent_Name=HttpContext.Session.GetString(SessionUsername),
               



        };

            return View(portfolioViewModel);
        }

        // POST: QuestionItems/Edit Reponse/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reponse(int id, QuestionsViewModel model)
        {
            if (id != model.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    Questions questionItem = new Questions
                    {
                        Id = model.id,
                        Question = model.Question,
                        QuestionIde = model.QuestionIde,
                        

                    };

                    _question.Entity.Update(questionItem);
                    _question.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.id))
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

    }
}
