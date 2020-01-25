using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Mappings
{
    public class NSubjectGroupAttachMapping: ClassMap<Model.NSubjectGroupAttach>
    {
        public NSubjectGroupAttachMapping()
        {
            Id(x => x.Id);
            Map(x => x.GroupId).Not.Nullable();
            Map(x => x.SubId).Not.Nullable();
            Table("SGAttach");
        }
    }
}
