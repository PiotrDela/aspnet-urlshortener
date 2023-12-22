using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Infrastructure;

public class NumberSequence : INumberSequence
{
    public const string SequenceName = nameof(NumberSequence);

    public int GetNext()
    {
        using (var dbContext = new SqlServerContext())
        {
            return dbContext.Database.SqlQuery<int>($"INSERT INTO NumberSequence OUTPUT INSERTED.ID DEFAULT VALUES").AsEnumerable().First();
        }
    }
}
