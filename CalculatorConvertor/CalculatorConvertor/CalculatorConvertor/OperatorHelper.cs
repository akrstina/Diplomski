using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorConvertor
{
    class OperatorHelper
    {

        public static double Calculate(double value1, double value2, string myoperator)
        {
            double result = 0;
            switch(myoperator)
            {
                case "/":
                    result = value1 / value2;
                    break;
                case "*":
                    result = value1 * value2;
                    break;
                case "+":
                    result = value1 + value2;
                    break;
                case "-":
                    result = value1 - value2;
                    break;
                case "x^2":
                    result = Math.Pow(value1,2); 
                    break;
                case "x^y":
                    result = Math.Pow(value1,value2);
                    break;
                case "√x":
                    result = Math.Sqrt(value1);
                    break;
                case "log":
                    result= Math.Log10(value1);
                    break;
                case "exp":
                    result = Math.Exp(value1);
                    break;
                case "sin":
                    result = Math.Sin(value1);
                    break;
                case "cos":
                    result = Math.Cos(value1);
                    break;
                case "tan":
                    result = Math.Tan(value1);
                    break;
                case "n!":
                    result = value1;
                    break;


            }
            return result;
        }
    }
}
