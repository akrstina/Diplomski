using System;

namespace CalculatorConvertor.Expressions
{
	public abstract class ArithmeticElement
	{
        public abstract string Val {get; }
		public virtual int IPR() { throw new Exception("Unknown IPR."); }
		public virtual int SPR() { throw new Exception("Unknown SPR."); }
		public virtual int Rank() { throw new Exception("Unknown Rank."); }
	}
}
