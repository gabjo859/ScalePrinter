using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScalePrinter.Client.Scale {
    public interface IScaleService {
        bool ScaleConnected { get; }
        double GetWeight();
    }
}
