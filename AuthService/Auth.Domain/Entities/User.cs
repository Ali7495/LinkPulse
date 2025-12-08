namespace Auth.Domain;

public class User : BaseEntity
{
    public string Username { get; set; }
    public Email Email { get; set; }
    public string FullName { get; set; }
    public string PasswordHash { get; set; }
}
