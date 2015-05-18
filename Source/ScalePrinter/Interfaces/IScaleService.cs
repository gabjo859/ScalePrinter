using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScalePrinter.Client.Interfaces {
    public interface IScaleService : IConnectingDevice {
        double CurrentWeight { get; }
        bool IsConnected { get; }
        event EventHandler WeightChanged;
    }

    public class WeightChangedEventArgs : EventArgs {

        public WeightChangedEventArgs(double weight) {
            Weight = weight;
        }

        public double Weight { get; private set; }
    }
}
