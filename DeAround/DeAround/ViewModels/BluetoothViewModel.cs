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
		public ICommand RequestBluetoothPermissionCommand { get; private set; }
		public ICommand StartSearchingCommand { get; private set; }

		public BluetoothPermissionStatus BluetoothPermissionStatus => bluetoothService.PermissionStatus;
		public bool IsBluetoothSupported => bluetoothService.IsSupported;
		public bool IsBluetoothEnabled => bluetoothService.IsEnabled;
		public bool IsSearching => bluetoothService.IsScanning;

		public BluetoothViewModel ()
		{
			RequestBluetoothPermissionCommand = new Command (RequestBluetoothPermission);
			StartSearchingCommand = new Command (StartSearching);
			//AddDummyData ();
			bluetoothService = DependencyService.Get<IBluetoothService> ();
			bluetoothService.UpdatedPermission += BluetoothService_UpdatedPermission;
			bluetoothService.UpdatedState += BluetoothService_ChangedState;
			bluetoothService.DiscoveredDevice += BluetoothService_DiscoveredDevice;
		}

		void RequestBluetoothPermission () => bluetoothService.RequestPermission ();

		void StartSearching ()
		{
			bluetoothService.StartScanning ();
			OnPropertyChanged (nameof (IsSearching));
		}

		void StopSearching ()
		{
			OnPropertyChanged (nameof (IsSearching));
			bluetoothService.StopScanning ();
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
