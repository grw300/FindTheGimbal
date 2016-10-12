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
        public MainPage()
        {
            InitializeComponent();

			var gimbalListener = DependencyService.Get<IGimbalListener>();

			gimbalListener.UpdateDisplay += (object sender, GimbalEventArgs e) =>
			{
				this.gimbalStatus.Text = e.message;
				this.BackgroundColor = e.color;
			};

			gimbalListener.listen();
        }
    }
}
