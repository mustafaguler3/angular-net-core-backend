using System;
using EStore.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EStore.Infrastructure.AppIdentity
{
	public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options):base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

