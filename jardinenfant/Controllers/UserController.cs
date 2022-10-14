using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using jardinenfant.Models;

namespace jardinenfant.Controllers
{
    public class UserController : Controller
    {
        //registrion Action 
        [HttpGet]
        public ActionResult Registration ()
        { 
            return View();
        
        }
        //registration post action

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = " IsEmailV,ActivationCode")]Useres user)
        {

            bool Status = false;
            string message = "";

            //Model validation 


            if(ModelState.IsValid){





                #region//email is already exist  
                var isExist = IsEmailExist(user.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("email exist", "EMAIL ALREADY EXIST");
                    return View(user);

                }

                #endregion



                #region// Generate Activision code


                user.ActivationCode = Guid.NewGuid();


                #endregion


                #region // password hashing 
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);

                #endregion

                #region save to data base
                using (MyDataBaseEntities dc =new MyDataBaseEntities() )
                {

                    dc.Useres.Add(user);
                    dc.SaveChanges();
                    //sensd email to user
                    sendverificationlinkemail(user.EmailID, user.ActivationCode.ToString());
                    message = "user " + "mail" + user.EmailID;
                    Status = true;

                }
                #endregion
            }
            else
            {


                message = "invalid Requet";

            }

            //Genrate Activation Code
            // password hashing 
            // save data to data base
            //sent Email to user


            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);

        }

        //verifier account 
[HttpGet]
public ActionResult VerifyAcount (string id)
        {

            bool status = false;
            using(MyDataBaseEntities dc =new MyDataBaseEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false; //confirme password issue on save change
                var v = dc.Useres.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                    if (v !=null) {
                    v.IsEmailV = true;
                    dc.SaveChanges();
                    status = true;
                }
                else
                {
                    ViewBag.message = "invalid Request ";

                }

            }
            ViewBag.Status =    status;
            return View();
        }


        //verifier mail link 
        //Login

        //login post 
        //logout
        [NonAction]public bool IsEmailExist(string emailIID)
        {

            using (MyDataBaseEntities dc = new MyDataBaseEntities())
            {
                var v = dc.Useres.Where(a => a.EmailID == emailIID).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]
        public void sendverificationlinkemail(string emailID,string activationcode)
        {

            var verifyurl = "User/verifyAccount/" + activationcode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyurl);
            var fromEmail = new MailAddress("bouazizibassem989@gmail.com ");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Catvcatv111";
            string subject = "successfulyy created";
            string body = "<br></br>nice" + "well done" + "<br></br>   < a href='" + link + "'>" +link+"</a > ";
                var stmp = new SmtpClient
                {
                    Host = "smtp.gmail.com", 
                    Port = 587,
                    EnableSsl=true,
                    DeliveryMethod=SmtpDeliveryMethod.Network,
                    UseDefaultCredentials=false,
                    Credentials =new NetworkCredential(fromEmail.Address,fromEmailPassword)

                };
            using (var message = new MailMessage(fromEmail, toEmail)
            {

                Subject = subject,
                Body = body,
                IsBodyHtml=true

            })


                stmp.Send(message);

        }
    }
}