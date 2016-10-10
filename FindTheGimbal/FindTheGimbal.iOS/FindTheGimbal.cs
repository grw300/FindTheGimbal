using System;
using System.Collections.Generic;
using System.Text;
using CoreLocation;
using CoreBluetooth;
using Foundation;
using System.Linq;

namespace FindTheGimbal
{
    public class FindTheGimbal
    {
        /* Figure out a way to get these from the Portable Project */
        static readonly string uuid = "E4C8A4FC-F68B-470D-959F-29382AF72CE7";
        static readonly string gimbalId = "Gimbal";

        CLLocationManager locationManager;
        CLProximity previousProximity;
        CLBeaconRegion beaconRegion;

        public event locationNotification FoundGimbal;
        public delegate void locationNotification(string body);

        public event updateDisplay UpdateDisplay;
        public delegate void updateDisplay(string text);

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
                            UpdateDisplay("You found the gimbal!");
                            break;
                        case CLProximity.Near:
                            UpdateDisplay("You're near the gimbal!");
                            break;
                        case CLProximity.Far:
                            UpdateDisplay("You're far from the gimbal!");
                            break;
                        case CLProximity.Unknown:
                            UpdateDisplay("Got no idea where the gimbal is!");
                            break;
                    }

                    previousProximity = beacon.Proximity;

                }
            };

            locationManager.StartMonitoring(beaconRegion);
            locationManager.StartRangingBeacons(beaconRegion);

        }

    }
}
