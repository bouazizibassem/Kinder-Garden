using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Repository;
using Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Infrastructure;
using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;



namespace Web.Controllers
{
    public class LoginController : Controller


    {
        private readonly IUnitOfWork<User> _user;
        private readonly IHostingEnvironment _hosting;


        private readonly DataContext Context;
        private DataRepository<User> Repository;
        private readonly IMapper Mapper;
        private const string SessionID = "UserID";
        private const string SessionUserTel = "UserTel";
        private const string SessionFirstName = "FirstName";
        private const string SessionLastName = "LastName";
        private const string SessionDateOfBirth = "DateOfBirth";
        private const string SessionGender = "Gender";
        private const string SessionEmail = "EmailID";
        private const string SessionUsername = "Username";
        private const string SessionPassword = "Password";
        private const string SessionConfirmePassword = "ConfirmePassword";
        private const string SessionIsAdmin = "IsAdmin";
        private const string SessionAvatar = "Avatar";
        private const string SessionRole = "Role";
        

        private const string SessionIsLoggedIn = "IsLoggedIn";

        public ClaimsIdentity userIdentity { get; set; }

        public LoginController(DataContext context, IMapper mapper, IUnitOfWork<User> user, IHostingEnvironment hosting)
        {
            _hosting = hosting;
            _user = user;
            Repository = new DataRepository<User>(context);
            Context = context;
            Mapper = mapper;
            userIdentity = new ClaimsIdentity("Custom");
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["HasError"] = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserViewModel UserViewModel)
        {
            ViewData["HasError"] = true;
            User Model = new User();
            var MappedModel = Mapper.Map(UserViewModel, Model);
            var all = Repository.SelectAll().Result.ToList();
            User user = null;
            

            userIdentity.AddClaim(new Claim("type", "value"));

            foreach (var item in all)
            {
                if (item.EmailID.Trim() == Model.EmailID && item.Password.Trim() == Model.Password)
                {
                    user = item;
                    HttpContext.Session.SetInt32(SessionID, user.Id);
                    HttpContext.Session.SetString(SessionUserTel, user.UserTel);
                    HttpContext.Session.SetString(SessionFirstName, user.FirstName);
                    HttpContext.Session.SetString(SessionLastName, user.LastName);
                    HttpContext.Session.SetString(SessionDateOfBirth, user.DateOfBirth.ToString());
                    HttpContext.Session.SetString(SessionGender, user.Gender.ToString());
                    HttpContext.Session.SetString(SessionEmail, user.EmailID);
                    HttpContext.Session.SetString(SessionUsername, user.Username);
                    HttpContext.Session.SetString(SessionPassword, user.Password);
                   
                   
                    HttpContext.Session.SetString(SessionIsLoggedIn, "true");
                    if (item.Admin == true)
                    {
                        HttpContext.Session.SetString(SessionIsAdmin, "true");
                        return RedirectToAction("ParentAccount");
                    }
                    else
                    {
                        if (item.Role.Equals("Parent"))
                        {
                            HttpContext.Session.SetString(SessionIsAdmin, "false");
                            HttpContext.Session.SetString(SessionRole, "Parent");

                            return RedirectToAction("ParentAccount");
                        }

                        else
                        {
                            if (item.Role.Equals("Responsable"))
                            {
                                HttpContext.Session.SetString(SessionIsAdmin, "false");
                                HttpContext.Session.SetString(SessionRole, "Responsable");
                                return RedirectToAction("ResponsableAccount");
                            }
                            

                        }
                       
                       




                    }
                } 
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel UserViewModel)
        {
            User Model = new User();
            

            
            var MappedModel = Mapper.Map(UserViewModel, Model);
            await Repository.Insert(MappedModel);
          
       
            SendVerificationLinkEmail(UserViewModel.EmailID, UserViewModel.Id);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult UserAccount()
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Username = HttpContext.Session.GetString(SessionUsername);


            return View(userViewModel);
        }
       

        [HttpGet]
        public IActionResult AdminAccount()
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Username = HttpContext.Session.GetString(SessionUsername);

            return View(userViewModel);
        }
        public IActionResult ParentAccount()
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Id =(int) HttpContext.Session.GetInt32(SessionID);
            userViewModel.UserTel = HttpContext.Session.GetString(SessionUserTel);
            userViewModel.Username = HttpContext.Session.GetString(SessionUsername);
            userViewModel.EmailID = HttpContext.Session.GetString(SessionEmail);
            userViewModel.Password = HttpContext.Session.GetString(SessionPassword);
            userViewModel.DateOfBirth = HttpContext.Session.GetString(SessionDateOfBirth);
            userViewModel.Gender = HttpContext.Session.GetString(SessionGender);
            userViewModel.Role = HttpContext.Session.GetString(SessionRole);
            userViewModel.FirstName = HttpContext.Session.GetString(SessionFirstName);
            userViewModel.LastName = HttpContext.Session.GetString(SessionLastName);
            userViewModel.FirstName = HttpContext.Session.GetString(SessionFirstName);
            userViewModel.ConfirmPassword=HttpContext.Session.GetString(SessionConfirmePassword);
            userViewModel.Avatar = HttpContext.Session.GetString(SessionAvatar);



