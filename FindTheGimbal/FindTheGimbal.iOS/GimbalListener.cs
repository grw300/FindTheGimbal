using System;
using System.Collections.Generic;
using System.Text;
using CoreLocation;
using CoreBluetooth;
using Foundation;
using System.Linq;
using Xamarin.Forms;

[assembly: Dependency(typeof(FindTheGimbal.iOS.GimbalListener))]

namespace FindTheGimbal.iOS
{

	public class GimbalListener: IGimbalListener
    {
        /* Figure out a way to get these from the Portable Project */
        static readonly string uuid = "487C659C-1FE2-4D2A-A289-130BBD7E534F";
        static readonly string gimbalId = "ER Room 1";

        CLLocationManager locationManager;
        CLProximity previousProximity;
        CLBeaconRegion beaconRegion;

        public event locationNotification FoundGimbal;
        public delegate void locationNotification(string body);

		public event EventHandler<GimbalEventArgs> UpdateDisplay;

        public void listen()
        {
            var gimbalUUID = new NSUuid(uuid);
            beaconRegion = new CLBeaconRegion(gimbalUUID, gimbalId);

            beaconRegion.NotifyEntryStateOnDisplay = true;
            beaconRegion.NotifyOnEntry = true;
            beaconRegion.NotifyOnExit = true;

            locationManager = new CLLocationManager();

            locationManager.RequestAlwaysAuthorization();

            locationManager.RegionEntered += (object sender, CLRegionEventArgs e) =>
            {
                if (e.Region.Identifier == gimbalId)
                {
                    FoundGimbal("There's a gimbal nearby!");
                }
            };

            locationManager.DidRangeBeacons += (object sender, CLRegionBeaconsRangedEventArgs e) =>
            {
                if (e.Beacons.Length > 0)
                {
                    var beacon = e.Beacons.FirstOrDefault();

                    switch ((CLProximity)beacon.Proximity)
                    {
                        case CLProximity.Immediate:
							OnUpdateDisplay(new GimbalEventArgs("You found the gimbal!", Color.Green));
                            break;
                        case CLProximity.Near:
							OnUpdateDisplay(new GimbalEventArgs("You're near the gimbal!", Color.Yellow));
                            break;
                        case CLProximity.Far:
							OnUpdateDisplay(new GimbalEventArgs("You're far from the gimbal!", Color.Red));
                            break;
                        case CLProximity.Unknown:
							OnUpdateDisplay(new GimbalEventArgs("Got no idea where the gimbal is!", Color.Gray));
                            break;
                    }

                    previousProximity = beacon.Proximity;

                }
            };

            locationManager.StartMonitoring(beaconRegion);
            locationManager.StartRangingBeacons(beaconRegion);

        }

		protected virtual void OnUpdateDisplay(GimbalEventArgs e)
		{
            UpdateDisplay?.Invoke(this, e);
        }
}
}
