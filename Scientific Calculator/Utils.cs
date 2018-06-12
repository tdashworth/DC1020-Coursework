using System;

namespace Scientific_Calculator
{
    public static class Utils
    {
        public enum AngleMode { Rad, Deg, Grad };

        internal static int Positive(int i)
        {
            return i < 0 ? 0 : i;
        }

        internal static double ConvertToRad(double value, AngleMode angleMode)
        {
            if (angleMode == AngleMode.Rad)
                return value;

            if (angleMode == AngleMode.Deg)
                return value * (Math.PI / 180);

            if (angleMode == AngleMode.Grad)
                return value * (Math.PI / 200);

            throw new Exception("Unknown angle mode.");
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
