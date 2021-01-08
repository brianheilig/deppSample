using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Inonvis.DigilentAdept
{
    class Program
    {
        static void Main(string[] args)
        {
            bool result;
            result = DeviceAccessManager.EnumDevices(out int pcdvc);
            result = DeviceAccessManager.GetDevice(0, out DeviceAccessManager.Device device);
            StringBuilder sb = new StringBuilder(1024);
            result = DeviceAccessManager.GetVersion(sb);
            result = ParallelPortInterface.GetVersion(sb);
            Console.WriteLine(sb.ToString());
            result = DeviceAccessManager.Open(out uint hif, "CmodS6");
            result = ParallelPortInterface.Enable(hif);
            result = ParallelPortInterface.GetReg(hif, 5, out byte data, false);

            result = DeviceAccessManager.Close(hif);
        }
    }
}
