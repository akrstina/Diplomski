using System;
using System.Collections.Generic;

namespace CalculatorConvertor.Expressions
{
	public static class Convertors
	{
		public static LinkedList<ArithmeticElement> Str2In(string expression,int fromBase=10)
		{
			var infix = new LinkedList<ArithmeticElement>();

			string temp = string.Empty;
			int state = 0;

			foreach (char c in expression)
			{
				switch (state)
				{
					case 0: // start
						{
							if ('0' <= c && c <= '9' || 'A' <= c && c <= 'F')
							{
								temp = c.ToString();
								state = 1;
							}
							else if (c == '.')
							{
								temp = "0.";
								state = 2;
							}
							else if (c == '(' || c == ')')
							{
								var paren = new Parenthesis(c);
								infix.AddLast(paren);

								temp = string.Empty;
								state = 0;
							}
							else
							{
								temp += c;
								state = 3;
							}
						} break;
					case 1: // operand
						{
							if ('0' <= c && c <= '9' || 'A' <= c && c <= 'F')
							{
								temp += c;
								state = 1;
							}
							else if (c == '.')
							{
								temp += c;
								state = 2;
							}
							else
							{
								var operand = new Operand(temp,fromBase);
								infix.AddLast(operand);

								temp = c.ToString();

								if (c == '(' || c == ')')
								{
									var paren = new Parenthesis(c);
									infix.AddLast(paren);

									temp = string.Empty;
									state = 0;
								}
								else state = 3;
							}
						} break;
					case 2: // operand after .
						{
							if ('0' <= c && c <= '9' || 'A' <= c && c <= 'F')
							{
								temp += c;
								state = 2;
							}
							else if (c == '.')
							{
								throw new Exception("Dot already found!");
							}
							else
							{
								var operand = new Operand(temp,fromBase);
								infix.AddLast(operand);

								temp = c.ToString();

								if (c == '(' || c == ')')
								{
									var paren = new Parenthesis(c);
									infix.AddLast(paren);

									temp = string.Empty;
									state = 0;
								}
								else state = 3;
							}
						} break;
					case 3: // operator
						{
							// vec postoji prvi karakter operatora u tempu
							if (char.IsLetter(temp[0]))
							{
								if ('0' <= c && c <= '9' || 'A' <= c && c <= 'F' || c == '.' || c == '(' || c == ')')
								{
									var op = new Operator(temp);
									infix.AddLast(op);

									temp = c.ToString();

									if ('0' <= c && c <= '9' || 'A' <= c && c <= 'F') state = 1;
									else if (c == '.') state = 2;
									else
									{
										var paren = new Parenthesis(c);
										infix.AddLast(paren);

										temp = string.Empty;
										state = 0;
									}
								}
								else
								{
									temp += c;
									state = 3;
								}
							}
							else
							{
								var op = new Operator(temp);
								infix.AddLast(op);

								temp = c.ToString();

								if ('0' <= c && c <= '9' || 'A' <= c && c <= 'F') state = 1;
								else if (c == '.') state = 2;
								else if (c == '(' || c == ')')
								{
									var paren = new Parenthesis(c);
									infix.AddLast(paren);

									temp = string.Empty;
									state = 0;
								}
							}
						} break;
				}
			}

			if (state == 1 || state == 2)
			{
				var operand = new Operand(temp,fromBase);
				infix.AddLast(operand);
			}
            else if (state==3 )
            {
                var op = new Operator(temp);
                infix.AddLast(op);
            }
			return infix;
		}
		public static LinkedList<ArithmeticElement> In2Post(IEnumerable<ArithmeticElement> infix)
		{
			var postfix = new LinkedList<ArithmeticElement>();

			var s = new Stack<ArithmeticElement>();
			int rank = 0;

			foreach (var next in infix)
			{
				if (next is Operand)
				{
					postfix.AddLast(next);
					++rank;
				}
				else
				{
					while (s.Count != 0 && next.IPR() <= s.Peek().SPR())
					{
						var x = s.Pop();
						postfix.AddLast(x);
						rank -= x.Rank();
						if (rank < 1) throw new Exception("Invalid expression.");
					}

					if (next is Operator) s.Push(next);
					else if (next is Parenthesis)
					{
						var paren = (Parenthesis)next;

						if (paren.Value == '(') s.Push(next);
						else s.Pop();
					}
				}
			}

			while (s.Count != 0)
			{
				var x = s.Pop();
				postfix.AddLast(x);
				rank -= x.Rank();
			}

			if (rank != 1) throw new Exception("Invalid expression.");

			return postfix;
		}

		public static double CalculateExpression(LinkedList<ArithmeticElement> postfix)
		{
			Stack<double> s = new Stack<double>();

			foreach (var e in postfix)
			{
				if (e is Operand)
				{
					var val = (Operand)e;
					s.Push(val.Value);
				}
				else // if (e is Operator)
				{
					var op = (Operator)e;

					var n = op.Rank() + 1;

					double[] operands = new double[n];
					for (int i = n - 1; i >= 0; --i) operands[i] = s.Pop();

					s.Push(CalculateOperation(operands, op.Value));
				}
			}

			return s.Pop();
		}
		public static double CalculateOperation(double[] operands, string op)
		{
			// [!!!]
			switch (op)
			{
				case "sin": return Math.Sin(operands[0]);
                case "cos": return Math.Cos(operands[0]);
                case "tan": return Math.Tan(operands[0]);
                case "√": return Math.Sqrt(operands[0]);
                case "log": return Math.Log(operands[0]);
                case "exp": return Math.Exp(operands[0]);
                case "+": return operands[0] + operands[1];
				case "-": return operands[0] - operands[1];
				case "*": return operands[0] * operands[1];
				case "/": return operands[0] / operands[1];
				case "^": return Math.Pow(operands[0], operands[1]);
			}

			throw new Exception("Invalid operator.");
		}
	}
}
