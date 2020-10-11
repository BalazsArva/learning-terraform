using LearningTerraform.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningTerraform.Api.Utilities.Filters
{
    public class EntityNotFoundExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is EntityNotFoundException entityNotFoundException)
            {
                context.ExceptionHandled = true;
                context.Result = new NotFoundObjectResult(
                    new ProblemDetails
                    {
                        Detail = entityNotFoundException.Message,
                        Title = "The requested entity could not be found.",
                        Status = StatusCodes.Status404NotFound,
                    });
            }
        }
    }
}
