using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Mappings
{
    public class NGradesMapping: ClassMap<Model.NGrades>
    {
        public NGradesMapping()
        {
            Id(x => x.Id);
            Map(x => x.StudentId).Not.Nullable();
            Map(x => x.SubId).Not.Nullable();
            Map(x => x.Grade).Not.Nullable();
            Table("Grades");
        }
    }
}
