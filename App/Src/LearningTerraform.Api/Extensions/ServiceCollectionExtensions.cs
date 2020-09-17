using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace LearningTerraform.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddOpenApiDocument(document =>
            {
                document.Version = "v1";
                document.ApiGroupNames = new[] { "1" };
                document.Title = "PetsAndOwners";
                document.DocumentName = "v1";
                document.SchemaType = NJsonSchema.SchemaType.OpenApi3;
            });

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.GroupNameFormat = "VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
