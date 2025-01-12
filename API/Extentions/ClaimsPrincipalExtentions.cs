using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Security.Claims;

namespace API.Extentions
{
    public static class ClaimsPrincipalExtentions
    {
        public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users.FirstOrDefaultAsync(x=>x.Email == user.GetEmail());
            if (userToReturn == null) throw new Exception("User not found");
            return userToReturn;
        }
        public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users
                .Include(x=>x.Address)
                .FirstOrDefaultAsync(x => x.Email == user.GetEmail());
            if (userToReturn == null) throw new Exception("User not found");
            return userToReturn;
        }
        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email) ?? throw new AuthenticationException("Email claim not found");
            return email;
        }
    }
}
