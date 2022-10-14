using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class QuestionsViewModel
    {
        public int id { get; set; }
        public int QuestionIde { get; set; }
        public string Question { get; set; }
        public string Question_Reponse { get; set; }


        public string Parent_Name { get; set; }
        public int JardinID { get; set; }

    }
}
