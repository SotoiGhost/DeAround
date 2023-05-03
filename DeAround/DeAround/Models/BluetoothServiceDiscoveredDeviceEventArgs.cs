using System;

namespace DeAround.Models {
	public sealed class BluetoothServiceDiscoveredDeviceEventArgs : EventArgs {
		public string DeviceName { get; private set; }

		public BluetoothServiceDiscoveredDeviceEventArgs (string deviceName)
		{
			DeviceName = deviceName;
		}
	}
}
