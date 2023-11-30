using System;
using EStore.Core.Errors;
using EStore.Core.Interfaces;
using EStore.Infrastructure.Data;
using EStore.Infrastructure.Repositories;
using EStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace EStore.Infrastructure
{
	public static class ServiceRegistration
	{
        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
		{

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddDbContext<StoreContext>(i => i.UseSqlite(configuration["ConnectionStrings:Local"]));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBasketRepository, BasketRepository>();


            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));

                return ConnectionMultiplexer.Connect(options);
            });

            return services;
        }
    }
}

