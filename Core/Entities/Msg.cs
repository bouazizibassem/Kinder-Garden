using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Msg :EntityBase
    {

     
        public string Message { get; set; }
        public string Subject { get; set; }
        public string ParentEmail { get; set; }
        public string MailGardin { get; set; }
    }
}
