using System;

using CoreBluetooth;
using Foundation;

using DeAround.Models;

namespace DeAround.iOS.Delegates {
	public class DeAroundCentralManagerDelegate : CBCentralManagerDelegate {
		public event EventHandler? StateUpdated;
		public event EventHandler<BluetoothServiceDiscoveredDeviceEventArgs>? PeripheralDiscovered;

		public override void UpdatedState (CBCentralManager central)
		{
			StateUpdated?.Invoke (this, EventArgs.Empty);
		}

		public override void DiscoveredPeripheral (CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI)
		{
			PeripheralDiscovered?.Invoke (this, new BluetoothServiceDiscoveredDeviceEventArgs (peripheral.Name ?? ""));
		}
	}
}

