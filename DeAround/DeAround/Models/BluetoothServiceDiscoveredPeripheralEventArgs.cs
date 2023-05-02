using System;

namespace DeAround.Models {
	public sealed class BluetoothServiceDiscoveredPeripheralEventArgs : EventArgs {
		public string DeviceName { get; private set; }

		public BluetoothServiceDiscoveredPeripheralEventArgs (string deviceName)
		{
			DeviceName = deviceName;
		}
	}
}
