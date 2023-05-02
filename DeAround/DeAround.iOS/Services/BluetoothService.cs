using System;

using CoreBluetooth;
using UIKit;
using Xamarin.Forms;

using DeAround.Models;
using DeAround.Services;

[assembly: Dependency (typeof (DeAround.iOS.Services.BluetoothService))]
namespace DeAround.iOS.Services {
	public sealed class BluetoothService : IBluetoothService {
		public

		CBCentralManager bluetoothCentralManager;

		public BluetoothService ()
		{
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Code Notifications", "XI0002:Notifies you from using newer Apple APIs when targeting an older OS version", Justification = "This is already handled within the method.")]
		public BluetoothAuthorization GetStatus ()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (13, 0))
				return CBCentralManager.Authorization switch {
					CBManagerAuthorization.AllowedAlways => BluetoothAuthorization.Allowed,
					CBManagerAuthorization.Denied or CBManagerAuthorization.Restricted => BluetoothAuthorization.NotAllowed,
					_ => BluetoothAuthorization.Unknown
				};

			return BluetoothAuthorization.Allowed;
		}

		public void SetUp ()
		{
			bluetoothCentralManager = new CBCentralManager ();
			bluetoothCentralManager.UpdatedState += DidUpdateState;
			bluetoothCentralManager.DiscoveredPeripheral += DidDiscoverPeripheral;
		}

		public void StartScanning () =>
			bluetoothCentralManager.ScanForPeripherals (peripheralUuids: null);

		public void StopScanning () =>
			bluetoothCentralManager.StopScan ();

		private void DidUpdateState (object sender, EventArgs e)
		{

		}

		private void DidDiscoverPeripheral (object sender, CBDiscoveredPeripheralEventArgs e)
		{
			e.Peripheral.
		}
	}
}
