using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculatorConvertor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Navigation : ContentPage
    {
        public Navigation()
        {
            InitializeComponent();
        }

        private void GoToMainPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BasicCalculator());
        }
        private void GoToScientificCalculator_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScientificCalculator());
        }
        private void GoToConvertor_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Convertor());
        }
    }
}