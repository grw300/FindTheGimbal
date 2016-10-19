using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindTheGimbal
{
    public partial class MainPage : ContentPage
    {
        IGimbalListener gimbalListener;
        public MainPage()
        {
            InitializeComponent();

            gimbalListener = DependencyService.Get<IGimbalListener>();

            gimbalListener.UpdateDisplay += (object sender, GimbalEventArgs e) =>
            {
                this.gimbalStatus.Text = e.message;
                this.BackgroundColor = e.color;
            };

            gimbalListener.UpdateDisplay += (object sender, GimbalEventArgs e) =>
            {
                this.personStatus.Text = e.message;
            };

            gimbalListener.listen();

            //this.Appearing += (object sender, EventArgs e) =>
            //{
            //    gimbalListener.speak();
            //};
            
        }

        

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    gimbalListener.speak();
        //}

    }
}
