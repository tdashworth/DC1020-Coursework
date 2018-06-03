namespace Scientific_Calculator
{
    public static class Utils
    {
        internal static int Positive(int i)
        {
            return i < 0 ? 0 : i;
        }

        internal static bool ValidBrackets(string calculation)
        {
            int count = 0;

            foreach(char c in calculation)
            {
                if (c == '(')
                    count++;
                if (c == ')')
                    count--;
            }

            return count == 0;
        }
    }
}

namespace StringExtensions
{
    public static class StringExtensionsClass
    {
        public static int SecondToLastIndexOf(this string s, string lookup) => s.Substring(0, s.LastIndexOf(lookup) - 1).LastIndexOf(lookup);
        public static string[] SplitAt(this string s, int index) => new string[] { s.Substring(0, index), s.Substring(index) };
    }
}
