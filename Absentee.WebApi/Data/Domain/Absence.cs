using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Absentee.WebApi.Data.Domain
{
    public class Absence : DomainEntity
    {
        public virtual DateTime StartDate { get; protected set; }
        public virtual DateTime EndDate { get; protected set; }
        public virtual string Comments { get; protected set; }
        public virtual User User { get; protected set; }
    }
}