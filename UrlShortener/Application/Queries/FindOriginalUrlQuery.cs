using MediatR;

namespace UrlShortener.Application.Queries;

class FindOriginalUrlQuery : IRequest<Uri>
{
    public string ShortUrl { get; set; }

    public FindOriginalUrlQuery(string shortUrl)
    {
        ShortUrl = shortUrl ?? throw new ArgumentNullException(nameof(shortUrl));
    }
}

