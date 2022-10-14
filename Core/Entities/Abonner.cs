using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Abonner : EntityBase

    {   public int userID { get; set; }
    
        public int PortfolioItemID { get; set; }
  
        public string date { get;set; }
        public string NomJardin { get; set; }



    }
}
