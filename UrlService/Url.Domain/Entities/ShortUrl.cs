namespace Url.Domain;

public class ShortUrl : BaseEntity
{
    public Url OriginalUrl { get; set; }
    public string ShortCode { get; set; }
    public Guid? UserId { get; set; }
    public int ClickCount { get; set; }
}
