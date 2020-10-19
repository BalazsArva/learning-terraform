using LearningTerraform.Api.Extensions;
using LearningTerraform.Api.Utilities.Filters;
using LearningTerraform.Api.Utilities.Middlewares;
using LearningTerraform.BusinessLogic.Extensions;
using LearningTerraform.DataAccess.MsSql.Database;
using LearningTerraform.DataAccess.MsSql.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.AspNetCore;

namespace LearningTerraform.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBusinessLogic();
            services.AddDataAccess();

            services.AddHealthChecks();
            services.AddControllers(options => options.Filters.Add<EntityNotFoundExceptionFilterAttribute>());

            System.Console.WriteLine(Configuration.GetConnectionString("Default"));

            services
                .AddDbContext<DataContext>(
                    opts => opts.UseSqlServer(Configuration.GetConnectionString("Default")),
                    ServiceLifetime.Transient,
                    ServiceLifetime.Singleton);

            services.AddApiDocumentation();
            services.AddApiVersionOptions(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ApplicationVersionHeadersMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.OAuth2Client = new OAuth2ClientSettings
                {
                    AppName = "PetsAndOwners",
                    Realm = "PetsAndOwners",
                };
            });

            app.UseHealthChecks("/health-check");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
