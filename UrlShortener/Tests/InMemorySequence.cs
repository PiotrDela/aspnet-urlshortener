using UrlShortener.Infrastructure;

namespace UrlShortener.Tests
{
    class InMemorySequence : INumberSequence
    {
        private static int currentValue = 10000;

        public int GetNext()
        {
            return Interlocked.Increment(ref currentValue);
        }
    }
}
