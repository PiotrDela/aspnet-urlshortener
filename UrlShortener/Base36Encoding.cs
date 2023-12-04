using System.Text;

namespace UrlShortener;

static class Base36Encoding
{
    private static readonly char[] Alphabet = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

    public static string Convert(int number)
    {
        var stack = new Stack<int>();
        var alphabetLength = Alphabet.Length;
        do
        {
            var mod = number % alphabetLength;
            stack.Push(mod);
            number /= alphabetLength;
        }
        while (number > 0);

        var builder = new StringBuilder();
        while (stack.Count > 0)
        {
            builder.Append(Alphabet[stack.Pop()]);
        }
        return builder.ToString();
    }
}

