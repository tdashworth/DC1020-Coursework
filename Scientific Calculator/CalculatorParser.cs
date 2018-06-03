using System;
using System.Collections.Generic;
using System.Linq;

namespace Scientific_Calculator
{
    public class CalculatorParser
    {
        public readonly static Dictionary<string, Func<double, double, double>> Operators = new Dictionary<string, Func<double, double, double>>()
        {
            {"yroot",(x, y) => Math.Pow(x, 1/(double)y) },
            {"^",    (x, y) => Math.Pow(x, y) },
            {"Mod",  (x, y) => x % y },
            {"/",    (x, y) => x / y },
            {"*",    (x, y) => x * y },
            {"-",    (x, y) => x - y },
            {"+",    (x, y) => x + y }
        };
        public readonly static Dictionary<string, Func<double, double>> Functions = new Dictionary<string, Func<double, double>>()
        {
            {"sin",   (x) => Math.Sin(x) },
            {"cos",   (x) => Math.Cos(x) },
            {"tan",   (x) => Math.Tan(x) },
            {"asin",  (x) => Math.Asin(x) },
            {"acos",  (x) => Math.Acos(x) },
            {"atan",  (x) => Math.Atan(x) },
            {"log",   (x) => Math.Log(x) },
            {"√",     (x) => Math.Sqrt(x) },
            {"exp",   (x) => Math.Exp(x) },
            {"negate",(x) => x * -1 }
        };

        public static double Resolve(string expressionStr)
        {
            string[] expressionAry = Clean(expressionStr);
            return Calculate(expressionAry.ToList());
        }

        private static string[] Clean(string expressionStr)
        {
            expressionStr = expressionStr.Replace(" ", "");

            foreach (var operation in Operators.Keys)
                expressionStr = expressionStr.Replace(operation.ToString(), " " + operation.ToString() + " ");

            return expressionStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static double Calculate(List<string> expressionList)
        {
            List<double?> values = new List<double?>();

            // Parse and populate values into list performing any functions along the way
            foreach (string stringTerm in expressionList)
                values.Add(ParseTerm(stringTerm));

            // Perform operations in order of array (BIDMAS) 
            foreach (string operation in Operators.Keys)
                while (expressionList.Contains(operation))
                    // expressionList used to dermine location of operator
                    // values are updated and remove as they are calculated
                    SolveOperation(operation, ref expressionList, ref values);

            if (values.Count == 0)
                values.Add(0);

            return values[0].Value;
        }

        private static double? ParseTerm(string stringTerm)
        {
            Double tempNum = 0.0;

            if (Operators.Keys.Contains(stringTerm))
                // Not a value so not added to array but null space is 
                return null;
            else if (Functions.Keys.Contains(stringTerm.Split('(')[0]))
                // Solve function and place in list
                return SolveFunction(stringTerm);
            else if (Double.TryParse(stringTerm, out tempNum))
                // Place value in list
                return tempNum;
            else
                throw new Exception($"Unknow term in expression. Term = '{stringTerm}'");
        }

        private static void SolveOperation(string operation, ref List<string> expressionList, ref List<double?> values)
        {
            // Calculate result
            int indexOfOperator = expressionList.IndexOf(operation);
            double value1 = values[indexOfOperator - 1].Value;
            double value2 = values[indexOfOperator + 1].Value;
            double result = Operators[operation](value1, value2);

            // Populate result
            values[indexOfOperator] = result;
            expressionList[indexOfOperator] = "✓";

            // Clean up
            values.RemoveAt(indexOfOperator + 1);
            expressionList.RemoveAt(indexOfOperator + 1);
            values.RemoveAt(indexOfOperator - 1);
            expressionList.RemoveAt(indexOfOperator - 1);
        }

        private static double SolveFunction(string stringTerm)
        {
            string[] parts = stringTerm.Split(new char[] { '(', ')' });
            string funcName = parts[0];
            double value = Double.Parse(parts[1]);

            return Functions[funcName](value);
        }
    }
}
