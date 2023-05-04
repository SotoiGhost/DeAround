using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Essentials;

using DeAround.Constants;
using DeAround.Models;
using DeAround.Services;

namespace DeAround.ViewModels {
	public class BluetoothViewModel : BaseViewModel, IDisposable {

		IBluetoothService bluetoothService;

		public ObservableCollection<string> DeviceNames { get; } = new ();
		public ICommand RequestBluetoothPermissionCommand { get; private set; }
		public ICommand StartSearchingCommand { get; private set; }

		public bool IsFirstTime => Preferences.Get (PreferencesKeys.FirstTime, true);
		public BluetoothPermissionStatus BluetoothPermissionStatus => bluetoothService.PermissionStatus;
		public bool IsBluetoothSupported => bluetoothService.IsSupported;
		public bool IsBluetoothEnabled => bluetoothService.IsEnabled;
		public bool IsSearching => bluetoothService.IsScanning;

		public BluetoothViewModel ()
		{
			RequestBluetoothPermissionCommand = new Command (RequestBluetoothPermission);
			StartSearchingCommand = new Command (StartSearching);
			bluetoothService = DependencyService.Get<IBluetoothService> ();
			bluetoothService.UpdatedPermission += BluetoothService_UpdatedPermission;
			bluetoothService.UpdatedState += BluetoothService_ChangedState;
			bluetoothService.DiscoveredDevice += BluetoothService_DiscoveredDevice;
		}

		void RequestBluetoothPermission ()
		{
			Preferences.Set (PreferencesKeys.FirstTime, false);
			bluetoothService.RequestPermission ();
			OnPropertyChanged (nameof (IsFirstTime));
		}

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

		public void Dispose ()
		{
			bluetoothService.UpdatedPermission -= BluetoothService_UpdatedPermission;
			bluetoothService.UpdatedState -= BluetoothService_ChangedState;
			bluetoothService.DiscoveredDevice -= BluetoothService_DiscoveredDevice;
		}
	}
}
