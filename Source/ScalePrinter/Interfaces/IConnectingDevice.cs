using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScalePrinter.Client.Interfaces {
    public interface IConnectingDevice {
        event EventHandler<ConnectionChangedEventArgs> ConnectionStatusChanged;
    }

    public class ConnectionChangedEventArgs : EventArgs {
        public ConnectionChangedEventArgs(bool isConnected) {
            this.IsConnected = isConnected;
        }
        public bool IsConnected { get; private set; }
    }
}
