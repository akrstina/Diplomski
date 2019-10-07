using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalculatorConvertor
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class BasicCalculator : ContentPage
    {
        int currentState = 1;
        string myOperator;
        double firstNumber, secondNumber;

        public BasicCalculator()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        void OnSelectedNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            string pressed = button.Text;

            if(this.lblRez.Text=="0" || currentState<0)
            {
                this.lblRez.Text = "";

                if (currentState < 0)
                    currentState *= -1;
            }

            this.lblRez.Text += pressed;
            double number;

            if (double.TryParse(this.lblRez.Text,out number))
            {
                this.lblRez.Text = number.ToString("");
                if(currentState==1)
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
            if (currentState==2)
            {
                var result = OperatorHelper.Calculate(firstNumber, secondNumber, myOperator);
                this.lblRez.Text = result.ToString();
                firstNumber = result;
                currentState = -1;

            }
        }
        void OnDot(object sender, EventArgs e)
        {
            if (lblRez.Text == firstNumber.ToString() +".")
                lblRez.Text = lblRez.Text;
            else
            lblRez.Text = firstNumber.ToString() +"."+ secondNumber.ToString();
        }

        private void BackToNavigationPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Navigation());
        }

    }
}