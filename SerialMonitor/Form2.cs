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
    public partial class Form2 : Form
    {
        private SerialPort sPort;
        private int xCount = 300;
        List<SensorData> myData = new List<SensorData>();
        string GetText;
        string GetText2;
        string GetText3;

        public Form2(string s1, string s2)
        {
            InitializeComponent();

            this.GetText = s1;
            this.GetText2 = s2;
        }
        private void Form2_FormClosing(object sendler, FormClosingEventArgs e)
        {
            sPort.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("draw");

            chart1.ChartAreas["draw"].AxisX.Minimum = 0;
            chart1.ChartAreas["draw"].AxisX.Maximum = xCount;
            chart1.ChartAreas["draw"].AxisX.Interval = xCount / 5;
            chart1.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.Black;
            chart1.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas["draw"].AxisY.Minimum = 0;
            chart1.ChartAreas["draw"].AxisY.Maximum = 1024;
            chart1.ChartAreas["draw"].AxisY.Interval = 200;
            chart1.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.Black;
            chart1.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas["draw"].BackColor = Color.White;
            chart1.ChartAreas["draw"].CursorX.AutoScroll = true;

            chart1.ChartAreas["draw"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["draw"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            chart1.ChartAreas["draw"].AxisX.ScrollBar.ButtonColor = Color.LightBlue;

            chart1.Series.Clear();
            chart1.Series.Add("PhotoCell0");
            chart1.Series["PhotoCell0"].ChartType = SeriesChartType.Line;
            chart1.Series["PhotoCell0"].Color = Color.Blue;
            chart1.Series["PhotoCell0"].BorderWidth = 1;

            chart1.Series.Add("PhotoCell1");
            chart1.Series["PhotoCell1"].ChartType = SeriesChartType.Line;
            chart1.Series["PhotoCell1"].Color = Color.Red;
            chart1.Series["PhotoCell1"].BorderWidth = 1;

            chart1.Series.Add("PhotoCell2");
            chart1.Series["PhotoCell2"].ChartType = SeriesChartType.Line;
            chart1.Series["PhotoCell2"].Color = Color.Green;
            chart1.Series["PhotoCell2"].BorderWidth = 1;

            chart1.Series.Add("PhotoCell3");
            chart1.Series["PhotoCell3"].ChartType = SeriesChartType.Line;
            chart1.Series["PhotoCell3"].Color = Color.Purple;
            chart1.Series["PhotoCell3"].BorderWidth = 1;

            chart1.Series.Add("PhotoCell4");
            chart1.Series["PhotoCell4"].ChartType = SeriesChartType.Line;
            chart1.Series["PhotoCell4"].Color = Color.HotPink;
            chart1.Series["PhotoCell4"].BorderWidth = 1;
            if (chart1.Legends.Count > 0)
                chart1.Legends.RemoveAt(0);

            //textBox1.Text = GetText;
            //textBox2.Text = GetText;
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
            //           Console.WriteLine("string {0}", s);
            this.BeginInvoke(new Action(delegate { ShowValue(s); }));
        }


        public void ShowValue(string s)
        {

            //textBox3.Text = s;
            GetText3 = s;
            
            float[] v = { 0, 0, 0, 0, 0, 0 };
            //구분자
            string[] split_s;
            split_s = s.Split(',', ' ', '/');
            for (int i = 0; i < split_s.Length; i++)
            {
                try
                {
                    v[i] = float.Parse(split_s[i]);
                    Console.WriteLine(v[i]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert {0}", s);
                }
            }
            SensorData data = new SensorData(DateTime.Now.ToShortDateString(), DateTime.Now.ToString("HH:mm:ss"), s);
            myData.Add(data);

            chart1.Series["PhotoCell0"].Points.Add(v[0]);
            chart1.Series["PhotoCell1"].Points.Add(v[1]);
            chart1.Series["PhotoCell2"].Points.Add(v[2]);
            chart1.Series["PhotoCell3"].Points.Add(v[3]);
            chart1.Series["PhotoCell4"].Points.Add(v[4]);

            chart1.ChartAreas["draw"].AxisX.Minimum = 0;
            chart1.ChartAreas["draw"].AxisX.Maximum =
                (myData.Count >= 300) ? myData.Count : xCount;
            if (myData.Count > xCount)
                chart1.ChartAreas["draw"].AxisX.ScaleView.Zoom(myData.Count - xCount, myData.Count);
            else
                chart1.ChartAreas["draw"].AxisX.ScaleView.Zoom(0, xCount);
        }
    }
}

