using MediatR;
using UrlShortener.Infrastructure;

namespace UrlShortener.Application.Queries;

class FindOriginalUrlQueryHandler : IRequestHandler<FindOriginalUrlQuery, Uri>
{
    private readonly IShortUrlStorage storage;

    public FindOriginalUrlQueryHandler(IShortUrlStorage storage)
    {
        this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public Task<Uri> Handle(FindOriginalUrlQuery request, CancellationToken cancellationToken)
    {
        var mapping = storage.Find(request.ShortUrl);
        if (mapping != null)
        {
            return Task.FromResult(mapping.OriginalUrl);
        }

        return Task.FromResult<Uri>(null);
    }
}

