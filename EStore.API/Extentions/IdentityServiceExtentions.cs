using System;
using System.Text;
using EStore.API.Mappers;
using EStore.Core.Entities;
using EStore.Infrastructure;
using EStore.Infrastructure.AppIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EStore.API.Extentions
{
	public static class IdentityServiceExtentions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
		{

			services.AddAutoMapper(typeof(MappingProfile));
            services.AddDbContext<AppIdentityDbContext>(i => i.UseSqlite(configuration["ConnectionStrings:IdentityConnection"]));
			
            services.AddIdentityCore<AppUser>(opt =>
			{
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireDigit = false;
			}).AddEntityFrameworkStores<AppIdentityDbContext>()
			.AddUserManager<UserManager<AppUser>>()
			.AddSignInManager<SignInManager<AppUser>>();

				services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
			{
				option.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
					ValidIssuer = configuration["Token:Issuer"],
					ValidateIssuer = true,
					ValidateAudience = false
				};
			});


			services.AddAuthorization();

			return services;
		}
	}
}

