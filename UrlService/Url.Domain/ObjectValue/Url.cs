namespace Url.Domain;

public sealed record Url
{
    public string Value { get; set; }

    private Url(string value) => Value = value;

    public static Url Create(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentNullException("The url can not be empty!");

        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            throw new ArgumentException("Invalid Url format!");

        return new(url.Trim());
    }

    
    public override string ToString() => Value;
}
