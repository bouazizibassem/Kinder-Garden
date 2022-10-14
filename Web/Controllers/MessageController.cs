////using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Web.ViewModels;
using Core.Entities;
using AutoMapper;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Web.Controllers
{
    public class MessageController : Controller
    {
        private const string SessionIsAdmin = "IsAdmin";
        private const string SessionID = "UserID";
        private readonly IHostingEnvironment _hosting;
        private readonly IUnitOfWork<Msg> _msg;
        private const string SessionEmail = "EmailID";
        private readonly IUnitOfWork<PortfolioItem> _portfolio;
        public MessageController(IUnitOfWork<Msg> msg, IHostingEnvironment hosting, IUnitOfWork<PortfolioItem> portfolio)

        {
            _portfolio = portfolio;
            _msg = msg;
            _hosting = hosting;

        }



        public IActionResult Index(int id)
        {


            if (this.HttpContext.Session.GetString(SessionIsAdmin).Equals("true"))
            {
                var Msgs = _msg.Entity.GetAll();

                return View(Msgs);
            }
            else


            {
                var Msgs = _msg.Entity.GetAll();
                return View(Msgs.Where(x => x.ParentEmail.Equals(this.HttpContext.Session.GetString(SessionEmail))));
            }

        }
        [HttpGet]
        public IActionResult Repport()
        {
            var model = new MsgViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]



        public IActionResult Repport(MsgViewModel model, int? id)
        {
            if (ModelState.IsValid)
            {

                var portfolioItem = _portfolio.Entity.GetById(id);
                Msg Msg = new Msg
                {
                    ParentEmail = HttpContext.Session.GetString(SessionEmail),

                    MailGardin = portfolioItem.EmailGaredn,
                    Subject = model.Subject,
                    Message = model.Message,





                };




                SendEmail(model.MailGardin, model.Message, model.Subject);




                _msg.Entity.Insert(Msg);
                _msg.Save();
                return RedirectToAction("Index", "Home");


            }
            return View(model);
        }
        public void SendEmail(string emailID, string Message, string Subject)
        {




            //var link = "/Login/VerifyAccount/"+id;


            //var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Message;




            var fromEmail = new MailAddress("aymenzouaoui97@gmail.com", Subject);
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Catvcatv11"; // Replace with actual password
            string subject = Subject;

            string body = link;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }


    }




}
