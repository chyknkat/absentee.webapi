using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Absentee.WebApi.Data;
using Absentee.WebApi.Data.Domain;
using Absentee.WebApi.Model.DTOs;

namespace Absentee.WebApi.Controllers
{
    [RoutePrefix("Absence")]
    public class AbsenceController : ApiController
    {
        private readonly IIRepository _repository;

        public AbsenceController(IIRepository repository)
        {
            _repository = repository;
        }

        [Route("GetAll")]
        [HttpGet]
        public virtual AbsenceDto[] GetAllAbsences()
        {
            var absences = _repository.GetAll<Absence>();
            var absencesDto = new List<AbsenceDto>();
            if (!absences.Any()) return absencesDto.ToArray();
            foreach (var absence in absences)
            {
                var absenceDto = LoadAbsenceDto(absence);
                absencesDto.Add(absenceDto);
            }

            return absencesDto.ToArray();
        }

        private AbsenceDto LoadAbsenceDto(Absence absence)
        {
            return new AbsenceDto
            {
                Id = absence.Id,
                StartDate = absence.StartDate,
                EndDate = absence.EndDate,
                Comments = absence.Comments,
                IsActive = absence.IsActive,
                User = new UserDto
                {
                    Id = absence.User.Id,
                    FirstName = absence.User.FirstName,
                    LastName = absence.User.LastName,
                    FullName = absence.User.FullName,
                    Team = absence.User.Team.ToString(),
                    IsActive = absence.User.IsActive
                }
            };
        }

    }
}