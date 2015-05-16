using ScalePrinter.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScalePrinter.Client.Scale {
    public class ScaleSimulatorService : IScaleService {

        public event EventHandler ConnectionStatusChanged;


        public double CurrentWeight {
            get { throw new NotImplementedException(); }
        }

        public bool IsConnected {
            get { throw new NotImplementedException(); }
        }

        private void OnConnectionChanged(ConnectionChangedEventArgs e) {
            if (ConnectionStatusChanged != null) {
                ConnectionStatusChanged(this, e);
            }
        }
    }
}
