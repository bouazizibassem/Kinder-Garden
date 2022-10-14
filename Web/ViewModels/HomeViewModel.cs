using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class HomeViewModel
    {
        public UserViewModel User { get; set; }
        public List<PortfolioItem> PortfolioItems { get; set; }
        public List<User> Users { get; set; }
        public List<Event> Events { get; set; }
      
        public IEnumerable<PortfolioItem> Porfoilio { get; internal set; }
     
    }
}
