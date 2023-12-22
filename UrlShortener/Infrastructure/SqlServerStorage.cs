using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Exceptions;

namespace UrlShortener.Infrastructure;

public class SqlServerStorage : IShortUrlStorage
{
    public ShortUrl Find(string shortUrl)
    {
        if (string.IsNullOrEmpty(shortUrl))
        {
            throw new ArgumentException($"{nameof(shortUrl)} cannot be null nor empty", nameof(shortUrl));
        }

        using (var db = new SqlServerContext())
        {
            return db.ShortUrls.FirstOrDefault(x => x.Slug == shortUrl);
        }
    }

    public ShortUrl Find(Uri originalUrl)
    {
        if (originalUrl == null)
        {
            throw new ArgumentNullException(nameof(originalUrl));
        }

        using (var db = new SqlServerContext())
        {
            return db.ShortUrls.FirstOrDefault(x => x.OriginalUrl == originalUrl);
        }
    }

    public void Save(ShortUrl shortUrl)
    {
        using (var db = new SqlServerContext())
        {
            try
            {
                db.Add(shortUrl);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    if (sqlException.Number == 2627)
                    {
                        throw new DuplicatedEntryException();
                    }
                }

                throw;
            }
        }
    }
}

