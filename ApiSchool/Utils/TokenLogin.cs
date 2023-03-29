using ApiSchool.Models;
using ApiSchool.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiSchool.Utils
{
    public class TokenLogin
    {
        public UserTokenModel GenerateToken(LoginModel userLogin)
        {
            IEnumerable<Claim> claims = new[]
                {
                new Claim("email", userLogin.Email),
                new Claim("meuToken", "TokenTeste"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Key));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime experitarion = DateTime.Now.AddMinutes(20);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: new AppSettings().GetConfiguration("Issuer"),
                audience: new AppSettings().GetConfiguration("Audience"),
                claims: claims,
                expires: experitarion,
                signingCredentials: creds
                );

            UserTokenModel tokenGenerated = new UserTokenModel();
            tokenGenerated.Token = new JwtSecurityTokenHandler().WriteToken(token);
            tokenGenerated.Expiration = experitarion;

            return tokenGenerated;
        }
    }
}
