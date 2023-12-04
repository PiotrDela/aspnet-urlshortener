namespace UrlShortener.Controllers;

public class ShortUrl
{
    public Uri OriginalUrl { get; set; }
    public string Slug { get; set; }

    public ShortUrl(Uri originalUrl, string slug)
    {
        OriginalUrl = originalUrl ?? throw new ArgumentNullException(nameof(originalUrl));
        Slug = slug ?? throw new ArgumentNullException(nameof(slug));
    }
}

