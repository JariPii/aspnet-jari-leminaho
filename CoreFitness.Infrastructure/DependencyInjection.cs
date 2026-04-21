using CoreFitness.Application.Identity;
using CoreFitness.Domain.Interfaces.Memberships;
using CoreFitness.Domain.Interfaces.TrainingSessions;
using CoreFitness.Domain.Interfaces.UnitOfWork;
using CoreFitness.Domain.Interfaces.Users;
using CoreFitness.Infrastructure.Identity;
using CoreFitness.Infrastructure.Persistence;
using CoreFitness.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreFitness.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config,
        IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            //services.AddSingleton<SqliteConnection>(_ =>
            //{
            //    var connection = new SqliteConnection("Data Source=:memory:;");
            //    connection.Open();
            //    return connection;
            //});

            services.AddDbContext<CoreFitnessDbContext>(options =>
            options.UseSqlite("Data Source=corefitness-dev.db"));

            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlite("Data Source=auth-dev.db"));

            //services.AddDbContext<CoreFitnessDbContext>((sp, options) =>
            //{
            //    var connection = sp.GetRequiredService<SqliteConnection>();
            //    options.UseSqlite(connection);
            //});

            //services.AddDbContext<AuthDbContext>((sp, options) =>
            //{
            //    var connection = sp.GetRequiredService<SqliteConnection>();
            //    options.UseSqlite(connection);
            //});
        }
        else
        {
            services.AddDbContext<CoreFitnessDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        }

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
        })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication()
            .AddGoogle(options =>
            {
                var clientId = config["Authentication:Google:ClientId"];
                var clientSecret = config["Authentication:Google:ClientSecret"];

                if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                {
                    throw new InvalidOperationException(
                        "Google authentication is missing in configuration." +
                        "Please set Authentication:Google:ClientId and ClientSecret");
                }

                options.ClientId = clientId;
                options.ClientSecret = clientSecret;

                //options.ClaimActions.MapJsonKey("email_verified", "email_verified");

                options.CallbackPath = "/signin-google";
            });

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        services.AddScoped<IMembershipTypeRepository, MembershipTypeRepository>();
        services.AddScoped<ITrainingSessionRepository, TrainingSessionRepository>();

        return services;
    }
}
