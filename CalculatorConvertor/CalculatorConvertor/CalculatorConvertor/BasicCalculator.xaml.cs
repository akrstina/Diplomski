using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CalculatorConvertor.Expressions;

namespace CalculatorConvertor
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class BasicCalculator : ContentPage
    {

        public BasicCalculator()
        {
            InitializeComponent();
            OnClear(this, EventArgs.Empty);
        }

      

        void OnClear(object sender, EventArgs e)
        {
            this.lblRez.Text = "0";
        }

        void OnCalculate(object sender, EventArgs e)
        {
            string exp = lblRez.Text;
            var infix = Convertors.Str2In(exp);
            var postfix = Convertors.In2Post(infix);
            double result = Convertors.CalculateExpression(postfix);
            lblRez.Text = result.ToString();
        }
        void OnElementClick(object sender, EventArgs e)
        {
            string etext = ((Button)sender).Text;
            if (lblRez.Text == "0" && !(etext == "+" || etext == "-" || etext == "*" || etext == "/" || etext == "^"))
                lblRez.Text = string.Empty;
            string exp = lblRez.Text+ etext ;
            try
            {
                var infix = Convertors.Str2In(exp);

                var infix2 = Convertors.Str2In(exp);
                if (infix2.Last.Value is Operator || infix2.Last.Value is Parenthesis && infix2.Last.Value.Val == "(")
                    infix2.AddLast(new Operand(0));
                int n = 0;
                foreach(var el in infix2)
                {
                    if(el is Parenthesis)
                    {
                        if (el.Val == "(")

                            ++n;
                        else
                            --n;
                    }
                }
                for (int i = 0; i < n; ++i)
                    infix2.AddLast(new Parenthesis(')'));
                var postfix = Convertors.In2Post(infix2);
                double result = Convertors.CalculateExpression(postfix);

                var sb = new StringBuilder();
                foreach (var el in infix)
                {
                    sb.Append(el.Val);
                }
                lblRez.Text = sb.ToString();
                if(etext==".")
                {
                    lblRez.Text += ".";
                }
            }
            catch
            {

            }
        }

    }
}