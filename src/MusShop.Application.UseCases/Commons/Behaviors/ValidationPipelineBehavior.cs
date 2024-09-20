using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Commons.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

        ValidationResult[] validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        IEnumerable<DomainError> errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new DomainError(
                validationFailure.ErrorCode,
                validationFailure.ErrorMessage))
            .ToList();

        if (!errors.Any()) return await next();

        MethodInfo? errorResult =
            typeof(TResponse).GetMethod(
                "Failure", new[] { typeof(IEnumerable<DomainError>) }
            );

        if (errorResult is null)
        {
            throw new InvalidOperationException("TResponse does not support validation failure handling.");
        }

        if (errorResult.Invoke(null, new object[] { errors }) is not TResponse response)
        {
            throw new InvalidOperationException("TResponse does not support validation failure handling.");
        }

        return response;
    }
}