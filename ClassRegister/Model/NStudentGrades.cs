using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Model
{
    public class NStudentGrades
    {
        public  string SubName { get; set; }
        public  float Grade { get; set; }
        public NStudentGrades(string subName, float grade)
        {
            this.SubName = subName;
            this.Grade = grade;
        }
    }
}
