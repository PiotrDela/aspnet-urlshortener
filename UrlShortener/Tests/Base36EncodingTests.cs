using UrlShortener.Domain;
using Xunit;

namespace UrlShortener.Tests
{
    public class Base36EncodingTests
    {
        [Theory]
        [InlineData(0, "0")]
        [InlineData(10, "A")]
        [InlineData(100, "2S")]
        [InlineData(1000, "RS")]
        [InlineData(10000, "7PS")]
        [InlineData(100000, "255S")]
        [InlineData(1000000, "LFLS")]
        [InlineData(1000000000, "GJDGXS")]
        public void ConvertionToBase36Test(int input, string expectedOutput)
        {
            Assert.Equal(expectedOutput, Base36Encoding.Convert(input));
        }
    }
}
