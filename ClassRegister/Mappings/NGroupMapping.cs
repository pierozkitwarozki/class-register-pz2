using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Mappings
{
    public class NGroupMapping: ClassMap<Model.NGroup>
    {
        public NGroupMapping()
        {
            Id(x => x.GroupId);
            Map(x => x.GroupName).Not.Nullable();
            Map(x => x.DepId).Not.Nullable();
            Table("[Group]");
        }
    }
}
