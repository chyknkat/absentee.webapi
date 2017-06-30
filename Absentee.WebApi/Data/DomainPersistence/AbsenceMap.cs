﻿using System;
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

            References(c => c.User).Column("UserId").ForeignKey("FK_Absence_ToUser").Not.Nullable();
        }
    }
}