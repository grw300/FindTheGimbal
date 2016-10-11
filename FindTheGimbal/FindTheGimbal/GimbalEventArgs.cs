using System;
using Xamarin.Forms;

namespace FindTheGimbal
{
	public class GimbalEventArgs : EventArgs
	{
		public string message;
		public Color color;
		public GimbalEventArgs(string message, Color color)
		{
			this.message = message;
			this.color = color;
		}
	}
}
