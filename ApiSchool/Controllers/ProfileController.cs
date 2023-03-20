using ApiSchool.Mapper;
using ApiSchool.Models;
using ApiSchool.Services.Interfaces;
using ApiSchool.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime;

namespace ApiSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<UserModel>>> GetUsers()
        {
            try
            {
                List<UserModel> listUsers = new List<UserModel>();

                listUsers = await _profileService.GetUsers();

                if(listUsers.FirstOrDefault() == null)
                    return NotFound();

                return Ok(listUsers);
            }
            catch (Exception ex)
            {
                throw new InvalidException(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUserById([FromQuery] int id)
        {
            try
            {
                UserModel user = new UserModel();

                user = await _profileService.GetUserById(id);

                throw new Exception();

                if (user == null) 
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new InvalidException(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Createuser([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] UserModel user)
        {
            try
            {
                user = ProfileMappers.CreateUserMapper(user);
                await _profileService.CreateUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                throw new InvalidException(ex);
            }
        }
    }
}
