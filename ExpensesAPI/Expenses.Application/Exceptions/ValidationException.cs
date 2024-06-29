using FluentValidation.Results;

namespace Expenses.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; set; } 
        public ValidationException(): base("Se ha producido un error de validación") {
            
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
        {
            Errors = failures
                     .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                     .ToDictionary(fg => fg.Key, fg => fg.ToArray());
        }

    }
}
