using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        [Route("GetByUser/{userId:int}")]
        [HttpGet]
        public virtual AbsenceDto[] GetAbsencesByUser(int userId)
        {
            var user = _repository.Get<User>(userId);
            var absencesDto = new List<AbsenceDto>();
            if (!user.Absences.Any()) return absencesDto.ToArray();
            foreach (var absence in user.Absences)
            {
                var absenceDto = LoadAbsenceDto(absence);
                absencesDto.Add(absenceDto);
            }

            return absencesDto.ToArray();
        }

        [Route("GetById/{absenceId:int}")]
        [HttpGet]
        public virtual AbsenceDto GetAbsenceById(int absenceId)
        {
            var absence = _repository.Get<Absence>(absenceId);
            return LoadAbsenceDto(absence);
        }

        [Route("New")]
        [HttpPost]
        public virtual HttpResponseMessage NewAbsence(AbsenceDto absence)
        {
            try
            {
                var domainAbsence = new Absence(absence.StartDate, absence.EndDate, absence.Comments);
                var user = _repository.Get<User>(absence.User.Id);
                domainAbsence.SetUser(user);
                _repository.Save(domainAbsence);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [Route("Update")]
        [HttpPut]
        public virtual HttpResponseMessage UpdateAbsence(AbsenceDto absence)
        {
            try
            {
                var domainAbsence = _repository.Get<Absence>(absence.Id);
                var domainUser = _repository.Get<User>(absence.User.Id);
                domainAbsence.SetDates(absence.StartDate, absence.EndDate);
                domainAbsence.SetUser(domainUser);
                domainAbsence.SetComments(absence.Comments);
                domainAbsence.ToggleActiveFlag(absence.IsActive);
                _repository.Save(domainAbsence);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [Route("ToggleActive/{absenceId:int}/{isActive:bool}")]
        [HttpPut]
        public virtual HttpResponseMessage ToggleActiveFlag(int absenceId, bool isActive)
        {
            try
            {
                var absence = _repository.Get<Absence>(absenceId);
                absence.ToggleActiveFlag(isActive);
                _repository.Save(absence);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
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