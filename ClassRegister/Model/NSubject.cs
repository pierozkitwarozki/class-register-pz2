using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Model
{
    public class NSubject
    {
        public virtual int SubId { get; set; }
        public virtual string SubName { get; set; }
        public virtual int TeacherId { get; set; }

    }
}
