using Microsoft.EntityFrameworkCore;
using RFAuthControllers;
using RFAuthEF;
using RFAuthServices;
using RFAuthServices.Filters;
using RFAuthServices.Middlewares;
using RFBaseEF;
using RFBaseServices;
using RFHttpExceptions.Middlewares;
using RFL10n;
using RFPermissionsEF;
using RFPermissionsServices;
using RFRBACEF;
using RFRBACServices;
using RFRGOBACEF;
using RFRGOBACServices;
using RFUserGroupsEF;
using RFUserGroupsServices;

namespace FreeCampusServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            builder.Services.AddScoped<DbContext, AppDbContext>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Frontend", policy =>
                {
                    policy
                        //.AllowAnyOrigin()
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddRFL10nServices();

            builder.Services.AddRFBaseServices();
            builder.Services.AddRFBaseEF();

            builder.Services.AddRFAuthServices();
            builder.Services.AddRFAuthEF();
            
            builder.Services.AddRFPermissionsServices();
            builder.Services.AddRFPermissionsEF();

            builder.Services.AddRFUserGroupsServices();
            builder.Services.AddRFUserGroupsEF();

            builder.Services.AddRFRBACServices();
            builder.Services.AddRFRBACEF();
            
            builder.Services.AddRFRGOBACServices();
            builder.Services.AddRFRGOBACEF();

            builder.Services.AddFCServices();

            builder.Services
                .AddControllers(options => {
                    options.UseGeneralRoutePrefix("api");
                    options.Filters.Add<AuthorizationFilter>();
                })
                .AddRFAuthControllers();

            var app = builder.Build();

            app.UseCors("Frontend");

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<AuthorizationMiddleware>();
            app.UseMiddleware<HttpExceptionMiddleware>();

            app.MapControllers();

            app.Services.CreateScope()
                .ServiceProvider
                .GetRequiredService<IL10n>()
                .AddTranslationsFromPath("Locale");

            app.Run();
        }
    }
}
