using System;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CalculatorConvertor.Expressions;

namespace CalculatorConvertor
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Convertor : ContentPage
	{
		private int _base = 0;
		public int Base
		{
			get { return _base; }
			set
			{
				if (_base == value) return;

				_base = value;

				OnPropertyChanged();
				hexDigit.ChangeCanExecute();
				decDigit.ChangeCanExecute();
				octDigit.ChangeCanExecute();
				binDigit.ChangeCanExecute();

				var textColor = (Color)Resources["TextColor"];
				lblHexSelect.TextColor = textColor;
				lblDecSelect.TextColor = textColor;
				lblOctSelect.TextColor = textColor;
				lblBinSelect.TextColor = textColor;

				Label lbl = null;

				switch (_base)
				{
					case 2: lbl = lblBinSelect; break;
					case 8: lbl = lblOctSelect; break;
					case 10: lbl = lblDecSelect; break;
					case 16: lbl = lblHexSelect; break;
				}

				if (lbl != null) lbl.TextColor = (Color)Resources["SelectedColor"];
			}
		}

		private Command hexDigit;
		public Command HexDigit => hexDigit;

		private Command decDigit;
		public Command DecDigit => decDigit;

		private Command octDigit;
		public Command OctDigit => octDigit;

		private Command binDigit;
		public Command BinDigit => binDigit;

		public Convertor()
		{
			InitializeComponent();

			hexDigit = new Command(() => { }, () => Base >= 16);
			decDigit = new Command(() => { }, () => Base >= 10);
			octDigit = new Command(() => { }, () => Base >= 8);
			binDigit = new Command(() => { }, () => Base >= 2);

			BindingContext = this;

			OnClear(this, EventArgs.Empty);
			Base = 10;
		}

		void OnBaseSelect(object sender, EventArgs e)
		{
			var lbl = sender as Label;

			if (lbl == null) return;
			else if (lbl == lblHexSelect) Base = 16;
			else if (lbl == lblDecSelect) Base = 10;
			else if (lbl == lblOctSelect) Base = 8;
			else if (lbl == lblBinSelect) Base = 2;

			long value = Convert.ToInt64(lblDec.Text);
			lblRez.Text = Convert.ToString(value, Base).ToUpperInvariant();
		}

		void OnClear(object sender, EventArgs e)
		{
			this.lblRez.Text = "0";

			this.lblHex.Text = "0";
			this.lblDec.Text = "0";
			this.lblOct.Text = "0";
			this.lblBin.Text = "0";
		}
		void OnDelete(object sender, EventArgs e)
		{
			this.lblRez.Text = this.lblRez.Text.Remove(this.lblRez.Text.Length - 1);
		}
		void OnCalculate(object sender, EventArgs e)
		{
			string exp = lblRez.Text;
			var infix = Convertors.Str2In(exp, Base);
			var postfix = Convertors.In2Post(infix);
			double result = Convertors.CalculateExpression(postfix);
			lblRez.Text = Convert.ToString((long)result, Base).ToUpperInvariant();
			
			DisplayInSystems((long)result);
		}
		void OnElementClick(object sender, EventArgs e)
		{
			string etext = ((Button)sender).Text;
			if (lblRez.Text == "0" && !(etext == "+" || etext == "-" || etext == "*" || etext == "/" || etext == "^")) lblRez.Text = string.Empty;
			string exp = lblRez.Text + etext;

			try
			{
				var infix = Convertors.Str2In(exp, Base);

				var infix2 = Convertors.Str2In(exp, Base);

				if (infix2.Last.Value is Operator || infix2.Last.Value is Parenthesis && infix2.Last.Value.Val == "(")
					infix2.AddLast(new Operand(0));

				int n = 0;
				foreach (var el in infix2)
				{
					if (el is Parenthesis)
					{
						if (el.Val == "(") ++n;
						else --n;
					}
				}
				for (int i = 0; i < n; ++i) infix2.AddLast(new Parenthesis(')'));

				var postfix = Convertors.In2Post(infix2);
				double result = Convertors.CalculateExpression(postfix);

				var sb = new StringBuilder();
				foreach (var el in infix)
				{
					if (el is Operand)
					{
						var operand = (Operand)el;
						var value = Convert.ToString((long)operand.Value, Base);
						sb.Append(value);
					}
					else sb.Append(el.Val);
				}

				lblRez.Text = sb.ToString().ToUpperInvariant();
				if (etext == ".") lblRez.Text += ".";

				if (infix2.Count == 1 && infix2.First.Value is Operand)
				{
					var operand = (Operand)infix2.First.Value;
					DisplayInSystems((long)operand.Value);
				}
			}
			catch { }
		}

		void DisplayInSystems(long value)
		{
			lblHex.Text = Convert.ToString(value, 16).ToUpperInvariant();
			lblDec.Text = Convert.ToString(value, 10).ToUpperInvariant();
			lblOct.Text = Convert.ToString(value, 8).ToUpperInvariant();
			lblBin.Text = Convert.ToString(value, 2).ToUpperInvariant();
		}
	}
}
