using System;
using System.Collections.Generic;
using System.Linq;

namespace Scientific_Calculator
{
    public class CalculatorParser
    {
        public static double Resolve(string expressionStr)
        {
            string[] expressionAry = Clean(expressionStr);
            return Calculate(expressionAry);
        }

        private static string[] Clean(string expressionStr)
        {
            expressionStr = expressionStr.Replace(" ", "");

            foreach (var operation in Operator.List)
                expressionStr = expressionStr.Replace(operation.ToString(), " " + operation.ToString() + " ");

            return expressionStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static double Calculate(string[] expressionTerms)
        {
            List<ITerm> terms = expressionTerms.ToList();
            // Solve all functions
            for (int i = 0; i < terms.Count; i++)
            {
                if (terms[i] is Function)
                    terms[i] = new Operand(((Function)terms[i]).Calculate());
            }

            return -999999999999999999999999999999999999999999.0;
        }
    }
}
