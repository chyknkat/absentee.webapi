using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Absentee.WebApi.Data.Domain;
using FluentNHibernate.Mapping;

namespace Absentee.WebApi.Data.DomainPersistence
{
    public class AbsenceMap : ClassMap<Absence>
    {
        public AbsenceMap()
        {
            Table("Absentee.Absence");
            LazyLoad();

            Id(c => c.Id).Column("Id").GeneratedBy.Identity();

            Map(c => c.StartDate).Column("StartDate").Not.Nullable();
            Map(c => c.EndDate).Column("EndDate").Not.Nullable();
            Map(c => c.Comments).Column("Comments").Nullable();
            Map(c => c.IsActive).Column("Active").Not.Nullable();

            References(c => c.User).Column("UserId").Not.Nullable();
           
        }
    }
}