using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Inonvis.DigilentAdept
{
    /// <summary>
    /// The DEPP subsystem provides the ability to transfer data between PC applications
    /// software and control registers or memory in certain Digilent Adept compatible
    /// devices. These data transfer API functions allow for writing or reading a single
    /// register, writing or reading sets of registers, or writing or reading a stream
    /// of data into or out of a single register.
    /// Some Digilent Adept compatible devices, such as programmable logic boards,
    /// require that a DEPP compatible logic configuration or device firmware be running
    /// in the target device for the DEPP interface to work.This device logic must
    /// conform to the interface described in the document: Application Note 10, Digilent
    /// Asynchronous Parallel Interface.This document is included in the Adept SDK
    /// documentation and is also available on the Digilent web site.
    /// (www.digilentinc.com)
    /// </summary>
    public static class ParallelPortInterface
    {
        /// <summary>
        /// Max length returned for DLL version string
        /// </summary>
        public const int cchVersionMax = 256;

        #region Basic interface functions

        /// <summary>
        /// This function returns a version number string identifying the version number
        /// of the DEPP DLL. The symbol <see cref="cchVersionMax"/> defines the
        /// longest string that can be returned in szVersion
        /// </summary>
        /// <param name="szVersion">Pointer to buffer to receive version string</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppGetVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetVersion([MarshalAs(UnmanagedType.LPStr)] StringBuilder szVersion);

        /// <summary>
        /// This function returns the number of DEPP ports supported by the device specified by hif.
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <param name="pcprt">pointer to variable to receive count of ports</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppGetPortCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetPortCount(uint hif, ref int pcprt);

        /// <summary>
        /// This function returns the port properties bits for the specified DEPP port.
        /// The port properties bits indicate the specific features of the DEPP
        /// specification implemented by the specified port.
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <param name="prtReq">port number to query</param>
        /// <param name="pdprp">pointer to variable to return port property bits</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppGetPortProperties", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetPortProperties(uint hif, int prtReq, ref uint pdprp);

        /// <summary>
        /// This function is used to enable the default DEPP port (port 0) on the
        /// specified device. This function must be called before any functions
        /// that operate on the DEPP port may be called for the specified device. 
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppEnable", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Enable(uint hif);

        /// <summary>
        /// This function is used to enable a specific port on devices that support
        /// multiple DEPP ports. This function must be called before any functions
        /// that operate on the DEPP port may be called. The prtReq parameter
        /// specifies the port number of the DEPP port to enable. 
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <param name="prtReq">DEPP port number</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppEnableEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool EnableEx(uint hif, int prtReq);

        /// <summary>
        /// This function is used to disable and end access to the currently enabled
        /// DEPP port on the specified interface handle. 
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppDisable", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Disable(uint hif);

        #endregion

        #region Data transfer functions

        /// <summary>
        /// This function writes a single data byte to the register with the specified address.
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <param name="bAddr">address of register to send data byte</param>
        /// <param name="bData">data byte to send to address</param>
        /// <param name="fOverlap">True if operation should be overlapped</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppPutReg", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool PutReg(uint hif, byte bAddr, byte bData, bool fOverlap);

        /// <summary>
        /// This function reads a single data byte from the register with the specified address.
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <param name="bAddr">address of register to read data byte</param>
        /// <param name="pbData">pointer to store data byte read</param>
        /// <param name="fOverlap">True if operation should be overlapped</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppGetReg", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetReg(uint hif, byte bAddr, out byte pbData, bool fOverlap);

        /// <summary>
        /// This function sends multiple data bytes to multiple register addresses.
        /// A buffer containing addresses/data pairs is provided in the pbAddrData
        /// parameter.  An address/data pair consists of 1 register address byte
        /// followed by 1 data byte in the pbAddrData buffer. For example, the
        /// address in pbAddrData [0] is paired with the data bye in pbAddrData [1].
        /// Each data byte will be written to the register at the associated address
        /// during the transaction.  The number of address/data pairs in the pbAddrData
        /// buffer is specified in the nAddrDataPairs parameter. It is permissible to
        /// repeat register addresses in the pdAddrData buffer to cause multiple bytes
        /// to be sent to the same register address.
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <param name="pbAddrData">buffer of register address and data pairs</param>
        /// <param name="nAddrDataPairs">number of register address/data pairs in pbAddrData buffer</param>
        /// <param name="fOverlap">True if operation should be overlapped</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppPutRegSet", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool PutRegSet(uint hif, ref byte pbAddrData, uint nAddrDataPairs, bool fOverlap);

        /// <summary>
        /// This function gets multiple data bytes from multiple register addresses.
        /// A buffer containing specified register addresses is provided along with
        /// a buffer to receive bytes read back from the addresses.  Each element
        /// in the pbData buffer is read from the corresponding address in the
        /// pbAddr buffer.  For example, the data byte in the register specified by
        /// the address in pbAddr [0] is written to pbData [0].  The number of bytes
        /// to be read out is specified in the cbData parameter. It is permissible
        /// to repeat register addresses in the pbAddr buffer to cause multiple
        /// values to be read from the same register address.
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <param name="pbAddr">buffer of register addresses</param>
        /// <param name="pbData">pointer to store data bytes read back from specified register addresses</param>
        /// <param name="cbData">number of data bytes to read back</param>
        /// <param name="fOverlap">True if operation should be overlapped</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppGetRegSet", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetRegSet(uint hif, ref byte pbAddr, ref byte pbData, int cbData, bool fOverlap);


        /// <summary>
        /// This function sends a stream of data bytes to a single, specified register
        /// address.  Data bytes from the buffer in pbData are sent to the address
        /// specified by bAddr.  The data is sent as quickly as the hardware will allow.
        /// The number of bytes to be sent to bAddr is specified in cbData.
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <param name="bAddr">register address to stream data bytes to</param>
        /// <param name="pbData">buffer of data bytes to stream to specified address</param>
        /// <param name="cbData">number of data bytes to stream to specified address</param>
        /// <param name="fOverlap">True if operation should be overlapped</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppPutRegRepeat", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool PutRegRepeat(uint hif, byte bAddr, ref byte pbData, int cbData, bool fOverlap);

        /// <summary>
        /// This function gets a stream of data bytes from a single, specified register
        /// address.  Data bytes are read out of the address specified by bAddr into
        /// the buffer in pbData.  The data is read as quickly as the hardware will
        /// allow.  The number of bytes to be read out of bAddr is specified in cbData.
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <param name="bAddr">register address to stream data bytes from</param>
        /// <param name="pbData">pointer to store data bytes streamed from specified address</param>
        /// <param name="cbData">number of data bytes to stream from specified address</param>
        /// <param name="fOverlap">True if operation should be overlapped</param>
        /// <returns>True when successful</returns>
        [DllImport("depp.dll", EntryPoint = "DeppGetRegRepeat", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetRegRepeat(uint hif, byte bAddr, ref byte pbData, int cbData, bool fOverlap);

        #endregion

        #region Misc. control functions

        /// <summary>
        /// This function is used to set the amount of delay to allow before an EPP
        /// transaction will timeout. This is to prevent a malfunctioning device
        /// from blocking access to the port. The timeout value will be set to the
        /// nearest supported value that doesn’t exceed the value requested in
        /// tnsTimoutTry, or the largest supported value. The actual value set is
        /// returned in ptnsTimeout. 
        /// </summary>
        /// <param name="hif">open interface handle on the device</param>
        /// <param name="tnsTimeoutTry">desired value (in nanoseconds) to be used to determine a timeout</param>
        /// <param name="ptnsTimeout">pointer to return actual actual value set</param>
        /// <returns></returns>
        [DllImport("depp.dll", EntryPoint = "DeppSetTimeout", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetTimeout(uint hif, int tnsTimeoutTry, ref int ptnsTimeout);

        #endregion
    }
}
