using ApiSchool.Models;
using ApiSchool.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ApiSchool.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly SignInManager<UserModel> _signInManager;

        private readonly UserManager<UserModel> _userManager;

        public AuthenticateService(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            UserModel appUser = new UserModel();
            appUser.UserName = email;
            appUser.Email = email;

            IdentityResult result = await _userManager.CreateAsync(appUser, password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, false);
            }

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
