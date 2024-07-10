using Expenses.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Expenses.Application.Filters
{
    public class ApiExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationException validationEx:
                    HandleValidationException(context, validationEx);
                    break;
                case NotFoundException notFoundEx:
                    HandleNotFoundException(context, notFoundEx);
                    break;
                case ForbiddenException forbiddenEx:
                    HandleForbiddenAccessException(context, forbiddenEx);
                    break;
                default:
                    HandleUnknownException(context);
                    break;
            }

            base.OnException(context);
        }
        private void HandleValidationException(ExceptionContext context, ValidationException exception)
        {
            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context, NotFoundException exception)
        {
            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "No se encontró el recurso especificado.",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleForbiddenAccessException(ExceptionContext context, ForbiddenException exception)
        {
            var details = new ProblemDetails
            {
                Title = "Prohibido",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                Detail = exception.Message
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Se produjo un error al procesar la solicitud.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
