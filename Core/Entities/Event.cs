using System;

namespace Core.Entities
{
    public class Event : EntityBase
    {
        public Nullable<System.DateTime> DateEvent { get; set; }
        public string EventName { get; set; }
        public string DescriptionE { get; set; }
       
        public string ImageEvent { get; set; }

      
    }
}
