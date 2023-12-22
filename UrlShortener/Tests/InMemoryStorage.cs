using Microsoft.Extensions.Caching.Memory;
using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure;

namespace UrlShortener.Tests
{
    class InMemoryStorage : IShortUrlStorage
    {
        private readonly Dictionary<string, ShortUrl> bySlugMapping = [];
        private readonly Dictionary<Uri, ShortUrl> byOriginalUrlMapping = [];

        public ShortUrl Find(string slug)
        {
            if (bySlugMapping.TryGetValue(slug, out ShortUrl x))
            {
                return x;
            }

            return null;
        }

        public ShortUrl Find(Uri originalUrl)
        {
            if (byOriginalUrlMapping.TryGetValue(originalUrl, out ShortUrl x))
            {
                return x;
            }

            return null;
        }

        public void Save(ShortUrl shortUrl)
        {
            bySlugMapping.Add(shortUrl.Slug, shortUrl);
            byOriginalUrlMapping.Add(shortUrl.OriginalUrl, shortUrl);
        }
    }
}
