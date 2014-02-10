using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Text;
using ScalePrinter.Client.Scale;

namespace IntegrationTests {
    [TestClass]
    public class ScaleServiceTests {
        [TestMethod]
        public void ConnectionTest() {
            var service = new ScaleService();
            var connected = service.ScaleConnected;
        }

        [TestMethod]
        public void BitWiseOperatorTest() {
            
        }
    }
}
