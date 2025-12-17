using FluentValidation;

namespace Url.Application;

public class CreateShortUrlCommandValidator : AbstractValidator<CreateShortUrlCommand>
{
    public CreateShortUrlCommandValidator()
    {
        RuleFor(r => r.originalUrl)
            .NotEmpty()
            .MaximumLength(500)
            .Must(BeValidAbsoluteUrl)
            .WithMessage("Url must be a valid address");
    }


    private static bool BeValidAbsoluteUrl(string url)
        => Uri.TryCreate(url, UriKind.Absolute, out _);
}
