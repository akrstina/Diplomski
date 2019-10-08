using System;

namespace CalculatorConvertor.Expressions
{
	public class Parenthesis : ArithmeticElement
	{
		public char Value { get; }
        public override string Val => Value.ToString();

        public Parenthesis(char value)
		{
			if (value != '(' && value != ')') throw new Exception("Invalid parenthesis.");
			Value = value;
		}

		public override int IPR()
		{
			switch (Value)
			{
				case '(': return int.MaxValue;
				case ')': return 1;
			}

			throw new Exception("Invalid parenthesis.");
		}
		public override int SPR()
		{
			switch (Value)
			{
				case '(': return 0;
				case ')': throw new Exception("Closing parenthesis does not have SPR.");
			}

			throw new Exception("Invalid parenthesis.");
		}
	}
}
