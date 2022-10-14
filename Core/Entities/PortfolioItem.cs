using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class PortfolioItem : EntityBase
    {
      
        public string NomJardin { get; set; }
        public string AddressJ { get; set; }
        public string NumTel { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public string EmailGaredn { get; set; }

        //public Event Events { get; set; }



        public int ResponsableID { get; set; }


        








    }
}
