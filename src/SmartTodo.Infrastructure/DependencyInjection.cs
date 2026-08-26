using SmartTodo.Application.Common.Interfaces;
using SmartTodo.Infrastructure.Common.Persistence;
using SmartTodo.Infrastructure.Admins.Persistence;
using SmartTodo.Infrastructure.Todos.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartTodo.Infrastructure.Users.Persistence;
using Microsoft.Extensions.Configuration;
using SmartTodo.Infrastructure.Authentication.TokenGenerator;
using Microsoft.Extensions.Options;
using SmartTodo.Domain.Common.Interfaces;
using SmartTodo.Infrastructure.Authentication.PasswordHasher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace SmartTodo.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAuthentication(configuration)
            .AddPersistence();
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<SmartTodoDbContext>(options =>
            options.UseSqlite("Data Source = SmartTodo.db"));

        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<ITodosRepository, TodosRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<SmartTodoDbContext>());

        return services;
    }
     public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.Section, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            });


        return services;
    }
}