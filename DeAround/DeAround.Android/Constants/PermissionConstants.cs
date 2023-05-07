using System;
using System.Collections.Generic;
using Android;

namespace DeAround.Droid.Constants {
	public static class PermissionConstants {
		public static Dictionary<string, Android.OS.BuildVersionCodes> RuntimePermissions = new () {
			[Manifest.Permission.AccessCoarseLocation] = Android.OS.BuildVersionCodes.Donut,
			[Manifest.Permission.AccessFineLocation] = Android.OS.BuildVersionCodes.Donut,
			[Manifest.Permission.BluetoothConnect] = Android.OS.BuildVersionCodes.S,
			[Manifest.Permission.BluetoothScan] = Android.OS.BuildVersionCodes.S,
		};
	}
}
