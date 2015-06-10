using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleTest {
    class Program {

        private static IEnumerable<HidDevice> allDevices = HidDevices.Enumerate();

        static void Main(string[] args) {

            if (!allDevices.Any()) {
                Console.WriteLine("Found no HID devices...");
                Exit();
            }

            DisplayDeviceInfo(allDevices);

            HidDevice selectedDevice = null;
            while (true) {
                selectedDevice = GetSelectedDevice();
                Console.WriteLine(String.Format("\nYou selected '{0}'", selectedDevice.Description));
            }
        }

        private static HidDevice GetSelectedDevice() {
            Console.Write("\nSelect a device for further testing, or 'q' to exit: ");
            var input = Console.ReadLine();
            if (input.ToLower() == "q") Environment.Exit(0);
            try {
                int index = int.Parse(input) - 1;
                return allDevices.ElementAt(index);
            } catch (Exception e) {
                Console.WriteLine("\nERROR: Invalid choice: " + input + ". Must a any of the numbers in the list of HID devices. Try again...");
                return GetSelectedDevice();
            }
        }

        private static void DisplayDeviceInfo(IEnumerable<HidDevice> devices) {
            int i = 0;
            foreach (var device in devices) {
                i++;
                Console.WriteLine(String.Format("\n\n{0}. {1}", i, device.Description));
                Console.WriteLine(String.Concat(Enumerable.Range(0, device.Description.Length).Select(x => "=").ToArray()));
                Console.WriteLine("ProductId:" + device.Attributes.ProductId);
                Console.WriteLine("ProductHexId:" + device.Attributes.ProductHexId);
                Console.WriteLine("VendorId:" + device.Attributes.VendorId);
                Console.WriteLine("VendorHexId:" + device.Attributes.VendorHexId);
                Console.WriteLine("Version:" + device.Attributes.Version);
            }
        }

        private static void Exit() {
            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
