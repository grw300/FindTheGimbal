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
	public class GimbalListener: IGimbalListener, IBeaconConsumer
    {
        BeaconManager beaconManager;

        public event EventHandler<GimbalEventArgs> UpdateDisplay;

        public void listen() 
		{
            beaconManager = BeaconManager.GetInstanceForApplication(Android.App.Application.Context);
            beaconManager.BeaconParsers.Add(new BeaconParser().
                                                SetBeaconLayout("m:2-3=0215,i:4-19,i:20-21,i:22-23,p:24-24"));

            beaconManager.Bind(this);
        }
    }
}