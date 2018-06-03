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
            ITerm[] expressionTerms = Parse(expressionAry);
            return Calculate(expressionTerms);
        }

        private static string[] Clean(string expressionStr)
        {
            expressionStr = expressionStr.Replace(" ", "");

            foreach (var operation in Operator.List)
                expressionStr = expressionStr.Replace(operation.ToString(), " " + operation.ToString() + " ");

            return expressionStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static ITerm[] Parse(string[] expressionAry)
        {
            List<ITerm> terms = new List<ITerm>();

            foreach (string stringTerm in expressionAry)
            {
                Double tempNum = 0.0;

                if (Operator.List.Keys.Contains(stringTerm))
                    terms.Add(new Operator(stringTerm));
                else if (Function.List.Keys.Contains(stringTerm.Split('(')[0]))
                    terms.Add(new Function(stringTerm));
                else if (Double.TryParse(stringTerm, out tempNum))
                    terms.Add(new Operand(tempNum));
                else
                    throw new Exception($"Unknow term in expression. Term = '{stringTerm}'");
            }

            return terms.ToArray();
        }

        private static double Calculate(ITerm[] expressionTerms)
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
