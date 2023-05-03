using System;

using CoreBluetooth;
using UIKit;
using Xamarin.Forms;

using DeAround.Models;
using DeAround.Services;
using Foundation;

[assembly: Dependency (typeof (DeAround.iOS.Services.BluetoothService))]
namespace DeAround.iOS.Services {
	public sealed class BluetoothService : IBluetoothService {

		#region Fields
		CBCentralManager? bluetoothCentralManager;
		#endregion

		#region Constructors

		public BluetoothService () { }

		#endregion

		#region IBluetoothService implementation

		public event EventHandler? UpdatedPermission;
		public event EventHandler? UpdatedState;
		public event EventHandler<BluetoothServiceDiscoveredDeviceEventArgs>? DiscoveredDevice;

		[System.Diagnostics.CodeAnalysis.SuppressMessage ("Code Notifications", "XI0002:Notifies you from using newer Apple APIs when targeting an older OS version", Justification = "This is already handled within the property.")]
		public BluetoothPermissionStatus PermissionStatus {
			get {
				if (UIDevice.CurrentDevice.CheckSystemVersion (13, 0))
					return CBCentralManager.Authorization switch {
						CBManagerAuthorization.AllowedAlways => BluetoothPermissionStatus.Allowed,
						CBManagerAuthorization.Denied or CBManagerAuthorization.Restricted => BluetoothPermissionStatus.NotAllowed,
						_ => BluetoothPermissionStatus.Unknown
					};

				return BluetoothPermissionStatus.Allowed;
			}
		}

		public void RequestPermission ()
		{
			bluetoothCentralManager = new CBCentralManager ();
			bluetoothCentralManager.UpdatedState += DidUpdateState;
			bluetoothCentralManager.DiscoveredPeripheral += DidDiscoverPeripheral;

			UpdatedPermission?.Invoke (this, EventArgs.Empty);
		}

		public bool IsSupported => bluetoothCentralManager?.State != CBCentralManagerState.Unsupported;

		public bool IsEnabled => bluetoothCentralManager?.State == CBCentralManagerState.PoweredOn;

		public void StartScanning () =>
			bluetoothCentralManager?.ScanForPeripherals (peripheralUuids: null, options: (NSDictionary?) null);

		public void StopScanning () =>
			bluetoothCentralManager?.StopScan ();

		public void OpenSettings ()
		{
			throw new NotImplementedException ();
		}

		public string OpenSettingsMessage => "";

		#endregion

		#region CBCentralManager event handlers

		private void DidUpdateState (object sender, EventArgs e)
		{
			UpdatedState?.Invoke (this, EventArgs.Empty);
		}

		private void DidDiscoverPeripheral (object sender, CBDiscoveredPeripheralEventArgs e)
		{
			DiscoveredDevice?.Invoke (this, new BluetoothServiceDiscoveredDeviceEventArgs (e.Peripheral.Name ?? ""));
		}

		#endregion
	}
}
