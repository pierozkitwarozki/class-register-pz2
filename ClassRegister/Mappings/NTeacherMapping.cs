using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Mappings
{
    public class NTeacherMapping: ClassMap<Model.NTeacher>
    {
        public NTeacherMapping()
        {
            Id(x => x.Id);
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.Username).Not.Nullable();
            Map(x => x.Password).Not.Nullable();
            Map(x => x.Email).Not.Nullable();  
            Table("Teacher");
        }
    }
}
