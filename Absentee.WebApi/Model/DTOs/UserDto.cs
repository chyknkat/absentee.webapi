using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Absentee.WebApi.Model.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Team { get; set; }
        public bool IsActive { get; set; }
        public IList<AbsenceDto> Absences { get; set; }
    }
}