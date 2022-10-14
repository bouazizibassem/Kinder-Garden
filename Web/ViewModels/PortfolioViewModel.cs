using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class PortfolioViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string NumTel { get; set; }
        public string AddressJ { get; set; }
        public string NomJardin { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile File { get; set; }
        public int ResponsableID { get; set; }

        public string EmailGaredn { get; set; }


    }
}
