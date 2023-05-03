using System;

using DeAround.Models;

using Android.Bluetooth.LE;
using Android.Runtime;

namespace DeAround.Droid.Callbacks {
	internal class BluetoothLeScanCallback : ScanCallback {
		public event EventHandler<BluetoothServiceDiscoveredDeviceEventArgs>? DiscoveredDevice;

		public override void OnScanResult ([GeneratedEnum] ScanCallbackType callbackType, ScanResult? result)
		{
			base.OnScanResult (callbackType, result);

			if (result?.Device == null)
				return;

			DiscoveredDevice?.Invoke (this, new BluetoothServiceDiscoveredDeviceEventArgs (result.Device.Name));
		}
	}
}

