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
        private readonly IConfiguration _configuration;
        private readonly IAuthenticateService _authenticateService;
        private readonly ISystemService _systemService;

        public AccountController(IConfiguration configuration, IAuthenticateService authenticateService, ISystemService systemService)
        {
            _configuration = configuration;
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
                logException.CreateUserId = Convert.ToInt32(AppStartUp.GetSettingsApp(AppSettingsKeys.DefaultUserId));
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
                IEnumerable<Claim> claims = new[]
            {
                new Claim("email", userLogin.Email),
                new Claim("meuToken", "TokenTeste"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                DateTime experitarion = DateTime.Now.AddMinutes(20);

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: experitarion,
                    signingCredentials: creds
                    );

                tokenGenerated.Token = new JwtSecurityTokenHandler().WriteToken(token);
                tokenGenerated.Expiration = experitarion;
            }
            else
            {
                ModelState.AddModelError("LoginUser", "Login inválido!");
                return BadRequest(ModelState);
            }

            return tokenGenerated;
        }

        //public ActionResult<UserTokenModel> GenerateToken(LoginModel userLogin)
        //{
        //    IEnumerable<Claim> claims = new[]
        //    {
        //        new Claim("email", userLogin.Email),
        //        new Claim("meuToken", "TokenTeste"),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        //    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings: JWT: Key"]));

        //    SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    DateTime experitarion = DateTime.Now.AddMinutes(20);

        //    JwtSecurityToken token = new JwtSecurityToken(
        //        issuer: _configuration["Jwt: Issuer"],
        //        audience: _configuration["Jwt: Audience"],
        //        claims: claims,
        //        expires: experitarion,
        //        signingCredentials: creds
        //        );

        //    UserTokenModel tokenGenerated = new UserTokenModel();
        //    tokenGenerated.Token = new JwtSecurityTokenHandler().WriteToken(token);
        //    tokenGenerated.Expiration = experitarion;

        //    return tokenGenerated;
        //}
    }
}
