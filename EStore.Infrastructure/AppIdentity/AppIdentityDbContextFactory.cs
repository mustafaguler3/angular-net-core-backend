using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EStore.Infrastructure.AppIdentity
{
	public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
	{
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("/Users/mustafa/Projects/EStore/EStore.API/appsettings.json")
               .Build();

            var builder = new DbContextOptionsBuilder<AppIdentityDbContext>();
            var connectionString = configuration["ConnectionStrings:IdentityConnection"];

            builder.UseSqlite(connectionString);

            return new AppIdentityDbContext(builder.Options);
        }
    }
}

