using System;
using System.Collections.Generic;

namespace Scientific_Calculator
{
    interface ITerm { }

    class Operand : ITerm
    {
        readonly double value;

        public Operand(double _value) => value = _value;
        //public Value Resolve() => this;
        public double Calculate() => value;
    }

    class Operator : ITerm
    {
        public readonly static Dictionary<string, Func<double, double, double>> List = new Dictionary<string, Func<double, double, double>>()
        {
            {"yroot",(x, y) => Math.Pow(x, 1/(double)y) },
            {"^",    (x, y) => Math.Pow(x, y) },
            {"Mod",  (x, y) => x % y },
            {"/",    (x, y) => x / y },
            {"*",    (x, y) => x * y },
            {"+",    (x, y) => x + y },
            {"-",    (x, y) => x - y }
        };

        public string Name;
        private Func<double, double, double> Method;

        public Operator(string s)
        {
            Name = s;
            Method = List[Name];
        }

        public double Calculate(double num1, double num2)
        {
            return Method(num1, num2);
        }
    }



    class Function : ITerm
    {
        public readonly static Dictionary<string, Func<double, double>> List = new Dictionary<string, Func<double, double>>()
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

        public string Name;
        private Func<double, double> Method;
        private double Value;

        public Function(string s)
        {
            string[] parts = s.Split(new char[] { '(', ')' });

            Name = parts[0];
            Method = List[Name];
            Value = Double.Parse(parts[1]);
        }

        public double Calculate()
        {
            return Method(Value);
        }
    }
}
