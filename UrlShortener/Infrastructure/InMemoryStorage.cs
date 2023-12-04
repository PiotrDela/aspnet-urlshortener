using Microsoft.Extensions.Caching.Memory;
using UrlShortener.Controllers;

namespace UrlShortener.Infrastructure;

class InMemoryStorage : IShortUrlStorage
{
    private readonly IMemoryCache memoryCache;

    public InMemoryStorage(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public ShortUrl Get(string shortUrl)
    {
        if (memoryCache.TryGetValue(shortUrl, out ShortUrl x))
        {
            return x;
        }

        return null;
    }

    public void Save(ShortUrl shortUrl)
    {
        memoryCache.Set(shortUrl.Slug, shortUrl);
    }
}

