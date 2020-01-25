using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Model
{
    public class NStudent: Model.NUserM
    {
        public virtual int GroupId { get; set; }
        
    }
}
