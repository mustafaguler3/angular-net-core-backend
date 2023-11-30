using System;
using EStore.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EStore.Infrastructure.AppIdentity
{
	public class AppIdentityDbContextSeed
	{
		public static async Task SeedUsersAsync(UserManager<AppUser> userManager,ILogger logger)
		{
			if (!userManager.Users.Any())
			{
				var user = new AppUser
				{
					DisplayName = "Mustafa",
					Email = "mustafa@test.com",
					UserName = "mustafa@test.com",
					Address = new Address
					{
						FirstName = "mustafa",
						LastName = "Guler",
						Street = "10 The street",
						City = "Chicago",
						State = "CH",
						ZipCode = "9000",
					}
				};

				var result = await userManager.CreateAsync(user, "password");
				var errors = result.Errors.Select(i => i.Description);

                if (!result.Succeeded)
				{
					throw new Exception($"${errors}");
				}
			}
        }
	}
}

