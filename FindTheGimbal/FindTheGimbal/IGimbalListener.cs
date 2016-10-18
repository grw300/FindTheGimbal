using System;
namespace FindTheGimbal
{
	public interface IGimbalListener
	{
		void listen();
        void StartMonitoring();
		event EventHandler<GimbalEventArgs> UpdateDisplay;
	}
}
