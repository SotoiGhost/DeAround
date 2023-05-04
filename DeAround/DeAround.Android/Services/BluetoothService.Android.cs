using System;
using System.Linq;

using Android;
using Android.App;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using Android.Content.PM;

using Xamarin.Forms;

using DeAround.Droid.Constants;
using DeAround.Droid.Callbacks;
using DeAround.Services;
using DeAround.Models;

[assembly: Dependency (typeof (DeAround.Droid.Services.BluetoothService))]
namespace DeAround.Droid.Services {
	public class BluetoothService : IBluetoothService {
		public const int RequestCode = 1000;

		#region Fields

		BluetoothManager? bluetoothManager;
		BluetoothAdapter? bluetoothAdapter;
		BluetoothLeScanner? bluetoothLeScanner;
		BluetoothLeScanCallback bluetoothLeScanCallback;

		#endregion

		#region Constructors

		public BluetoothService ()
		{
			bluetoothManager = (BluetoothManager) MainApplication.ActivityContext?.GetSystemService (Context.BluetoothService)!;
			bluetoothAdapter = bluetoothManager?.Adapter;
			bluetoothLeScanner = bluetoothAdapter?.BluetoothLeScanner;
			bluetoothLeScanCallback = new ();
			bluetoothLeScanCallback.DiscoveredDevice += BluetoothLeScanCallback_DiscoveredDevice;
		}

		#endregion

		#region IBluetoothService implementation

		public event EventHandler? UpdatedPermission;
		public event EventHandler? UpdatedState;
		public event EventHandler<BluetoothServiceDiscoveredDeviceEventArgs>? DiscoveredDevice;

		public BluetoothPermissionStatus PermissionStatus {
			get {
				var permission = BluetoothPermissionStatus.Allowed;
				var availablePermissions = PermissionConstants.BluetoothPermissions.Where (p => Android.OS.Build.VERSION.SdkInt >= p.Value);

				foreach (var (permissionName, _) in availablePermissions)
					if (MainApplication.ActivityContext?.CheckSelfPermission (Manifest.Permission.Bluetooth) == Permission.Denied) {
						permission = BluetoothPermissionStatus.NotAllowed;
						break;
					}

				return permission;
			}
		}

		public void RequestPermission ()
		{
			var availablePermissions = PermissionConstants.BluetoothPermissions.Where (p => Android.OS.Build.VERSION.SdkInt >= p.Value)
				.Select (kv => kv.Key)
				.ToArray ();
			var activity = (Activity) MainApplication.ActivityContext!;
			activity.RequestPermissions (availablePermissions, RequestCode);
		}

		public bool IsSupported => bluetoothAdapter != null;

		public bool IsEnabled => bluetoothAdapter?.IsEnabled ?? false;

		public void StartScanning ()
		{
			StopScanning ();
			bluetoothLeScanner?.StartScan (bluetoothLeScanCallback);
		}

		public void StopScanning ()
		{
			bluetoothLeScanner?.StopScan (bluetoothLeScanCallback);
		}

		public void OpenSettings ()
		{
			throw new NotImplementedException ();
		}

		public string OpenSettingsMessage => throw new NotImplementedException ();

		#endregion

		#region BluetoothLeScanCallback events

		void BluetoothLeScanCallback_DiscoveredDevice (object sender, BluetoothServiceDiscoveredDeviceEventArgs e) =>
			DiscoveredDevice?.Invoke (this, e);

		#endregion
	}
}
