using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;


namespace Web.ViewModels
{
   
    public class UserViewModel
    {
        [Key]
        public int UserID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        public string DateOfBirth { set; get; }

        [Display(Name = "Gender")]
        public string Gender { set; get; }
        [Display(Name = "Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        public string EmailID { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Admin")]
        public bool Admin { set; get; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        public int Id { get; set; }
        public string UserTel { get; set; }
        public IFormFile File { get; set; }
    }
}

