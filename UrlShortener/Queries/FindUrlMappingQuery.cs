using MediatR;

namespace UrlShortener.Queries;

class FindUrlMappingQuery : IRequest<Uri>
{
    public string ShortUrl { get; set; }

    public FindUrlMappingQuery(string shortUrl)
    {
        ShortUrl = shortUrl ?? throw new ArgumentNullException(nameof(shortUrl));
    }
}

