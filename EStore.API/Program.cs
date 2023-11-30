using EStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EStore.Infrastructure;
using EStore.Core.Interfaces;
using EStore.Infrastructure.Repositories;
using EStore.API.Mappers;
using EStore.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using EStore.Core.Errors;
using StackExchange.Redis;
using EStore.API.Extentions;
using EStore.Infrastructure.AppIdentity;
using Microsoft.AspNetCore.Identity;
using EStore.Core.Entities;
using EStore.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCors(i => i.AddPolicy("Angular", i => i.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials()
           .WithOrigins("http://localhost:4200", "https://localhost:4200")));

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Where(i => i.Value.Errors.Count > 0)
        .SelectMany(i => i.Value.Errors)
        .Select(i => i.ErrorMessage).ToArray();

        var response = new ValidationErrorResponse
        {
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    };


});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseCors("Angular");

app.UseStaticFiles();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
app.UseSwaggerDocumentation();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    
    try
    {
        await context.Database.MigrateAsync();
        await identityContext.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context);
        await AppIdentityDbContextSeed.SeedUsersAsync(userManager, logger);
    }
    catch (Exception ex)
    {
        logger.LogError(ex.Message, "An error occured");
    }
}




app.Run();