            return View(userViewModel);
        }

        public IActionResult ResponsableAccount()
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Id = (int)HttpContext.Session.GetInt32(SessionID);
            userViewModel.UserTel = HttpContext.Session.GetString(SessionUserTel);
            userViewModel.Username = HttpContext.Session.GetString(SessionUsername);
            userViewModel.EmailID = HttpContext.Session.GetString(SessionEmail);
            userViewModel.Password = HttpContext.Session.GetString(SessionPassword);
            userViewModel.DateOfBirth = HttpContext.Session.GetString(SessionDateOfBirth);
            userViewModel.Gender = HttpContext.Session.GetString(SessionGender);
            userViewModel.Role = HttpContext.Session.GetString(SessionRole);
            userViewModel.FirstName = HttpContext.Session.GetString(SessionFirstName);
            userViewModel.LastName = HttpContext.Session.GetString(SessionLastName);
            userViewModel.ConfirmPassword = HttpContext.Session.GetString(SessionConfirmePassword);
            userViewModel.Avatar = HttpContext.Session.GetString(SessionAvatar);




            return View(userViewModel);
        }

        [HttpPost]
       /* public async Task<IActionResult> ChangeUserDetails(UserViewModel UserViewModel)
        {
            User Model = new User();
            var MappedModel = Mapper.Map(UserViewModel, Model);
            Model.Password = HttpContext.Session.GetString(SessionPassword);
            await Repository.Update(Model);
            return RedirectToAction("Index ", "Home");
        }
       */
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _user.Entity.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        // GET: User/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _user.Entity.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel = new UserViewModel
            {
                Id=user.Id,
                FirstName = user.FirstName,
                Username=user.Username,
                Avatar = user.Avatar,
                LastName=user.LastName,
                Password = user.Password,
                EmailID = user.EmailID,
                Gender = user.Gender,
                Role = user.Role,
                UserTel=user.UserTel,
                DateOfBirth = user.DateOfBirth,
                ConfirmPassword=user.ConfirmPassword

            };

            return View(userViewModel);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int  id, UserViewModel model)
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

                    User user = new User
                    {

                        Id=model.Id,
                        FirstName = model.FirstName,
                        Avatar = model.Avatar,
                        Username=model.Username,
                        LastName=model.LastName,
                        Password = model.Password,
                        EmailID = model.EmailID,
                        Gender = model.Gender,
                        Role = model.Role,
                        UserTel=model.UserTel,
                        DateOfBirth = model.DateOfBirth,
                        ConfirmPassword=model.ConfirmPassword,
                       
                    };

                    _user.Entity.Update(user);
                    _user.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(model.Id))
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

        // GET: Users/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _user.Entity.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _user.Entity.Delete(id);
            _user.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _user.Entity.GetAll().Any(e => e.Id == id);
        }



        // GET: User
        public IActionResult Index(int id)
        {
            return View(_user.Entity.GetAll());
        }
        
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, int id)
        {

           //var verifyUrl = "/Login/VerifyAccount/" + id;
            var link = "http://localhost:44362/Login/VerifyAccount/" +id;

            var fromEmail = new MailAddress("Aymenzouaoui97@gmail.com", "Welcome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Catvcatv11"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                " successfully created. Please click on the below link to verify your account" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

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
        /*
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (DataContext dc = new DataContext())
            {
               // dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                                                                // Confirm password does not match issue on save changes
                var v = dc.User.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }*/
    }
}