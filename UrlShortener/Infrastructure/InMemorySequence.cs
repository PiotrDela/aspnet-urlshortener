namespace UrlShortener.Infrastructure;

class InMemorySequence : ISequence
{
    private static int currentValue = 10000;

    public int GetNext()
    {
        return Interlocked.Increment(ref currentValue);
    }
}

