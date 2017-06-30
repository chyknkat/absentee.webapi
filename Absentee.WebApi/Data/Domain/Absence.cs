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
        public virtual bool IsActive { get; protected set; }
        public virtual User User { get; protected set; }

        public Absence() { User = new User();}

        public Absence(DateTime startDate, DateTime endTime)
        {
            StartDate = startDate;
            EndDate = endTime;
            IsActive = true;
        }

        public Absence(DateTime startDate, DateTime endTime, string comments)
        {
            StartDate = startDate;
            EndDate = endTime;
            Comments = comments;
            IsActive = true;
        }

        public virtual void SetUser(User user)
        {
            User = user;
        }

        public virtual void SetDates(DateTime startDate, DateTime endTime)
        {
            StartDate = startDate;
            EndDate = endTime;
        }

        public virtual void SetComments(string comments)
        {
            Comments = comments;
        }

        public virtual void ToggleActiveFlag(bool isActive)
        {
            IsActive = isActive;
        }
    }
}