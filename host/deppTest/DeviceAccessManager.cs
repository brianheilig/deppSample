using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Inonvis.DigilentAdept
{
    /// <summary>
    /// The DMGR subsystem is used to open and close handles for access to
    /// Digilent Adept compatible devices. It is also used to enumerate
    /// Digilent Adept compatible devices connected to or accessible from
    /// the user’s PC and to query and set attributes within the devices.
    /// </summary>
    public static class DeviceAccessManager
    {
        /// <summary>
        /// Max length returned for DLL version string
        /// </summary>
        public const int cchVersionMax = 256;

        /// <summary>
        /// Size of name field in DVC structure
        /// </summary>
        public const int cchDeviceNameMax = 64;

        [StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct Device
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = cchDeviceNameMax)]
            public string szName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 261)]
            public string szConn;

            public int dtp;
        }

        /// <summary>
        /// Maximum length of error code symbolic name
        /// </summary>
        public const int cchErcMax = 48;

        /// <summary>
        /// Maximum length of error message descriptive string
        /// </summary>
        public const int cchErcMsgMax = 128;

        /// <summary>
        /// This function returns a version number string identifying the version number of the DMGR DLL.
        /// The symbol <see cref="cchVersionMax"/> defines the longest string that can be returned
        /// in szVersion
        /// </summary>
        /// <param name="szVersion">pointer to buffer to receive version string</param>
        /// <returns>True when successful</returns>
        [DllImport("deppExport.dll", EntryPoint = "DmgrGetVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetVersion([MarshalAs(UnmanagedType.LPStr)] StringBuilder szVersion);

        /// <summary>
        /// This function returns the error code of the last error to occur in the context of the
        /// calling process and thread. If no error has occurred since the last time this function
        /// was called, the value ercNoError is returned. After the error code has been returned,
        /// the error status for the calling thread is reset to ercNoError. 
        /// </summary>
        /// <returns>The error code of the last error to occur</returns>
        [DllImport("deppExport.dll", EntryPoint = "DmgrGetLastError", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLastError();

        /// <summary>
        /// This function returns strings that can be used for generating error messages.
        /// It will return a string giving a symbolic name for the error code in szErc
        /// and another string describing the meaning of the error code in szErcMessage.
        /// <see cref="cchErcMax"/> and <see cref="cchErcMsgMax"/> give the maximum lengths of the
        /// strings that will be returned. 
        /// </summary>
        /// <param name="erc">error code</param>
        /// <param name="szErc">buffer to receive symbolic name for the error code</param>
        /// <param name="szErcMessage">buffer to receive descriptive string for the error code</param>
        /// <returns>True when successful</returns>
        [DllImport("deppExport.dll", EntryPoint = "DmgrSzFromErc", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool StringFromErrorCode(int erc, [MarshalAs(UnmanagedType.LPStr)] StringBuilder szErc, [MarshalAs(UnmanagedType.LPStr)] StringBuilder szErcMessage);

        #region Open and close functions

        /// <summary>
        /// This function will attempt to open the device specified by the given connection string.
        /// If the connection string is one that requires an enumeration to be performed to discover
        /// the requested device, only device transport types that allow ‘quick discovery’ will be
        /// enumerated. See the description of Enumeration in the Digilent Adept System Programmer’s
        /// Reference Manual. If the function call succeeds, an interface handle is returned in the
        /// variable specified by phif.
        /// </summary>
        /// <param name="phif">pointer to variable to receive interface handle</param>
        /// <param name="szSel">connection string to use to open the device</param>
        /// <returns>True when successful</returns>
        [DllImport("deppExport.dll", EntryPoint = "DmgrOpen", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Open(out uint phif, [MarshalAs(UnmanagedType.LPStr)] string szSel);

        /// <summary>
        /// This function is used to close an interface handle when access to the device is no longer
        /// required. Interface handles are a scarce resource in the system and should be closed as
        /// soon as they are no longer needed. Any enabled port should be disabled before the handle
        /// is closed.
        /// Once this function has returned, the specified interface handle can no longer be used to
        /// access the device.
        /// </summary>
        /// <param name="hif">interface handle to be closed</param>
        /// <returns>True when successful</returns>
        [DllImport("deppExport.dll", EntryPoint = "DmgrClose", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Close(uint hif);

        #endregion

        #region Enumeration functions

        [DllImport("deppExport.dll", EntryPoint = "DmgrEnumDevices", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool EnumDevices(out int pcdvc);

        [DllImport("deppExport.dll", EntryPoint = "DmgrGetDvc", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetDevice(int idvc, out Device device);

        #endregion
    }
}
