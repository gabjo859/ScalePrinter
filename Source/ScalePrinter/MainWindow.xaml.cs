using ScalePrinter.Client.Scale;
using ScalePrinter.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScalePrinter.Client {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            var scaleService = new ScaleSimulatorService(this);
            this.DataContext = new MainViewModel(scaleService);
            this.KeyDown += MainWindow_KeyDown;
            InitializeComponent();
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.P) {
                var viewModel = (MainViewModel)this.DataContext;
                viewModel.IsPrinterConnected = !viewModel.IsPrinterConnected;
                Console.WriteLine("Printer is " + (viewModel.IsPrinterConnected ? "connected" : "disconnected"));
            }
        }
    }
}
