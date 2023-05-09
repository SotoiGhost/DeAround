using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Essentials;

using DeAround.Constants;
using DeAround.Models;
using DeAround.Services;

namespace DeAround.ViewModels {
	public class BluetoothViewModel : BaseViewModel {

		bool searching;
		IBluetoothService bluetoothService;
		CancellationTokenSource? source;
		CancellationToken? token;

		public ObservableCollection<string> DeviceNames { get; } = new ();
		public ICommand RequestBluetoothPermissionCommand { get; private set; }
		public ICommand StartSearchingCommand { get; private set; }
		public ICommand StopSearchingCommand { get; private set; }

		public bool IsFirstTime => Preferences.Get (PreferencesKeys.FirstTime, true);
		public BluetoothPermissionStatus BluetoothPermissionStatus => bluetoothService.PermissionStatus;
		public bool IsBluetoothSupported => bluetoothService.IsSupported;
		public bool IsBluetoothEnabled => bluetoothService.IsEnabled;
		public bool IsSearching {
			get => searching;
			set => SetProperty (ref searching, value);
		}

		public BluetoothViewModel ()
		{
			RequestBluetoothPermissionCommand = new Command (RequestBluetoothPermission);
			StartSearchingCommand = new Command (StartSearching);
			StopSearchingCommand = new Command (StopSearching);
			bluetoothService = DependencyService.Get<IBluetoothService> ();
			bluetoothService.UpdatedState += BluetoothService_ChangedState;
			bluetoothService.DiscoveredDevice += BluetoothService_DiscoveredDevice;
		}

		void RequestBluetoothPermission ()
		{
			bluetoothService.RequestPermission ();
		}

		void StartSearching ()
		{
			IsSearching = true;
			DeviceNames.Clear ();

			if (source != null) {
				source.Cancel ();
				source = null;
			}

			source = new CancellationTokenSource ();
			token = source.Token;

			Task.Factory.StartNew (async () => await SearchByIntervals (3, 1, (CancellationToken) token), (CancellationToken) token);
		}

		void StopSearching ()
		{
			source?.Cancel ();
			bluetoothService.StopScanning ();
			IsSearching = false;
		}

		async Task SearchByIntervals (int searchingIntervalInSeconds, int pauseIntervalInSeconds, CancellationToken token)
		{
			do {
				if (token.IsCancellationRequested) return;

				bluetoothService.StartScanning ();
				await Task.Delay (searchingIntervalInSeconds * 1000, token);

				bluetoothService.StopScanning ();
				await Task.Delay (pauseIntervalInSeconds * 1000, token);

				if (token.IsCancellationRequested) return;
			} while (true);
		}

		void BluetoothService_ChangedState (object sender, EventArgs e)
		{
			OnPropertyChanged (nameof (IsBluetoothSupported));
			OnPropertyChanged (nameof (IsBluetoothEnabled));

			if (IsFirstTime) {
				Preferences.Set (PreferencesKeys.FirstTime, false);
				OnPropertyChanged (nameof (IsFirstTime));

				if (BluetoothPermissionStatus == BluetoothPermissionStatus.Allowed)
					StartSearching ();
			}
		}

		void BluetoothService_DiscoveredDevice (object sender, BluetoothServiceDiscoveredDeviceEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace (e.DeviceName) && !DeviceNames.Contains (e.DeviceName))
				DeviceNames.Add (e.DeviceName);
		}

		public override void Dispose ()
		{
			bluetoothService.UpdatedState -= BluetoothService_ChangedState;
			bluetoothService.DiscoveredDevice -= BluetoothService_DiscoveredDevice;
		}
	}
}
