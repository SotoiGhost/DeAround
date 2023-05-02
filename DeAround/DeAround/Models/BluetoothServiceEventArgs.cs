using System;

namespace DeAround.Models {
	public sealed class BluetoothServiceEventArgs : EventArgs {
		public BluetoothState State { get; private set; }

		public BluetoothServiceEventArgs (BluetoothState state)
		{
			State = state;
		}
	}
}

