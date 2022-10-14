using System;

namespace Core.Entities
{
    
    public  class User : EntityBase
    {
        public string ConfirmPassword { get; set; }
        
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string DateOfBirth { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public System.Guid ActivationCode { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
       
        public string  UserTel { get; set; }
        public string Username { get; set; }
        public string Gender { set; get; }
        public bool Admin { set; get; }

    




    }
    public enum Gender
    {
        Male = 1 << 1,
        Female = 1 << 2
    }
}
