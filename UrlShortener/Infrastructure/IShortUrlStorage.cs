using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure;

interface IShortUrlStorage
{
    public void Save(ShortUrl shortUrl);
    public ShortUrl Find(string slug);
    public ShortUrl Find(Uri originalUrl);
}

