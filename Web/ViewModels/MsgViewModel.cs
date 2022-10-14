using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class MsgViewModel
    {
        public int id { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string ParentEmail { get; set; }
        public string MailGardin { get; set; }
       
    }
}
