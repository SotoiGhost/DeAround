using System;

using DeAround.Models;

namespace DeAround.Services {
	public interface IBluetoothService {
		BluetoothAuthorization GetStatus ();
		void SetUp ();
		void StartScanning ();
		void StopScanning ();
	}
}
