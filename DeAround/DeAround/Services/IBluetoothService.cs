using System;

using DeAround.Models;

namespace DeAround.Services {
	public interface IBluetoothService {
		/// <summary>
		/// Event triggered when a new device is discovered.
		/// </summary>
		event EventHandler<BluetoothServiceDiscoveredDeviceEventArgs>? DiscoveredDevice;

		event EventHandler? UpdatedState;

		BluetoothPermissionStatus PermissionStatus { get; }

		/// <summary>
		/// Verifies if the Bluetooth hardware is available on the device.
		/// </summary>
		/// <returns></returns>
		bool IsSupported { get; }

		/// <summary>
		/// Verifies if the Bluetooth is enabled and ready to be used.
		/// </summary>
		/// <returns></returns>
		bool IsEnabled { get; }

		/// <summary>
		/// Start scanning for devices with the Bluetooth enabled.
		/// </summary>
		void StartScanning ();

		/// <summary>
		/// Stops scanning for devices.
		/// </summary>
		void StopScanning ();

		void RequestPermission ();
	}
}
