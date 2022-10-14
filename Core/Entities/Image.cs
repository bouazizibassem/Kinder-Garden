using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Image :EntityBase
    {
        public string NameImage { get; set; }   
        public string DateInsert { get; set; }
        public string ImageG_Url { get; set; }
        public int ResponsableID { get; set; }
    }

}
