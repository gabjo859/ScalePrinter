using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ScalePrinter.Client.Scale {
    public class HidScaleService : IScaleService {

        #region IScaleService Implementation

        public bool ScaleConnected {
            get { return CheckConnectivity(); }
        }

        public double GetWeight() {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods

        private bool CheckConnectivity() {
            // Get the HID class guid
            var guid = HidD_GetHidGuid();

            // Get info about all connected HID devices
            var h = SetupApiInterop.SetupDiGetClassDevs(ref guid, IntPtr.Zero, IntPtr.Zero, (uint)DiGetClassFlags.DIGCF_DEVICEINTERFACE);

            if (h != INVALID_HANDLE_VALUE) {

                var success = true;
                UInt32 i = 1;
                while (success) {

                    // Create the device interface data structure
                    var dia = new SP_DEVICE_INTERFACE_DATA();
                    dia.cbSize = Marshal.SizeOf(dia);

                    // Start the enumeration
                    success = SetupDiEnumDeviceInterfaces(h, IntPtr.Zero, ref guid, i, ref dia);
                    if (success) {
                        var da = new SP_DEVINFO_DATA();
                        da.cbSize = (uint)Marshal.SizeOf(da);

                        // Build a Device Interface Detail Data structure
                        var didd = new NativeDeviceInterfaceDetailData();
                        didd.size = 4 + Marshal.SystemDefaultCharSize;

                        // now we can get some more detailed information
                        uint nRequiredSize = 0;
                        uint nBytes = (uint)BUFFER_SIZE;
                        if (SetupDiGetDeviceInterfaceDetail(h, ref dia, ref didd, nBytes, out nRequiredSize, ref da)) {
                            // current InstanceID is at the "USBSTOR" level, so we
                            // need up "move up" one level to get to the "USB" level
                            UInt32 ptrPrevious;
                            CM_Get_Parent(out ptrPrevious, da.DevInst, 0);

                            // Now we get the InstanceID of the USB level device
                            IntPtr ptrInstanceBuf = Marshal.AllocHGlobal((int)nBytes);
                            CM_Get_Device_ID(ptrPrevious, ptrInstanceBuf, (int)nBytes, 0);
                            string InstanceID = Marshal.PtrToStringAuto(ptrInstanceBuf);

                            Marshal.FreeHGlobal(ptrInstanceBuf);
                        }
                        i++;
                    }
                }
            }
            SetupDiDestroyDeviceInfoList(h);
            return false;
        }

        #endregion

        #region hid.dll imports

        [DllImport("hid.dll", CharSet=CharSet.Auto, SetLastError=true)]
        static extern Guid HidD_GetHidGuid();
        
        #endregion

        #region setupapi.dll imports

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern Boolean SetupDiEnumDeviceInterfaces(
            IntPtr hDevInfo,
            IntPtr devInfo,
            ref Guid interfaceClassGuid,
            UInt32 memberIndex,
            ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern Boolean SetupDiGetDeviceInterfaceDetail(
           IntPtr hDevInfo,
           ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
           ref NativeDeviceInterfaceDetailData deviceInterfaceDetailData,
           UInt32 deviceInterfaceDetailDataSize,
           out UInt32 requiredSize,
           ref SP_DEVINFO_DATA deviceInfoData
        );

        [DllImport("setupapi.dll")]
        static extern int CM_Get_Parent(
           out UInt32 pdnDevInst,
           UInt32 dnDevInst,
           int ulFlags
        );

        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        static extern int CM_Get_Device_ID(
           UInt32 dnDevInst,
           IntPtr Buffer,
           int BufferLen,
           int ulFlags
        );

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        #endregion

        #region Data structs, constants and enums

        private IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        private int BUFFER_SIZE = 512;

        [StructLayout(LayoutKind.Sequential)]
        struct SP_DEVICE_INTERFACE_DATA {
            public Int32 cbSize;
            public Guid interfaceClassGuid;
            public Int32 flags;
            private UIntPtr reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SP_DEVINFO_DATA {
            public uint cbSize;
            public Guid ClassGuid;
            public uint DevInst;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        struct NativeDeviceInterfaceDetailData {
            public int size;
            public char devicePath;
        }

        [Flags]
        enum DeviceInterfaceDataFlags : uint {
            Active = 0x00000001,
            Default = 0x00000002,
            Removed = 0x00000004
        }

        [Flags]
        enum DiGetClassFlags : uint {
            DIGCF_DEFAULT = 0x00000001,  // only valid with DIGCF_DEVICEINTERFACE
            DIGCF_PRESENT = 0x00000002,
            DIGCF_ALLCLASSES = 0x00000004,
            DIGCF_PROFILE = 0x00000008,
            DIGCF_DEVICEINTERFACE = 0x00000010,
        }

        #endregion

    }
}
