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
    public partial class FormMIDI2PC : Form
    {
        public FormMIDI2PC()
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

        private void FormMIDI2PC_Load(object sender, EventArgs e)
        {
            callbackDelegate = new Win32Midi.MidiInProc(callback);
            callbackPtr = (IntPtr)Marshal.GetFunctionPointerForDelegate(callbackDelegate);

            uint uDevicesSum = Win32Midi.MidiInGetNumDevs();
            lblMes.Text = "Devices-sum:" + uDevicesSum.ToString();
            Win32Midi.MidiInCaps getMidiInCap = new Win32Midi.MidiInCaps();
            for (uint i=0;i< uDevicesSum;i++)
            {
                Win32Midi.MidiInGetDevCaps(i, ref getMidiInCap, Win32Midi.MaxPNameLen);
                int Version1 = (int)getMidiInCap.vDriverVersion >> 8 & 0xff;
                int Version2 = (int)getMidiInCap.vDriverVersion & 0xff;
                lblInCaps.Text = i.ToString() +
                    ": Mid:0x" + getMidiInCap.wMid.ToString("X4") +
                    "    Pid:0x" + getMidiInCap.wPid.ToString("X4") +
                    "    DriverVersion: V" + Version1.ToString() + "." +Version2.ToString() +
                    "    Pname:" + getMidiInCap.szPname.ToString() +
                    "    Support:0x" + getMidiInCap.dwSupport.ToString("X2");
                if (i < uDevicesSum - 1)
                {
                    lblInCaps.Text += "\n";
                }
            }

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

        private void callback(IntPtr lphMidiIn, uint wMsg, uint dwInstance, uint dwParam1, uint dwParam2)
        {
            //var state = (int)dw1 & 0xff;
            //var data1 = (int)dw1 >> 8 & 0xff;
            //var data2 = (int)dw1 >> 16 & 0xff;

            if (dwParam1 != 0xF8 && dwParam1 != 0xFE)
            {
                Invoke(new addLogDelegate(addLog), string.Format("{0}\t0x{1}\t0x{2}\t0x{3}\t0x{4}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),
                    wMsg.ToString("X8"),
                    dwInstance.ToString("X8"),
                    dwParam1.ToString("X8"),
                    dwParam2.ToString("X8")));
            }
        }

        private void FormMIDI2PC_FormClosed(object sender, FormClosedEventArgs e)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
