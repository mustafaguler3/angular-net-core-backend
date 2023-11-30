using System;
using System.Security.Claims;
using EStore.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EStore.API.Extentions
{
	public static class UserManagerExtentions
	{
		public static async Task<AppUser> FindUserByClaimsPrincipleWithAddress(this UserManager<AppUser> userManager,ClaimsPrincipal user)
		{
			var email = user.FindFirstValue(ClaimTypes.Email);

			return await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
		}

        public static async Task<AppUser> FindByEmailFromClaimsPrincipal(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            return await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        }
    }
}

