using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Mappings
{
    public class NSubjectMapping: ClassMap<Model.NSubject>
    {
        public NSubjectMapping()
        {
            Id(x => x.SubId);
            Map(x => x.SubName).Not.Nullable();
            Map(x => x.TeacherId).Not.Nullable();
            Table("Subject");
        }
    }
}
