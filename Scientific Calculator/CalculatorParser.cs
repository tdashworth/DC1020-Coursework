using System;
using System.Collections.Generic;
using System.Linq;

namespace Scientific_Calculator
{
    public class CalculatorParser
    {
        readonly static string[] operators = { "+", "-", "/", "*", "%", "^" };

        public static double Resolve(string expressionStr)
        {
            PerformFunctions(ref expressionStr);
            string[] expressionAry = Clean(expressionStr);
            return Calculate(expressionAry);
        }

        private static string[] Clean(string expressionStr)
        {
            expressionStr = expressionStr.Replace(" ", "");
            foreach (var operation in operators)
                expressionStr = expressionStr.Replace(operation, " " + operation + " ");

            string[] expressionAry = expressionStr.Split(' ');
            return expressionAry;
        }

        private static double Calculate(string[] expressionAry)
        {
            double result = StringToDouble(expressionAry[0]);

            for (int index = 1; index < expressionAry.Length; index += 2)
            {
                switch (expressionAry[index].Trim())
                {
                    case "+":
                        result += StringToDouble(expressionAry[index + 1]);
                        break;
                    case "-":
                        result -= StringToDouble(expressionAry[index + 1]);
                        break;
                    case "*":
                        result *= StringToDouble(expressionAry[index + 1]);
                        break;
                    case "/":
                        result /= StringToDouble(expressionAry[index + 1]);
                        break;
                    case "^":
                        result = Math.Pow(result, StringToDouble(expressionAry[index + 1]));
                        break;
                    case "%":
                        result %= StringToDouble(expressionAry[index + 1]);
                        break;
                }
            }

            return result;
        }

        readonly static Dictionary<string, Func<double, double>> functions = new Dictionary<string, Func<double, double>>()
        {
            {"sin",   (x) => Math.Sin(x) },
            {"cos",   (x) => Math.Cos(x) },
            {"tan",   (x) => Math.Tan(x) },
            {"sin-1", (x) => Math.Asin(x) },
            {"cos-1", (x) => Math.Acos(x) },
            {"tan-1", (x) => Math.Atan(x) },
            {"log",   (x) => Math.Log(x) },
            {"root2", (x) => Math.Sqrt(x) },
            {"root3", (x) => Math.Pow(x, 1/(double)3) },
            {"exp",   (x) => Math.Exp(x) }

        };

        private static void PerformFunctions(ref string expressionStr)
        {
            foreach (var func in functions)
            {
                string funcName = func.Key + "(";
                while (expressionStr.Contains(funcName))
                {
                    int startBracket = expressionStr.IndexOf(funcName) + funcName.Length - 1;
                    int endBracket = Utils.LocateEndingParenesis(expressionStr, startBracket);
                    string bracketedValue = expressionStr.Substring(startBracket + 1, endBracket - startBracket - 1);
                    double calculatedValue = func.Value(StringToDouble(bracketedValue));

                    expressionStr =
                        expressionStr.Substring(0, startBracket - funcName.Length + 1) +
                        calculatedValue.ToString() +
                        expressionStr.Substring(endBracket + 1);
                }
            }
        }

        private static double StringToDouble (string s)
        {                
            if (s == "π")
                return Math.PI;

            return Double.Parse(s);
        }
    }
}
