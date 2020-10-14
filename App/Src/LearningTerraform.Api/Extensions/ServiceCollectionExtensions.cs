using LearningTerraform.Api.Options;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningTerraform.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
        {
            services.AddApiVersioning(cfg =>
            {
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(cfg =>
            {
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.GroupNameFormat = "VVV";
                cfg.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerDocument(cfg =>
            {
                cfg.Version = "v1";
                cfg.ApiGroupNames = new[] { "1" };
                cfg.Title = "PetsAndOwners";
                cfg.DocumentName = "v1";
                cfg.SchemaType = NJsonSchema.SchemaType.OpenApi3;
            });

            return services;
        }

        public static IServiceCollection AddApiVersionOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<ApplicationVersionOptions>()
                .Bind(configuration.GetSection(ApplicationVersionOptions.SectionName));

            return services;
        }
    }
}
