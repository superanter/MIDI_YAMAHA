using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MIDI_YAMAHA
{
    class Win32Midi
    {
        /// <summary>
        /// 检索系统内MIDI输入设备的数量。
        /// UINT midiInGetNumDevs(void);
        /// </summary>
        /// <returns>
        /// 返回系统内MIDI输入设备的数量。 返回值为0表示系统内没有设备。（无错误返回值）
        /// </returns>
        [DllImport("winmm.dll", EntryPoint = "midiInGetNumDevs")]
        public static extern uint MidiInGetNumDevs();

        /// <summary>
        /// 确定指定的MIDI输入设备的功能。
        /// MMRESULT midiInGetDevCaps(
        ///     UINTgongnenn uDeviceID,
        ///     LPMIDIINCAPS lpMidiInCaps,
        ///     UINT cbMidiInCaps
        /// );
        /// </summary>
        /// <param name="uDeviceID">MIDI输入设备的标识符。设备标识符从0到当前设备数-1变化。这个参数也可以是一个正确的的的设备句柄。</param>
        /// <param name="lpMidiInCaps">一个MIDIINCAPS结构指针，指向设备容量的信息。</param>
        /// <param name="cbMidiInCaps"> MIDIINCAPS结构的字节数容量。复制最多cbMidiInCaps字节的信息到lpMidiInCaps所指向的位置。如果cbMidiInCaps是0，则不复制，并且函数返回MMSYSERR_NOERROR。</param>
        /// <returns>
        /// 如果正确则返回 MMSYSERR_NOERROR。如果出现错误，返回以下错误值：
        /// MMSYSERR_BADDEVICEID: 指定的设备标识符超出范围。
        /// MMSYSERR_INVALPARAM: 指定的指针或结构无效。
        /// MMSYSERR_NODRIVER: 驱动没有安装。
        /// MMSYSERR_NOMEM: 系统无法分配或锁定内存。
        /// </returns>
        [DllImport("winmm.dll", EntryPoint = "midiInGetDevCaps")]
        public static extern uint MidiInGetDevCaps(uint uDeviceID, ref MidiInCaps lpMidiInCaps, uint cbMidiInCaps);

        /// <summary>
        /// typedef struct {
        ///     WORD      wMid;
        ///     WORD      wPid;
        ///     MMVERSION vDriverVersion;
        ///     TCHAR     szPname[MAXPNAMELEN];
        ///     DWORD     dwSupport;
        /// } MIDIINCAPS;
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MidiInCaps
        {
            /// <summary>
            /// MIDI输入设备设备驱动程序的制造商标识符。制造商标识符是在制造商和产品标识符中定义的。
            /// </summary>
            public UInt16 wMid;

            /// <summary>
            /// MIDI输入设备的产品标识符。产品标识符是在制造商和产品标识符中定义的。
            /// </summary>
            public UInt16 wPid;

            /// <summary>
            /// MIDI输入设备设备驱动程序的版本号。高字节是主要版本号，低阶字节是次要版本号。
            /// </summary>
            public uint vDriverVersion;

            /// <summary>
            /// 在空终止字符串中的产品名称。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxPNameLen)]
            public string szPname;

            /// <summary>
            ///保留：必须为0。
            /// </summary>
            public uint dwSupport;
        }

        /// <summary>
        /// #define MAXPNAMELEN      32     /* 最大产品名称长度 (包括 NULL) */
        /// </summary>
        public const int MaxPNameLen = 32;

        /// <summary>
        /// 打开一个指定的MIDI输入设备。
        /// MMRESULT midiInOpen(
        ///     LPHMIDIIN lphMidiIn,
        ///     UINT uDeviceID,
        ///     DWORD dwCallback,
        ///     DWORD dwCallbackInstance,
        ///     DWORD dwFlags
        /// );
        /// </summary>
        /// <param name="lphMidiIn">一个HMIDIIN句柄指针。这个位置充满了识别打开的MIDI输入设备的句柄。该句柄用于在调用其他MIDI输入函数时识别设备。</param>
        /// <param name="uDeviceID">要打开的MIDI输入设备的标识符。</param>
        /// <param name="dwCallback">指向回调函数、线程标识符或窗口句柄的指针，该窗口调用有关传入MIDI消息的信息。在回调函数的更多信息，见MidiInProc.</param>
        /// <param name="dwCallbackInstance">传递给回调函数的用户实例数据。此参数不用于窗口回调函数或线程。</param>
        /// <param name="dwFlags">
        /// 用于打开设备的回调标志和可选的状态标志，有助于调节快速数据传输。它可以是以下值:
        /// CALLBACK_FUNCTION: dwCallback参数是一个回调函数的地址。
        /// CALLBACK_NULL: 没有回调机制。此值是默认设置。
        /// CALLBACK_THREAD: dwCallback参数是一个线程标识符。
        /// CALLBACK_WINDOW: dwCallback参数是窗口的句柄。
        /// MIDI_IO_STATUS:  When this parameter also specifies CALLBACK_FUNCTION, MIM_MOREDATA messages are sent to the callback function as well as MIM_DATA messages. Or, if this parameter also specifies CALLBACK_WINDOW, MM_MIM_MOREDATA messages are sent to the window as well as MM_MIM_DATA messages. 这个标志不影响事件或线程的回调函数。
        /// 大多数应用程序使用回调机制将使用此参数指定CALLBACK_FUNCTION。
        /// </param>
        /// <returns>
        /// 如果成功则返回 MMSYSERR_NOERROR。如果出现错误，返回以下错误值：
        /// MMSYSERR_ALLOCATED: 指定的资源已经分配。
        /// MMSYSERR_BADDEVICEID: 指定的设备标识符超出范围。
        /// MMSYSERR_INVALFLAG:	指定的标志 bydwFlagsare 无效。
        /// MMSYSERR_INVALPARAM: 指定的指针或结构无效。
        /// MMSYSERR_NOMEM: 系统无法分配或锁定内存。
        /// </returns>
        [DllImport("winmm.dll", EntryPoint = "midiInOpen")]
        public static extern uint MidiInOpen(ref IntPtr lphMidiIn, uint uDeviceID, IntPtr dwCallback, IntPtr dwCallbackInstance, uint dwFlags);

        //public delegate void MidiInOpenCallback(IntPtr hdrvr, uint msg, uint user, uint dw1, uint dw2);

        /// <summary>
        /// #define CALLBACK_FUNCTION   0x00030000l    /* dwCallback is a FARPROC */
        /// </summary>
        public const uint CallbackFunction = 0x00030001;

        /// <summary>
        /// 关闭一个指定的MIDI输入设备。
        /// MMRESULT midiInClose(
        ///   HMIDIIN hMidiIn 
        /// );
        /// </summary>
        /// <param name="hMidiIn">MIDI输入设备的句柄。如果函数成功，在调用此函数后句柄不再有效。</param>
        /// <returns>
        /// 如果成功则返回 MMSYSERR_NOERROR。如果出现错误，返回以下错误值：
        /// MIDIERR_STILLPLAYING: 缓冲区仍在队列中。
        /// MMSYSERR_INVALHANDLE: 指定的设备句柄无效。
        /// MMSYSERR_NOMEM: 系统无法分配或锁定内存。
        /// </returns>
        [DllImport("winmm.dll", EntryPoint = "midiInClose")]
        public static extern uint midiInClose(ref IntPtr hMidiIn);

        public const uint MMSysErrNoError = 0;

        /// <summary>
        /// 检索指定错误代码识别的错误的文本说明。
        /// MMRESULT midiInGetErrorText(
        ///   MMRESULT wError,
        ///   LPSTR lpText,
        ///   UINT cchText
        /// );
        /// </summary>
        /// <param name="wError">错误代码。</param>
        /// <param name="lpText">指向要填充文本错误描述的缓冲区的指针。</param>
        /// <param name="cchText">长度、字符的缓冲区指向lpText。</param>
        /// <returns>
        /// 如果成功则返回 MMSYSERR_NOERROR。如果出现错误，返回以下错误值：
        /// MMSYSERR_BADERRNUM: 指定的错误号超出范围。
        /// MMSYSERR_INVALPARAM: 指定的指针或结构无效。
        /// MMSYSERR_NOMEM: 系统无法分配或锁定内存。
        /// </returns>
        [DllImport("winmm.dll", EntryPoint = "midiInGetErrorText")]
        public static extern uint MidiInGetErrorText(uint wError, StringBuilder lpText, uint cchText);

        /// <summary>
        /// 在指定的MIDI输入设备上启动MIDI输入。
        /// MMRESULT midiInStart(
        ///   HMIDIIN hMidiIn 
        /// );
        /// </summary>
        /// <param name="hMidiIn">MIDI输入设备的句柄。</param>
        /// <returns>
        /// 如果成功则返回 MMSYSERR_NOERROR。如果出现错误，返回以下错误值：
        /// MMSYSERR_INVALHANDLE: 指定的设备句柄无效。
        /// </returns>
        [DllImport("winmm.dll", EntryPoint = "midiInStart")]
        public static extern uint MidiInStart(IntPtr hMidiIn);

        /// <summary>
        /// 在指定的MIDI输入设备上停止MIDI输入。
        /// MMRESULT midiInStop(
        ///   HMIDIIN hMidiIn 
        /// );
        /// </summary>
        /// <param name="hMidiIn">MIDI输入设备的句柄。</param>
        /// <returns>
        /// 如果成功则返回 MMSYSERR_NOERROR。如果出现错误，返回以下错误值：
        /// MMSYSERR_INVALHANDLE: 指定的设备句柄无效。
        /// </returns>
        [DllImport("winmm.dll", EntryPoint = "midiInStop")]
        public static extern uint MidiInStop(IntPtr hMidiIn);

        /// <summary>
        /// 处理传入的MIDI消息的回调函数。MidiInProc是一个提供的函数名称的应用占位符。这个函数的地址可以在midiInOpen函数回调地址参数指定。
        /// 用来获取消息的回调函数指针，原型如下:
        /// void CALLBACK MidiInProc(
        ///     HMIDIIN hMidiIn,
        ///     UINT wMsg,
        ///     DWORD_PTR dwInstance,
        ///     DWORD_PTR dwParam1,
        ///     DWORD_PTR dwParam2
        /// );
        /// </summary>
        /// <param name="lphMidiIn">设备handle。MIDI输入设备的句柄。</param>
        /// <param name="wMsg">传来的消息号。输入消息。</param>
        /// <param name="dwInstance">用户参数指针，如果没传入一般为null。实例数据提供与midiInOpen功能。</param>
        /// <param name="dwParam1">消息参数1</param>
        /// <param name="dwParam2">消息参数2</param>
        public delegate void MidiInProc(IntPtr lphMidiIn, uint wMsg, uint dwInstance, uint dwParam1, uint dwParam2);

    }
}
