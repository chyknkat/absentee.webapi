using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Absentee.WebApi.Tools;

namespace Absentee.WebApi.Data.Domain
{
    public class User : DomainEntity
    {
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string FullName { get; protected set; }
        public virtual Team Team { get; protected set; }
        public virtual bool IsActive { get; protected set; }
        public virtual IList<Absence> Absences { get; protected set; }

        public User() { Absences = new List<Absence>(); }

        public User(string firstName)
        {
            FirstName = firstName;
            Absences = new List<Absence>();
            IsActive = true;
        }

        public User(string firstName, string lastName, Team team)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = $"{firstName} {lastName}";
            Team = team;
            Absences = new List<Absence>();
            IsActive = true;
        }

        public virtual void SetName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = $"{firstName} {lastName}";
        }

        public virtual void SetTeam(Team team)
        {
            Team = team;
        }

        public virtual void SetAbsences(IEnumerable<Absence> absences)
        {
            Absences.AddRange(absences);
        }

        public virtual void ToggleActiveFlag(bool isActive)
        {
            IsActive = isActive;
        }
    }

    public enum Team
    {
        NoTeam = 0,
        External = 1,
        Internal = 2,
        IT = 3, 
        OffShore = 4
    }
}