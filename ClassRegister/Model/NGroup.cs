using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Model
{
    public class NGroup
    {
        public virtual int GroupId { get; set; }
        public virtual string GroupName { get; set; }
        public virtual int DepId { get; set; }
    }
}
