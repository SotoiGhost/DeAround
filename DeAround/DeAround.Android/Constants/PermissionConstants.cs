using System;
using System.Collections.Generic;
using Android;

namespace DeAround.Droid.Constants {
	public static class PermissionConstants {
		public static Dictionary<string, Android.OS.BuildVersionCodes> BluetoothPermissions = new () {
			[Manifest.Permission.Bluetooth] = Android.OS.BuildVersionCodes.Donut,
			[Manifest.Permission.BluetoothAdmin] = Android.OS.BuildVersionCodes.Donut,
			[Manifest.Permission.BluetoothScan] = Android.OS.BuildVersionCodes.S,
		};
	}
}

