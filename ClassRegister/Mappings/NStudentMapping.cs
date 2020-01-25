using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Mappings
{
     public class NStudentMapping: ClassMap<Model.NStudent>
    {
        public NStudentMapping()
        {
            //Mapowanie studenta na studenta z bazy
            Id(x => x.Id);
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.Username).Not.Nullable();
            Map(x => x.Password).Not.Nullable();
            Map(x => x.Email).Not.Nullable();
            Map(x => x.GroupId).Not.Nullable();            
            Table("Student");
        }
    }
}
