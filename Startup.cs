using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.PortableExecutable;
using Player.Api.Repository;
using Player.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Text;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceStack;
using Player.Api.Helpers;

namespace Player.Api;
public class Startup
{

    /// <summary>
    ///     Initializes a new instance of <see cref="Startup" />.
    /// </summary>
    /// <param name="configuration">An <see cref="IConfiguration" /> representing the application's configuration.</param>
    /// <param name="env">An <see cref="IWebHostEnvironment" /> representing the application's hosting environment.</param>
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    /// <summary>
    ///     Gets an <see cref="IConfiguration" /> representing the application's configuration.
    /// </summary>
    private IConfiguration Configuration { get; }

    /// <summary>
    ///     Gets an <see cref="IWebHostEnvironment" /> representing the application's hosting environment.
    /// </summary>
    private IWebHostEnvironment Env { get; }

    /// <summary>
    ///     Configures the application and its hosting environment.
    /// </summary>
    /// <param name="app">An <see cref="IApplicationBuilder" /> representing the application builder.</param>
    /// <param name="PlayerDBContext"> Database context for the wallet repository, and data access</param>
    public void Configure(IApplicationBuilder app, PlayerDBContext PlayerDBContext)
    {
        // REMINDER: Order matters here.
        PlayerDBContext.Database.EnsureCreated();
        // Apply Migrations
        PlayerDBContext.Database.Migrate();

            app.UseHttpsRedirection();
            // Add the generic middleware.
            app.UseStaticFiles();
            app.UseRouting();

            // Add forwarded headers.
            app.UseForwardedHeaders();
        // Add CORS.
        app.UseCors(builder =>
        {
            var corsOrigins = Configuration["CorsOrigins"].Split(";");
            if (corsOrigins.Length == 1 && string.IsNullOrWhiteSpace(corsOrigins[0]) || corsOrigins[0] == "*")
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader().AllowAnyOrigin();
            }
            else
            {
                builder.WithOrigins()
                .AllowAnyMethod()
                .AllowAnyHeader().AllowAnyOrigin().AllowCredentials();
            }
        });



        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "API"); });
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var config = Configuration.Get<Config>();
        var localString = Configuration["PlayerDB:DbEndpoint"];
        services.AddDbContext<PlayerDBContext>(opt => opt
                .UseSqlServer(localString),
            ServiceLifetime.Scoped);
        // Add services to the container.

        //var key = Encoding.ASCII.GetBytes(config.JwtSecret); //I would like to implement this but i will just store it on app settings for now.
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        services.AddAuthorization(option =>
        option.AddPolicy(Role.Player, p => p.RequireClaim(Role.Claim,"true")));
        services.AddControllers();
        services.AddDbContext<PlayerDBContext>(opt => opt.UseSqlServer("PlayerDB")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution));
        services.AddTransient<IPlayerRepository, PlayerRepository>();
        services.AddTransient<IPlayerServices, PlayerServices>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Player Swagger",
                Version = "v1",
            });
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer Test')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
        });

        services.AddHealthChecks();
        services.AddEndpointsApiExplorer();
        services.AddControllersWithViews();
    }
}