using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Xamarin.Forms;

using DeAround.Models;
using DeAround.Services;

namespace DeAround.ViewModels {
	public class BluetoothViewModel : BaseViewModel {

		IBluetoothService bluetoothService;

		public ObservableCollection<string> DeviceNames { get; } = new ();
		public ICommand InitializeBluetoothCommand { get; private set; }

		public BluetoothPermissionStatus BluetoothPermissionStatus { get => bluetoothService.PermissionStatus; }
		public bool IsBluetoothSupported { get => bluetoothService.IsSupported; }
		public bool IsBluetoothEnabled { get => bluetoothService.IsEnabled; }

		public BluetoothViewModel ()
		{
			InitializeBluetoothCommand = new Command (InitializeBluetooth);
			//AddDummyData ();
			bluetoothService = DependencyService.Get<IBluetoothService> ();
			bluetoothService.UpdatedPermission += BluetoothService_UpdatedPermission;
			bluetoothService.UpdatedState += BluetoothService_ChangedState;
			bluetoothService.DiscoveredDevice += BluetoothService_DiscoveredDevice;
		}

		void InitializeBluetooth ()
		{
			bluetoothService.StartScanning ();
		}

		void AddDummyData ()
		{
			for (int i = 1; i < 50; i++) {
				DeviceNames.Add ($"Device {i}");
			}
		}

		private void BluetoothService_UpdatedPermission (object sender, EventArgs e) =>
			OnPropertyChanged (nameof (BluetoothPermissionStatus));

		private void BluetoothService_ChangedState (object sender, EventArgs e)
		{
			OnPropertyChanged (nameof (IsBluetoothSupported));
			OnPropertyChanged (nameof (IsBluetoothEnabled));
		}

		void BluetoothService_DiscoveredDevice (object sender, BluetoothServiceDiscoveredDeviceEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace (e.DeviceName) && !DeviceNames.Contains (e.DeviceName))
				DeviceNames.Add (e.DeviceName);
		}
	}
}
