using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ICMON.Domain.Interfaces;
using ICMON.Infrastructure.Persistence;
using ICMON.Infrastructure.Persistence.Repositories;
using ICMON.Infrastructure.Cache;
using ICMON.Infrastructure.Authentication;
using ICMON.Application.Services;
using StackExchange.Redis;

namespace ICMON.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        var redisConnectionString = configuration.GetConnectionString("Redis");
        if (!string.IsNullOrEmpty(redisConnectionString))
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(redisConnectionString));
            services.AddScoped<ICacheService, RedisCacheService>();
        }

        return services;
    }
}
