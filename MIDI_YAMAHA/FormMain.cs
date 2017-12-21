using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MIDI_YAMAHA
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmDeviceChange)
            {
                new Thread(new ThreadStart(detectDevice)).Start();
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private const int WmDeviceChange = 0x0219;

        private IntPtr midiIn;
        private Win32Midi.MidiInProc callbackDelegate;
        private IntPtr callbackPtr;

        private void FormMain_Load(object sender, EventArgs e)
        {
            callbackDelegate = new Win32Midi.MidiInProc(callback);
            callbackPtr = (IntPtr)Marshal.GetFunctionPointerForDelegate(callbackDelegate);

            lblMes.Text = "Devices-sum:" + Win32Midi.MidiInGetNumDevs();
            Win32Midi.MidiInCaps midiInCap = new Win32Midi.MidiInCaps();
            Win32Midi.MidiInGetDevCaps(0, ref midiInCap, 0xff);
            lblInCaps.Text = "Mid:0x" + midiInCap.wMid.ToString("X4")
                + "    Pid:0x" + midiInCap.wPid.ToString("X4")
                + "    DriverVersion:0x" + midiInCap.vDriverVersion.ToString("X4")
                + "    Pname:" + midiInCap.szPname.ToString()
                + "    Support:0x" + midiInCap.dwSupport.ToString("X2");

            new Thread(new ThreadStart(detectDevice)).Start();
        }

        private delegate void addLogDelegate(string line);

        private void addLog(string line)
        {
            textBoxDebug.Text += line + Environment.NewLine;
        }

        private Mutex mutexDetectDevice = new Mutex();

        private void detectDevice()
        {
            mutexDetectDevice.WaitOne();
            var devNum = Win32Midi.MidiInGetNumDevs();
            if (devNum != 0)
            {
                var res = Win32Midi.MidiInOpen(ref midiIn, 0, callbackPtr, (IntPtr)0, Win32Midi.CallbackFunction);
                if (res == Win32Midi.MMSysErrNoError)
                {
                    res = Win32Midi.MidiInStart(midiIn);
                }
                if (res != Win32Midi.MMSysErrNoError)
                {
                    var sb = new StringBuilder();
                    sb.Capacity = 1024;
                    Win32Midi.MidiInGetErrorText(res, sb, (uint)sb.Capacity);
                    Invoke(new addLogDelegate(addLog), sb.ToString());
                }
            }
            mutexDetectDevice.ReleaseMutex();
        }

        private void callback(IntPtr hdrvr, uint msg, uint user, uint dw1, uint dw2)
        {
            //var state = (int)dw1 & 0xff;
            //var data1 = (int)dw1 >> 8 & 0xff;
            //var data2 = (int)dw1 >> 16 & 0xff;

            if ( dw1 != 0xF8 && dw1 != 0xFE)
            {
                Invoke(new addLogDelegate(addLog), string.Format("{0}\t0x{1}\t0x{2}\t0x{3}\t0x{4}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),
                    msg.ToString("X8"),
                    user.ToString("X8"),
                    dw1.ToString("X8"),
                    dw2.ToString("X8")));
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Win32Midi.MidiInStop(midiIn);
            Win32Midi.midiInClose(ref midiIn);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxDebug.Text = "";
        }

        private void txtWriteTxt_Click(object sender, EventArgs e)
        {
            if(textBoxDebug.Text != "")
            {
                txtWriteTxt.Enabled = false;
                string strDateTime = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                StreamWriter myFile = File.CreateText(strDateTime + ".txt");
                myFile.Write(textBoxDebug.Text);
                myFile.Close();
                //MessageBox.Show("OK");
                txtWriteTxt.Enabled = true;
            }

        }
    }
}
