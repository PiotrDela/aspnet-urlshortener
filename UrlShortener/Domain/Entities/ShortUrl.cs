using UrlShortener.Infrastructure;

namespace UrlShortener.Domain.Entities;

public class ShortUrl
{
    public Uri OriginalUrl { get; private set; }
    public string Slug { get; private set; }

    public static ShortUrl Create(Uri originalUrl, INumberSequence sequence)
    {
        var id = sequence.GetNext();
        return new ShortUrl(originalUrl, Base36Encoding.Convert(id));
    }

    private ShortUrl(Uri originalUrl, string slug)
    {
        OriginalUrl = originalUrl ?? throw new ArgumentNullException(nameof(originalUrl));
        Slug = slug ?? throw new ArgumentNullException(nameof(slug));
    }
}

