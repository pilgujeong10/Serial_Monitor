using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;

namespace SerialMonitor
{
    public partial class Form3 : Form
    {
        private SerialPort sPort;
        List<SensorData> myData = new List<SensorData>();
        string GetText;
        string GetText2;
        string GetText3;
        public Form3(string s1, string s2)
        {
            InitializeComponent();

            this.GetText = s1;
            this.GetText2 = s2;
        }
        private void Form3_FormClosing(object sendler, FormClosingEventArgs e)
        {
            sPort.Close();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            //textBox1.Text = GetText;
            //textBox2.Text = GetText2;
            sPort = new SerialPort();
            //int a = Convert.ToInt32(textBox2.Text);
            //sPort.PortName = textBox1.Text;
            int a = Convert.ToInt32(GetText2);
            sPort.PortName = GetText;
            sPort.BaudRate = a;
            sPort.Open();
            //string s = sPort.ReadLine();
            sPort.DataReceived += SPort_DataReceived;
            //this.BeginInvoke(new Action(delegate { ShowValue(s); }));
        }
        public void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string s;
            try
            {
                s = sPort.ReadLine();
            }
            catch
            {
                MessageBox.Show("   Check Baudrate and Retry Connect        ", "Serial Monitor Console", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CheckForIllegalCrossThreadCalls = true;
            //Console.WriteLine("string {0}", s);
            this.BeginInvoke(new Action(delegate { ShowValue(s, checkBox1.Checked); }));
        }
        public void ShowValue(string s, bool Checked)
        {
            GetText3 = s;
            //textBox3.Text = s;
            //float[] v = { 0, 0, 0, 0, 0, 0 };
            //구분자
            //string[] split_s;
            //split_s = s.Split(',', ' ', '/');
            //for (int i = 0; i < split_s.Length; i++)
            //{
            //    v[i] = float.Parse(split_s[i]);
            //}
            //SensorData data = new SensorData(DateTime.Now.ToShortDateString(), DateTime.Now.ToString("HH:mm:ss"), s);
            //myData.Add(data);
            string item = DateTime.Now.ToString() + "\t" + s;
            listBox1.Items.Add(item);
            if (Checked)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.SelectionMode = SelectionMode.One;
            }
            else
            {
                listBox1.SelectionMode = SelectionMode.MultiExtended;
            }
        }
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                System.Text.StringBuilder copy_buffer = new System.Text.StringBuilder();
                foreach (object item in listBox1.SelectedItems)
                    copy_buffer.AppendLine(item.ToString());
                if (copy_buffer.Length > 0)
                    Clipboard.SetText(copy_buffer.ToString());
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string s;
            s = sPort.ReadLine();
            ShowValue(s, checkBox1.Checked);
        }
    }
}