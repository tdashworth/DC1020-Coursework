using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Calculator
{
    public class CalculatorParser
    {
        readonly static string[] operators = { "+", "-", "/", "*" };

        public static decimal Resolve(string expressionStr)
        {
            string[] expressionAry = Clean(expressionStr);

            if (!Validate(expressionAry))
                throw new Exception("Invalid expression");

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

        private static bool Validate(string[] expressionAry)
        {
            if (expressionAry.Length < 3 || expressionAry.Length % 2 == 0)
                return false;

            for (int index = 0; index < expressionAry.Length; index++)
            {
                if (index % 2 == 0) // Index is even, must be a number
                {
                    if (!Decimal.TryParse(expressionAry[index], out var tempResult))
                        return false;
                }
                else // Index is old, must be an operator
                {
                    if (!operators.Contains(expressionAry[index].Trim()))
                        return false;
                }
            }

            return true;
        }

        private static decimal Calculate(string[] expressionAry)
        {
            decimal result = Decimal.Parse(expressionAry[0]);

            for (int index = 1; index < expressionAry.Length; index += 2)
            {
                switch (expressionAry[index].Trim())
                {
                    case "+":
                        result += Decimal.Parse(expressionAry[index + 1]);
                        break;
                    case "-":
                        result -= Decimal.Parse(expressionAry[index + 1]);
                        break;
                    case "*":
                        result *= Decimal.Parse(expressionAry[index + 1]);
                        break;
                    case "/":
                        result /= Decimal.Parse(expressionAry[index + 1]);
                        break;
                }
            }

            return result;
        }
    }
}
