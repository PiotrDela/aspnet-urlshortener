using MediatR;

namespace UrlShortener.Commands;

class ShortentUrlCommand : IRequest<string>
{
    public Uri Url { get; set; }

    public ShortentUrlCommand(Uri url)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
    }
}

