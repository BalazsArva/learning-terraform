using System;
using System.Threading.Tasks;
using LearningTerraform.Api.Constants;
using LearningTerraform.Api.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LearningTerraform.Api.Utilities.Middlewares
{
    public class ApplicationVersionHeadersMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IOptions<ApplicationVersionOptions> applicationVersionOptions;

        public ApplicationVersionHeadersMiddleware(
            RequestDelegate next,
            IOptions<ApplicationVersionOptions> applicationVersionOptions)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.applicationVersionOptions = applicationVersionOptions ?? throw new ArgumentNullException(nameof(applicationVersionOptions));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Response.Headers[HeaderNames.CommitHash] = applicationVersionOptions.Value.Commit;
            context.Response.Headers[HeaderNames.Version] = applicationVersionOptions.Value.Version;

            await next(context);
        }
    }
}
