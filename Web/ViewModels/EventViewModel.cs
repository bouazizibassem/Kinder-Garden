using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class EventViewModel
    {
        public Nullable<System.DateTime> DateEvent { get; set; }
        public string EventName { get; set; }
        public string DescriptionE { get; set; }

        public IFormFile File { get; set; }
        public string ImageEvent { get; set; }
        public int Id { get; internal set; }

        public List<Event> Events { get; internal set; }
    }
}
