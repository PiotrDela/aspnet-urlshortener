using UrlShortener.Controllers;

namespace UrlShortener.Infrastructure;

interface IShortUrlStorage
{
    public void Save(ShortUrl shortUrl);
    public ShortUrl Get(string shortUrl);
}

