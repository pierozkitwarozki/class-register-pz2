﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Model
{
    public class NSubjectGroupAttach
    {
        public virtual int Id {get; set;}
        public virtual int GroupId { get; set; }
        public virtual int SubId { get; set; }
    }
}
