using MediatR;
using UrlShortener.Infrastructure;

namespace UrlShortener.Queries;

class FindUrlMappingQueryHandler : IRequestHandler<FindUrlMappingQuery, Uri>
{
    private readonly IShortUrlStorage storage;

    public FindUrlMappingQueryHandler(IShortUrlStorage storage)
    {
        this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public Task<Uri> Handle(FindUrlMappingQuery request, CancellationToken cancellationToken)
    {
        var mapping = this.storage.Get(request.ShortUrl);
        if (mapping != null)
        {
            return Task.FromResult(mapping.OriginalUrl);
        }

        return Task.FromResult<Uri>(null);
    }
}

