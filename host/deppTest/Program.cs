using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Inonvis.DigilentAdept
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool result = DeviceAccessManager.DmgrEnumDevices(out int pcdvc);
            //result = DeviceAccessManager.DmgrGetDvc(0, out DeviceAccessManager.Device device);
            bool result;
            StringBuilder sb = new StringBuilder(256);
            result = DeppGetVersion(sb);
            Console.WriteLine(sb.ToString());
            //result = DeviceAccessManager.DmgrOpen(out uint hif, "CmodS6");
            //result = ParallelPortInterface.DeppEnable(hif);
            //result = ParallelPortInterface.DeppGetReg(hif, 5, out byte data, false);

            //result = DeviceAccessManager.DmgrClose(hif);
        }

        [DllImport("deppExport32.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool DeppGetVersion([MarshalAs(UnmanagedType.LPStr)] StringBuilder szVersion);

    }
}
