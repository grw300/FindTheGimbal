using System;
namespace FindTheGimbal
{
	public interface IGimbalListener
	{
		void listen();
		event EventHandler<GimbalEventArgs> UpdateDisplay;
	}
}
