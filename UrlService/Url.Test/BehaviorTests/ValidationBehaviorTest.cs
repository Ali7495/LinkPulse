using FluentValidation;
using MediatR;
using Moq;
using Url.Application;

namespace Url.Test;

public class ValidationBehaviorTest
{
    [Fact]
    public async Task Handle_WhenRequestIsInvalid_ShouldThrowValidationException_AndNotCallNext()
    {
        // Arrange

        CreateShortUrlCommand request = new ("",null);

        var validators = new IValidator<CreateShortUrlCommand>[]
        {
            new CreateShortUrlCommandValidator()
        };

        ValidationBehavior<CreateShortUrlCommand, string> behavior = new (validators);

        Mock<RequestHandlerDelegate<string>> mocknext = new();


        // Act + Assert

        await Assert.ThrowsAsync<ValidationException>(()=> behavior.Handle(request,mocknext.Object,CancellationToken.None));

        mocknext.Verify(x=> x.Invoke(), Times.Never);
    }
}
