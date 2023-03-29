using ApiSchool.Models;
using ApiSchool.Models.ViewModels;
using ApiSchool.Services;
using ApiSchool.Services.Interfaces;
using ApiSchool.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static ApiSchool.Models.Enum.SystemEnum;

namespace ApiSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly ISystemService _systemService;

        public AccountController(IAuthenticateService authenticateService, ISystemService systemService)
        {
            _authenticateService = authenticateService;
            _systemService = systemService;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser([FromBody] RegisterUserModel userRegister)
        {
            try
            {
                if (userRegister.Password != userRegister.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfimPassword", "As senhas não conferem");
                    return BadRequest(ModelState);
                }

                bool result = await _authenticateService.RegisterUser(userRegister.Email, userRegister.Password);

                if (result)
                    return Ok($"Usuário {userRegister.Email} criado com sucesso!");
                else
                {
                    ModelState.AddModelError("CreateUser", "Registro inválido!");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                LogExceptionModel logException = new LogExceptionModel();
                logException.Error = ex.ToString() + Environment.NewLine;
                logException.CreateTime = DateTime.Now;
                logException.CreateUserId = Convert.ToInt32(AppSettings.DefaultUserId);
                await _systemService.InsertLogException(logException);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserTokenModel>> LoginUser([FromBody] LoginModel userLogin)
        {
            UserTokenModel tokenGenerated = new UserTokenModel();

            bool result = await _authenticateService.Authenticate(userLogin.Email, userLogin.Password);

            if (result)
            {
                tokenGenerated = new TokenLogin().GenerateToken(userLogin);
            }
            else
            {
                ModelState.AddModelError("LoginUser", "Login inválido!");
                return BadRequest(ModelState);
            }

            return tokenGenerated;
        }
    }
}
