using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Calculator
{
    public class CalculatorParser
    {
        public static decimal Resolve(string expressionStr)
        {
            expressionStr = expressionStr.Replace("+", " + ");
            expressionStr = expressionStr.Replace("-", " - ");
            expressionStr = expressionStr.Replace("/", " / ");
            expressionStr = expressionStr.Replace("*", " * ");

            string[] expressionAry = expressionStr.Split(' ');

            if (!Validate(expressionAry))
                throw new Exception("Invalid expression");

            return Calculate(expressionAry);
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
                    string[] validOperators = { "+", "-", "/", "*" };
                    if (!validOperators.Contains(expressionAry[index].Trim()))
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
                        result += Decimal.Parse(expressionAry[index+1]);
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
