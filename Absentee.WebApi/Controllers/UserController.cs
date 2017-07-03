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
    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        private readonly IIRepository _repository;

        public UserController(IIRepository repository)
        {
            _repository = repository;
        }

        [Route("GetAll")]
        [HttpGet]
        public virtual UserDto[] GetAllUsers()
        {
            var users = _repository.GetAll<User>();
            var usersDto = new List<UserDto>();
            if (!users.Any()) return usersDto.ToArray();
            foreach (var user in users)
            {
                var userDto = LoadUserDto(user);
                usersDto.Add(userDto);
            }

            return usersDto.ToArray();
        }

        [Route("GetById/{userId:int}")]
        [HttpGet]
        public virtual UserDto GetUserById(int userId)
        {
            var user = _repository.Get<User>(userId);
            return LoadUserDto(user);
        }

        [Route("New")]
        [HttpPost]
        public virtual HttpResponseMessage NewUser(UserDto user)
        {
            try
            {
                Team tm;
                if (!Enum.TryParse(user.Team, true, out tm))
                {
                    throw new Exception("Invalid Team.");
                }

                var domainUser = new User(user.FirstName, user.LastName, tm);
                
                _repository.Save(domainUser);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [Route("Update")]
        [HttpPost]
        public virtual HttpResponseMessage UpdateUser(UserDto user)
        {
            try
            {
                Team tm;
                if (!Enum.TryParse(user.Team, true, out tm))
                {
                    throw new Exception("Invalid Team.");
                }
                var domainUser = _repository.Get<User>(user.Id);
                domainUser.SetName(user.FirstName, user.LastName);
                domainUser.SetTeam(tm);
                domainUser.ToggleActiveFlag(user.IsActive);
                _repository.Save(domainUser);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [Route("ToggleActive/{userId:int}/{isActive:bool}")]
        [HttpPost]
        public virtual HttpResponseMessage ToggleActiveFlag(int userId, bool isActive)
        {
            try
            {
                var user = _repository.Get<User>(userId);
                user.ToggleActiveFlag(isActive);
                _repository.Save(user);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        private UserDto LoadUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                Team = user.Team.ToString(),
                IsActive = user.IsActive
            };
        }
    }
}