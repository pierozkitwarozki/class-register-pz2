using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Mappings
{
    public class NDepartmentMapping: ClassMap<Model.NDepartment>
    {
        public NDepartmentMapping()
        {
            Id(x => x.DepId);
            Map(x => x.DepName).Not.Nullable();
            Table("Department");
        }
    }
}
