using System;
using System.Linq;

using Android.App;

using AltBeaconOrg.BoundBeacon;
using Xamarin.Forms;

[assembly: Dependency(typeof(FindTheGimbal.Droid.GimbalListener))]

namespace FindTheGimbal.Droid
{
	public class GimbalListener : Activity, IGimbalListener, IBeaconConsumer
	{
		static readonly string uuid = "487C659C-1FE2-4D2A-A289-130BBD7E534F";
		static readonly string gimbalId = "ER Room 1";

		BeaconManager beaconManager;
		MonitorNotifier monitorNotifier;
		RangeNotifier rangeNotifier;
		Region beaconRegion;

		public event locationNotification FoundGimbal;
		public delegate void locationNotification(string body);

		public event EventHandler<GimbalEventArgs> UpdateDisplay;

		public void listen()
		{
			beaconManager = BeaconManager.GetInstanceForApplication(Android.App.Application.Context);
			beaconManager.BeaconParsers.Add(new BeaconParser().
												SetBeaconLayout("m:2-3=0215,i:4-19,i:20-21,i:22-23,p:24-24"));

			beaconRegion = new Region(gimbalId, Identifier.Parse(uuid), null, null);
			monitorNotifier = new MonitorNotifier();
			rangeNotifier = new RangeNotifier();

			beaconManager.SetRangeNotifier(rangeNotifier);
			beaconManager.SetMonitorNotifier(monitorNotifier);

			monitorNotifier.EnterRegionComplete += (object obj, MonitorEventArgs e) =>
			{
				if (e.Region == beaconRegion)
				{
					FoundGimbal("There's a gimbal nearby!");
				}
			};


			rangeNotifier.DidRangeBeaconsInRegionComplete += (object sender, RangeEventArgs e) =>
			{
				if (e.Beacons.Count > 0)
				{
					var beacon = e.Beacons.FirstOrDefault();
					var l = beacon.Distance;

					if (isBetween(beacon.Distance, 0, 1))
					{
						OnUpdateDisplay(new GimbalEventArgs("You found the gimbal!", Color.Green));

					}
					else if (isBetween(beacon.Distance, 1, 3))
					{

						OnUpdateDisplay(new GimbalEventArgs("You're near the gimbal!", Color.Yellow));
					}
					else if (isBetween(beacon.Distance, 3, 10))
					{
						OnUpdateDisplay(new GimbalEventArgs("You're near the gimbal!", Color.Yellow));
					}
					else
					{
						OnUpdateDisplay(new GimbalEventArgs("Got no idea where the gimbal is!", Color.Gray));
					}

				}
			};
			beaconManager.Bind(this);

		}

		protected virtual void OnUpdateDisplay(GimbalEventArgs e)
		{
			UpdateDisplay?.Invoke(this, e);
		}

		public void OnBeaconServiceConnect()
		{
			beaconManager.StartRangingBeaconsInRegion(beaconRegion);
			beaconManager.StartMonitoringBeaconsInRegion(beaconRegion);
		}

		public static bool isBetween(double x, double lower, double upper)
		{
			return lower <= x && x <= upper;
		}
	}
}