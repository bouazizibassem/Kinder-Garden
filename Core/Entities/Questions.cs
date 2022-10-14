using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Questions:EntityBase
    {

        
        public int QuestionIde { get; set; }
        public string Question { get; set; }
        public string Question_Reponse { get; set; }


        public string Parent_Name { get; set; }
        public int JardinID { get; set; }
    }
}
