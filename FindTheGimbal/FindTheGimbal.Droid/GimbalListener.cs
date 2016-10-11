using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using AltBeaconOrg.BoundBeacon;
using Xamarin.Forms;

[assembly: Dependency(typeof(FindTheGimbal.Droid.GimbalListener))]

namespace FindTheGimbal.Droid
{
	public class GimbalListener: IGimbalListener
    {
		event EventHandler IGimbalListener.UpdateDisplay
		{
			add
			{
				throw new NotImplementedException();
			}

			remove
			{
				throw new NotImplementedException();
			}
		}

		public void listen() 
		{
		
		}

		void IGimbalListener.listen()
		{
			throw new NotImplementedException();
		}
	}
}