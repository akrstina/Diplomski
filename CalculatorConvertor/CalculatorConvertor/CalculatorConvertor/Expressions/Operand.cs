using System;

namespace CalculatorConvertor.Expressions
{
	public class Operand : ArithmeticElement
	{
		public double Value { get; }
        public override string Val => Value.ToString();
        public Operand(double value)
		{
			Value = value;
		}
		public Operand(string value, int fromBase=10)
		{
            if (fromBase == 10)
                Value = Convert.ToDouble(value);
            else Value = Convert.ToInt32(value, fromBase);
        }
	}
}
