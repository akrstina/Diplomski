using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorConvertor.Expressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculatorConvertor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScientificCalculator : ContentPage
    {

        int currentState = 1;
        string myOperator;
        double firstNumber, secondNumber;

        public ScientificCalculator()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        void OnSelectedNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            string pressed = button.Text;

            if (this.lblRez.Text == "0" || currentState < 0)
            {
                this.lblRez.Text = "";

                if (currentState < 0)
                    currentState *= -1;
            }

            this.lblRez.Text += pressed;
            double number;

            if (double.TryParse(this.lblRez.Text, out number))
            {
                this.lblRez.Text = number.ToString("");
                if (currentState == 1)
                {
                    firstNumber = number;
                }
                else
                {
                    secondNumber = number;
                }
            }

        }
        void OnSelectedOperator(object sender, EventArgs e)
        {
            currentState = -2;
            Button button = (Button)sender;
            string pressed = button.Text;
            myOperator = pressed;
        }

        void OnClear(object sender, EventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            currentState = 1;
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
        void OnDot(object sender, EventArgs e)
        {
            if (lblRez.Text == firstNumber.ToString() + ".")
                lblRez.Text = lblRez.Text;
            else
                lblRez.Text = firstNumber.ToString() + "." + secondNumber.ToString();
        }


    }
}