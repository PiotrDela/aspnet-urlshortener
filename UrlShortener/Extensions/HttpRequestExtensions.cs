namespace UrlShortener.Extensions;

public static class HttpRequestExtensions
{
    public static Uri BaseUrl(this HttpRequest req)
    {
        ArgumentNullException.ThrowIfNull(req);

        var uriBuilder = new UriBuilder(req.Scheme, req.Host.Host, req.Host.Port ?? -1);
        if (uriBuilder.Uri.IsDefaultPort)
        {
            uriBuilder.Port = -1;
        }

        return uriBuilder.Uri;
    }
}

