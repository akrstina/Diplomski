using System;

namespace CalculatorConvertor.Expressions
{
	public class Operator : ArithmeticElement
	{

		public string Value { get; }
        public override string Val => Value;
        public Operator(string value)
		{
			Value = value;
		}

		public override int IPR()
		{
			// Bitno da prioriteti krecu od 2!
			switch (Value)
			{
				case "+": case "-": return 2;
				case "*": case "/": return 3;
				case "^": return 5;
				case "sin": case "cos": case "tan": case "exp": case "√": case "log":  return int.MaxValue - 1;
			}

			throw new Exception("Invalid operator.");
		}
		public override int SPR()
		{
			// Bitno da prioriteti krecu od 2!
			switch (Value)
			{
				case "+": case "-": return 2;
				case "*": case "/": return 3;
				case "^": return 4;
				case "sin": case "cos": case "tan": case "exp": case "√": case "log": return int.MaxValue - 2;
			}

			throw new Exception("Invalid operator.");
		}
		public override int Rank()
		{
			// n-arnost operatora - 1!
			switch (Value)
			{
				case "sin":
                case "cos":
                case "tan":
                case "exp":
                case "√":
                case "log":
                    return 0;
				case "+": case "-":
				case "*": case "/":
				case "^":
					return 1;
			}

			throw new Exception("Invalid operator.");
		}
	}
}
