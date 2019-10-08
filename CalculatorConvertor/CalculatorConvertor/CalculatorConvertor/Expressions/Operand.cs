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
		public Operand(string value)
		{
			Value = Convert.ToDouble(value);
		}
	}
}
