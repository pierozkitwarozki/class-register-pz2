using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Model
{
    public class NGrades
    {
        public virtual int Id { get; set; }
        public virtual int StudentId { get; set; }
        public virtual int SubId { get; set;  }
        public virtual float Grade { get; set; }
    }
}
