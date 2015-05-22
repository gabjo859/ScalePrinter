using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using ScalePrinter.Client.Interfaces;
using ScalePrinter.Client.Scale;

namespace ScalePrinter.Client.ViewModels {
    public class MainViewModel : INotifyPropertyChanged {

        private IScaleService scaleService;

        public MainViewModel(IScaleService scaleService) {
            this.scaleService = scaleService;
            scaleService.ConnectionStatusChanged += scaleService_ConnectionStatusChanged;
            scaleService.WeightChanged += scaleService_WeightChanged;
            IsScaleConnected = scaleService.IsConnected;
            IsPrinterConnected = false;
        }

        void scaleService_WeightChanged(object sender, WeightChangedEventArgs e) {
            CurrentWeight = e.Weight;
        }

        void scaleService_ConnectionStatusChanged(object sender, ConnectionChangedEventArgs e) {
            IsScaleConnected = e.IsConnected;
        }

        #region Properties

        private bool isScaleConnected;
        public bool IsScaleConnected {
            get { return isScaleConnected; }
            set { 
                isScaleConnected = value;
                NotifyPropertyChanged("IsScaleConnected");
            }
        }

        private bool isPrinterConnected;
        public bool IsPrinterConnected {
            get { return isPrinterConnected; }
            set { 
                isPrinterConnected = value;
                NotifyPropertyChanged("IsPrinterConnected");
            }
        }

        private double currentWeight;
        public double CurrentWeight {
            get { return currentWeight; }
            set { 
                currentWeight = value;
                NotifyPropertyChanged("CurrentWeight");
            }
        }


        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName) {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
