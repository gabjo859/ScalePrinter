using ScalePrinter.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScalePrinter.Client.Scale {
    class DymoS100ScaleService : IScaleService {
        public double CurrentWeight {
            get { throw new NotImplementedException(); }
        }

        public bool IsConnected {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler ConnectionStatusChanged;

        private void OnConnectionStatusChanged() {
            if(ConnectionStatusChanged != null) {
                ConnectionStatusChanged(this, new ConnectionChangedEventArgs(IsConnected));
            }
        }
    }
}
