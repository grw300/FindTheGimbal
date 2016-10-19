using System;
namespace FindTheGimbal
{
	public interface IGimbalListener
	{
		void listen();
        void speak();
        void StartMonitoring();
		event EventHandler<GimbalEventArgs> UpdateDisplay;
        event EventHandler<GimbalEventArgs> FoundPerson;
	}
}
