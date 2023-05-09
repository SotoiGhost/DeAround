using System;

using DeAround.Droid.Services;

namespace DeAround.Droid.Singletons {
	public sealed class BluetoothServiceSingleton {
		static readonly Lazy<BluetoothService> lazyInstance = new (() => new BluetoothService ());

		public static BluetoothService SharedInstance => lazyInstance.Value;

		BluetoothServiceSingleton () { }
	}
}
