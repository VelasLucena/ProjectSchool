using ApiSchool.Mapper;
using ApiSchool.Models;
using ApiSchool.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mime;
using System.Runtime;
using static ApiSchool.Models.Enum.SystemEnum;

namespace ApiSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly ISystemService _systemService;

        public ProfileController(IProfileService profileService, ISystemService systemService)
        {
            _profileService = profileService;
            _systemService = systemService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<UserModel>>> GetUsers()
        {
            try
            {
                List<UserModel> listUsers = new List<UserModel>();

                listUsers = await _profileService.GetUsers();

                if (listUsers.FirstOrDefault() == null)
                    return NotFound();

                return Ok(listUsers);
            }
            catch (Exception ex)
            {
                LogExceptionModel logException = new LogExceptionModel();
                logException.Error = ex.ToString() + Environment.NewLine;
                logException.CreateTime = DateTime.Now;
                logException.CreateUserId = Convert.ToInt32(AppSettings.DefaultUserId);
                await _systemService.InsertLogException(logException);
                return BadRequest("Houve um erro");
            }
        }

        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUserById([FromQuery] int id)
        {
            try
            {
                UserModel user = new UserModel();

                user = await _profileService.GetUserById(id);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                LogExceptionModel logException = new LogExceptionModel();
                logException.Error = ex.ToString() + Environment.NewLine;
                logException.CreateTime = DateTime.Now;
                logException.CreateUserId = Convert.ToInt32(AppSettings.DefaultUserId);
                await _systemService.InsertLogException(logException);
                return BadRequest("Houve um erro");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<UserModel>>> GetUserByName(string name)
        {
            try
            {
                List<UserModel> users = new List<UserModel>();
                users = await _profileService.GetUserByName(name);

                if (users.FirstOrDefault() == null)
                    return NotFound();

                return Ok(users);
            }
            catch (Exception ex)
            {
                LogExceptionModel logException = new LogExceptionModel();
                logException.Error = ex.ToString() + Environment.NewLine;
                logException.CreateTime = DateTime.Now;
                logException.CreateUserId = Convert.ToInt32(AppSettings.DefaultUserId);
                await _systemService.InsertLogException(logException);
                return BadRequest("Houve um erro");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Createuser([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] UserModel user)
        {
            try
            {
                user.UpdateUserId = Convert.ToInt32(AppSettings.DefaultUserId);
                user.UpdateTime = DateTime.Now;
                await _profileService.CreateUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                LogExceptionModel logException = new LogExceptionModel();
                logException.Error = ex.ToString() + Environment.NewLine;
                logException.CreateTime = DateTime.Now;
                logException.CreateUserId = Convert.ToInt32(AppSettings.DefaultUserId);
                await _systemService.InsertLogException(logException);
                return BadRequest("Houve um erro");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser([FromQuery] int id)
        {
            try
            {
                UserModel user = new UserModel();
                user = await _profileService.GetUserById(id);

                if (user == null)
                    return NotFound();

                await _profileService.DeleteUser(user);

                return Ok(id);

            }
            catch (Exception ex)
            {
                LogExceptionModel logException = new LogExceptionModel();
                logException.Error = ex.ToString() + Environment.NewLine;
                logException.CreateTime = DateTime.Now;
                logException.CreateUserId = Convert.ToInt32(AppSettings.DefaultUserId);
                await _systemService.InsertLogException(logException);
                return BadRequest("Houve um erro");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] UserModel userModified)
        {
            try
            {
                userModified.UpdateUserId = Convert.ToInt32(AppSettings.DefaultUserId);
                userModified.UpdateTime = DateTime.Now;

                await _profileService.UpdateUser(userModified);

                return CreatedAtAction(nameof(GetUserById), new { id = userModified.Id }, userModified);
            }
            catch (Exception ex)
            {
                LogExceptionModel logException = new LogExceptionModel();
                logException.Error = ex.ToString() + Environment.NewLine;
                logException.CreateTime = DateTime.Now;
                logException.CreateUserId = Convert.ToInt32(AppSettings.DefaultUserId);
                await _systemService.InsertLogException(logException);
                return BadRequest("Houve um erro");
            }
        }
    }
}
