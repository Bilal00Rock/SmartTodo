using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;

namespace SmartTodo.Application.Common.Behaviors;
/// <summary>
/// Application Layer Generic Validation Pipline into MediatR 
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator = validator;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //if we did not set validator for out cammand or query it will skip hte validation
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
            .ConvertAll(error => Error.Validation(
                code: error.PropertyName,
                description: error.ErrorMessage));
            
        //using dynamic is okay here because the type is converted to ErrorOr
        return (dynamic)errors;
    }
}
