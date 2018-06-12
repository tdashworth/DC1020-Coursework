using System;
using System.Collections.Generic;
using System.Linq;
using static Scientific_Calculator.Utils;

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
            {"+",    (x, y) => x + y },
            {"-",    (x, y) => x - y }

        };
        public readonly static Dictionary<string, Func<double, double>> Functions = new Dictionary<string, Func<double, double>>()
        {
            {"sin",      (x) => Math.Sin(x) },
            {"cos",      (x) => Math.Cos(x) },
            {"tan",      (x) => Math.Tan(x) },
            {"asin",     (x) => Math.Asin(x) },
            {"acos",     (x) => Math.Acos(x) },
            {"atan",     (x) => Math.Atan(x) },
            {"log",      (x) => Math.Log(x) },
            {"√",        (x) => Math.Sqrt(x) },
            {"exp",      (x) => Math.Exp(x) },
            {"negate",   (x) => x * -1 },
            {"brackets", (x) => x }
        };


        /// <summary>
        /// Entry point into parser 
        /// </summary>
        /// <param name="expressionStr"></param>
        /// <returns></returns>
        public static double Resolve(string expressionStr, AngleMode angleMode = AngleMode.Rad)
        {
            if (!Validation.Brackets(expressionStr))
                throw new Exception("Invalid brackets");

            if (expressionStr.Length == 0)
                // There are no terms so default to 0
                return 0;

            string[] expressionAry = Clean(expressionStr);
            return Calculate(expressionAry.ToList(), angleMode);
        }

        /// <summary>
        /// Splits a string expression into its root components of operator, operands and functions
        /// </summary>
        /// <param name="expressionStr"> String expression </param>
        /// <returns></returns>
        private static string[] Clean(string expressionStr)
        {
            expressionStr = " " + expressionStr.Replace(" ", "");

            foreach (var operation in Operators.Keys)
                expressionStr = expressionStr.Replace(operation.ToString(), " " + operation.ToString() + " ");

            expressionStr = expressionStr.Replace(" (", " brackets(");
            expressionStr = expressionStr.Replace("(", " ( ");
            expressionStr = expressionStr.Replace(")", " ) ");

            List<string> terms = expressionStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            CondenseBracketedTerms(ref terms);

            if (terms[0] == "-")
                // Append "0" to the front as "-" cannot be used as a sign
                terms.Insert(0, "0");

            return terms.ToArray();
        }

        /// <summary>
        /// Scans for "(" and ")" and concatenates those terms into a single element
        /// </summary>
        /// <param name="terms"></param>
        private static void CondenseBracketedTerms(ref List<string> terms)
        {
            int functionNameLocation = -1;
            int nestingLevel = 0;
            for (int i = 0; i < terms.Count; i++)
            {
                string stringTerm = terms[i];

                if (stringTerm == "(")
                {
                    nestingLevel++;
                    if (nestingLevel == 1)
                        // Only searching for one level of brackets
                        // any deeper nesting with be captured by recursion
                        functionNameLocation = i - 1;
                }

                if (functionNameLocation != -1)
                {
                    terms[functionNameLocation] += stringTerm;
                    terms.RemoveAt(i);
                    i--;
                }

                if (stringTerm == ")")
                {
                    if (nestingLevel == 1)
                        // Only searching for one level of brackets
                        // any deeper nesting with be captured by recursion
                        functionNameLocation = -1;
                    nestingLevel--;
                }
            }
        }

        /// <summary>
        /// The cleaned list of terms are now calculated following the BIDMAS order of precedence
        /// </summary>
        /// <param name="expressionList">A list of strings split into there individual operators, functions and operands </param>
        /// <returns></returns>
        private static double Calculate(List<string> expressionList, AngleMode angleMode)
        {
            if (expressionList.Count % 2 == 0)
                // Number of terms must be odd to be valid
                throw new Exception("Invalid expression");

            List<double?> values = new List<double?>();

            // Parse and populate values into list performing any functions (and brackets) along the way
            foreach (string stringTerm in expressionList)
                values.Add(ParseTerm(stringTerm, angleMode));

            // Perform operations in order of array (_IDM__) ignoring AS
            List<string> operatorsToCalculate = Operators.Keys.ToList().GetRange(0, Operators.Count - 2);
            foreach (string operation in operatorsToCalculate)
                SolveOperation(operation, ref expressionList, ref values);

            // Perform +, - operations from left to right
            while (expressionList.Contains("+") || expressionList.Contains("-"))
                SolveOperation(expressionList[1], ref expressionList, ref values);

            if (values[0].Value.Equals(Double.NaN))
                throw new Exception("Invalid input");

            return values[0].Value;
        }

        /// <summary>
        /// Determine what the given string is and calculate if necessary 
        /// </summary>
        /// <param name="stringTerm"></param>
        /// <returns> Type of double? because if the term could not be a number </returns>
        private static double? ParseTerm(string stringTerm, AngleMode angleMode)
        {
            Double tempNum = 0.0;

            if (Operators.Keys.Contains(stringTerm))
                // Not a value so not added to array but null space is 
                // Null space is to maintain indexs between the two Lists (string, double)
                return null;
            else if (Functions.Keys.Contains(stringTerm.Split('(')[0]))
                // Solve function and place in list
                return SolveFunction(stringTerm, angleMode);
            else if (Double.TryParse(stringTerm, out tempNum))
                // Place value in list
                return tempNum;
            else
                throw new Exception($"Unknow term in expression. Term = '{stringTerm}'");
        }

        /// <summary>
        /// Performs the passed operation from left to right within the expression list
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="expressionList"></param>
        /// <param name="values"></param>
        private static void SolveOperation(string operation, ref List<string> expressionList, ref List<double?> values)
        {
            while (expressionList.Contains(operation))
            {
                // expressionList used to dermine location of operator
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
        }

        /// <summary>
        /// Performs the function named before the "(" using the value/expression between the "(" and ")"
        /// </summary>
        /// <param name="stringTerm"></param>
        /// <returns></returns>
        private static double SolveFunction(string stringTerm, AngleMode angleMode)
        {
            string[] parts = stringTerm.Split(new char[] { '(' }, 2);
            string funcName = parts[0];
            double value = Resolve(parts[1].Substring(0, parts[1].Length - 1));

            string[] trigFunctions = new string[] { "sin", "cos", "tan", "asin", "acos", "atan" };
            if (trigFunctions.Contains(funcName))
                value = ConvertToRad(value, angleMode);

            return Functions[funcName](value);
        }

        class Validation
        {
            internal static bool Brackets(string calculation)
            {
                int count = 0;

                foreach (char c in calculation)
                {
                    if (c == '(')
                        count++;
                    else if (c == ')')
                        count--;
                }

                return count == 0;
            }
        }
    }
}
