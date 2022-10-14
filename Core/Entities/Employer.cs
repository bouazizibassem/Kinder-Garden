using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Employer :EntityBase
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Age { get; set; }
        public string Date_of_Birth { get; set; }
        public string Date_Of_Recuit { get; set; }
        public int salaire { get; set; }
        public string NumTel { get; set; }
        public string ImageUrl { get; set; }
        public string Gender { get; set; }
      
    }
}
