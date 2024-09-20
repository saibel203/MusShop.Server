using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using MusShop.Application.UseCases.Commons.Behaviors;
using MusShop.Domain.Model.RepositoryAbstractions.Base;
using MusShop.Domain.Model.ResultItems;
using MusShop.UnitTests.Shared.Mocks;
using NSubstitute;

namespace MusShop.UnitTests.Shared;

public abstract class BaseUnitTest
{
    protected readonly IUnitOfWork UnitOfWork = Substitute.For<IUnitOfWork>();
    protected readonly IMapper Mapper = MapsterMock.GetMapper();

    protected const int BaseReceivedCount = 1;

    protected static ValidationPipelineBehavior<TCommand, DomainResult<TResponse>>
        GetValidationPipeline<TCommand, TResponse>(string errorDescription, string propertyName)
        where TCommand : class
        where TResponse : class
    {
        IValidator<TCommand> validator =
            Substitute.For<IValidator<TCommand>>();

        ValidationFailure validationFailure =
            new ValidationFailure(
                propertyName,
                errorDescription);

        validator.ValidateAsync(
                Arg.Any<ValidationContext<TCommand>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(
                new ValidationResult(new List<ValidationFailure> { validationFailure })));

        List<IValidator<TCommand>> validators =
            new List<IValidator<TCommand>> { validator };

        ValidationPipelineBehavior<TCommand, DomainResult<TResponse>> pipeline =
            new ValidationPipelineBehavior<TCommand, DomainResult<TResponse>>(validators);

        return pipeline;
    }

    protected static string GenerateCustomLengthString(int stringLength)
    {
        Random rd = new Random();
        
        const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
        char[] chars = new char[stringLength];

        for (int i = 0; i < stringLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }

        return new string(chars);
    }
}