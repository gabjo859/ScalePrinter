using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleTest {
    class Program {
        static void Main(string[] args) {

            var allDevices = HidDevices.Enumerate();

            if (allDevices.Any()) {
                foreach (var device in allDevices) {
                    Console.WriteLine("\n\nDescription: " + device.Description);
                    Console.WriteLine(String.Concat(Enumerable.Range(0, device.Description.Length).Select(x => "=").ToArray()));
                    Console.WriteLine("ProductId:" + device.Attributes.ProductId);
                    Console.WriteLine("ProductHexId:" + device.Attributes.ProductHexId);
                    Console.WriteLine("VendorId:" + device.Attributes.VendorId);
                    Console.WriteLine("VendorHexId:" + device.Attributes.VendorHexId);
                    Console.WriteLine("Version:" + device.Attributes.Version);
                    Console.WriteLine("Path: " + device.DevicePath);
                }
            } else {
                Console.WriteLine("Found no HID devices..:");
            }

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
