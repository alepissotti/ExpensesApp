using FluentValidation;
using MediatR;

namespace Expenses.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> :IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> 
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any()) {

                var context = new ValidationContext<TRequest>(request);

                var validationsResult = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationsResult.Where(vr => vr.Errors.Any())
                                                .SelectMany(vr => vr.Errors)
                                                .ToList();

                if (failures.Any())
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
