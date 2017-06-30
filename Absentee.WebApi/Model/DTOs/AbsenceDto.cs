using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Absentee.WebApi.Model.DTOs
{
    public class AbsenceDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }
        public UserDto User { get; set; }

    }
}