using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class ImageViewModel
    {public int id { get; set; }
        public string NameImage { get; set; }
        public string DateInsert { get; set; }
        public string ImageG_Url { get; set; }

        
        public IFormFile File { get; set; }
        public int ResponsableID { get; set; }
        public List<Image> Images { get; set; }
        public IEnumerable<Image> Image_s{ get; internal set; }


    }
}
