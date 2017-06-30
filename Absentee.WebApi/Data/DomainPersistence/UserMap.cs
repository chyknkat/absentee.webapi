using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Absentee.WebApi.Data.Domain;
using FluentNHibernate.Mapping;

namespace Absentee.WebApi.Data.DomainPersistence
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Absentee.User");
            LazyLoad();

            Id(c => c.Id).Column("Id").GeneratedBy.Identity();
            Map(c => c.FirstName).Column("FirstName").Not.Nullable();
            Map(c => c.LastName).Column("LastName").Nullable();
            Map(c => c.FullName).Column("FullName").Nullable();
            Map(c => c.Team).Column("Team").Nullable();
            Map(c => c.IsActive).Column("Active").Not.Nullable();

            HasMany(c => c.Absences).Inverse().LazyLoad();
        }
    }
}