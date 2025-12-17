using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Url.Application;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

        ValidationResult[] result = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        List<ValidationFailure> failures = result.SelectMany(r => r.Errors)
                        .Where(f => f is not null)
                        .ToList();

        if (failures.Count != 0)
            throw new ValidationException(failures);

        return await next();
    }
}
