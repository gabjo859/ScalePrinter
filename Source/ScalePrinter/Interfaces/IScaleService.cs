using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScalePrinter.Client.Interfaces {
    public interface IScaleService : IConnectingDevice {
        public double CurrentWeight { get; }
        public bool IsConnected { get; }
    }
}
