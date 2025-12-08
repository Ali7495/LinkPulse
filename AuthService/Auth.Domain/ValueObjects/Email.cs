namespace Auth.Domain;

public sealed record Email
{
    public string Value { get; set; }

    private Email(string value)
    {
        Value = value.ToLower().Trim();
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentNullException("Email format is not correct!");

        return new(email);
    }

    public override string ToString() => Value;
}
