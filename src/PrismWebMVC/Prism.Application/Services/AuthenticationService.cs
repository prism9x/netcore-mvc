using Microsoft.AspNetCore.Identity;
using Prism.Application.Abstracts;
using Prism.Application.DTOs;
using Prism.Domain.Entities;

namespace Prism.Application.Services
{
    public class AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : IAuthenticationService
    {
        public async Task<ResponseModel> CheckLogin(string username, string password, bool hasRemember)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user is null)
            {
                return new ResponseModel
                {
                    Message = "Tài khoản hoặc mật khẩu không đúng"
                };
            }

            var result = await signInManager.PasswordSignInAsync(user, password, isPersistent: hasRemember, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                var remainingLockout = user.LockoutEnd.Value - DateTimeOffset.UtcNow;
                return new ResponseModel
                {
                    Message = $"Tài khoản của bạn đã bị khoá !, Vui lòng thử lại sau {Math.Round(remainingLockout.TotalMinutes)} minutes"
                };
            }

            if (!result.Succeeded)
            {
                return new ResponseModel
                {
                    Message = "Tài khoản hoặc mật khẩu không đúng"
                };
            }

            if (user.AccessFailedCount > 0)
            {
                await userManager.ResetAccessFailedCountAsync(user);
            }

            return new ResponseModel
            {
                Status = true
            };
        }
    }
}