using MediatR;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Exceptions;
using UrlShortener.Infrastructure;

namespace UrlShortener.Application.Commands;

class ShortenUrlCommandHandler : IRequestHandler<ShortentUrlCommand, ShortUrl>
{
    private readonly INumberSequence sequence;
    private readonly IShortUrlStorage storage;

    public ShortenUrlCommandHandler(INumberSequence sequence, IShortUrlStorage storage)
    {
        this.sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
        this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public Task<ShortUrl> Handle(ShortentUrlCommand request, CancellationToken cancellationToken)
    {
        var mapping = storage.Find(request.Url);
        if (mapping != null)
        {
            throw new DuplicatedEntryException();
        }

        var shortUrl = ShortUrl.Create(request.Url, sequence);

        storage.Save(shortUrl);

        return Task.FromResult(shortUrl);
    }
}

