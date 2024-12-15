using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tournament.Core.Exceptions;

namespace Tournament.Presentation.Filters;

public class ApiExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException notFoundException)
        {
            context.Result = new ObjectResult(new ProblemDetails()
            {
                Title = "Resource not found",
                Detail = notFoundException.Message,
                Status = StatusCodes.Status404NotFound,
                Instance = context.HttpContext.Request.Path
            })
            {
                StatusCode = StatusCodes.Status404NotFound
            };

            // Tells ASP.NET Core that the exception has been handled and no further processing of that
            // exception should occur.
            context.ExceptionHandled = true;
        }
        else if (context.Exception is TournamentMaxGamesViolationException exception)
        {
            context.Result = new ObjectResult(new ProblemDetails()
            {
                Title = "Tournament Max Games Violation",
                Detail = exception.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = context.HttpContext.Request.Path
            });
        }
    }
}