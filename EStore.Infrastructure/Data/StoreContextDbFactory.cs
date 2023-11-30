using System;
using EStore.Infrastructure.AppIdentity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EStore.Infrastructure.Data
{
	public class StoreContextDbFactory : IDesignTimeDbContextFactory<StoreContext>
	{

        public StoreContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("/Users/mustafa/Projects/EStore/EStore.API/appsettings.json")
               .Build();

            var builder = new DbContextOptionsBuilder<StoreContext>();
            var connectionString = configuration["ConnectionStrings:Local"];

            builder.UseSqlite(connectionString);

            return new StoreContext(builder.Options);
        }
    }
}

