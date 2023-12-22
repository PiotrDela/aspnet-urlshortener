using MediatR;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Commands;

class ShortentUrlCommand : IRequest<ShortUrl>
{
    public Uri Url { get; set; }

    public ShortentUrlCommand(Uri url)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
    }
}

