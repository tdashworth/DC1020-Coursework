using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Calculator
{
    class Utils
    {
        public static int Positive(int i)
        {
            if (i < 0)
                i = 0;

            return i;
        }

        public static int LocateEndingParenesis(string stringToSearchIn, int startIndex)
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
    }
}

namespace StringExtensions
{
    public static class StringExtensionsClass
    {
        public static int SecondToLastIndexOf(this string s, string lookup)
        {
            return s.Substring(0, s.LastIndexOf(lookup) - 1).LastIndexOf(lookup);
        }
    }
}
