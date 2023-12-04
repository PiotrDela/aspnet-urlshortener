using MediatR;
using UrlShortener.Controllers;
using UrlShortener.Infrastructure;

namespace UrlShortener.Commands;

class ShortenUrlCommandHandler : IRequestHandler<ShortentUrlCommand, string>
{
    private readonly ISequence sequence;
    private readonly IShortUrlStorage storage;

    public ShortenUrlCommandHandler(ISequence sequence, IShortUrlStorage storage)
    {
        this.sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
        this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public Task<string> Handle(ShortentUrlCommand request, CancellationToken cancellationToken)
    {
        var id = this.sequence.GetNext();
        var shortUrl = new ShortUrl(request.Url, Base36Encoding.Convert(id));

        this.storage.Save(shortUrl);
        return Task.FromResult(shortUrl.Slug);
    }
}

