namespace Scientific_Calculator
{
    public static class Utils
    {
        internal static int Positive(int i)
        {
            if (i < 0)
                i = 0;

            return i;
        }

        internal static int LocateEndingParenesis(string stringToSearchIn, int startIndex)
        {
            bool paraenesisExist = false;
            int counter = 0;
            int openParaenesis = 0;
            int closeParaenesis = 0;
            for (var i = 0; i < stringToSearchIn.Length; i++)
            {
                char currentCharacter = stringToSearchIn[i];

                if (currentCharacter == '(')
                {
                    if (paraenesisExist == false)
                    {
                        openParaenesis = i;
                        paraenesisExist = true;
                    }
                    counter++;
                }
                else if (currentCharacter == ')')
                {
                    counter--;

                    if (paraenesisExist)
                        closeParaenesis = i;

                    if (counter == 0)
                        paraenesisExist = false;

                    if (openParaenesis == startIndex)
                        return closeParaenesis;
                }
            }

            return -1;
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
