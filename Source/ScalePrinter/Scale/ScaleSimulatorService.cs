using ScalePrinter.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ScalePrinter.Client.Scale {
    public class ScaleSimulatorService : IScaleService {

        private bool isConnected;
        private double currentWeight;
        Random random;

        public ScaleSimulatorService(Window window) {
            random = new Random();
            window.KeyDown += window_KeyDown;
            isConnected = false;
        }

        void window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            Console.WriteLine("Key event: " + e.Key);
            switch (e.Key) {
                case Key.S:
                    IsConnected = !IsConnected;
                    break;
                case Key.W:
                    CurrentWeight = random.NextDouble() * 100;
                    break;
                default:
                    break;
            }
        }

        public double CurrentWeight {
            get { 
                return currentWeight; 
            }
            private set { 
                currentWeight = value;
                OnWeightChanged(currentWeight);
            }
        }

        public bool IsConnected {
            get { 
                return isConnected;
            }
            private set {
                isConnected = value;
                OnConnectionChanged(new ConnectionChangedEventArgs(isConnected));
            }
        }

        #region Events

        public event EventHandler<ConnectionChangedEventArgs> ConnectionStatusChanged;

        private void OnConnectionChanged(ConnectionChangedEventArgs e) {
            Console.WriteLine("Scale connection status changed to " + (e.IsConnected ? "connected" : "disconnected"));
            if (ConnectionStatusChanged != null) {
                ConnectionStatusChanged(this, e);
            }
        }

        public event EventHandler<WeightChangedEventArgs> WeightChanged;

        private void OnWeightChanged(double weight) {
            Console.WriteLine("Current weight changed to " + weight);
            var eventArgs = new WeightChangedEventArgs(weight);
            if (WeightChanged != null) {
                WeightChanged(this, eventArgs);
            }
        }

        #endregion
    }
}
