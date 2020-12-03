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
    public partial class Form1 : Form
    {

        private SerialPort sPort;
//        private int xCount = 500;
        List<SensorData> myData = new List<SensorData>();
        List<PortData> portdata = new List<PortData>();

        //DB연결
        //String connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KINETIX_SVR\source\repos\SerialMonitor\SerialMonitor\SensorData.mdf;Integrated Security = True";
        public Form1()
        {
            InitializeComponent();
//            mLastState = this.WindowState;
            //Combo Box
            foreach(var port in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(port);
            }
            comboBox1.Text = "Select Port";
            string[] BaudrateArray = { "300","1200", "2400", "4800", "9600",  "19200", "38400", "57600", "115200","230400","250000","500000","1000000", "2000000" };
            comboBox2.DataSource = BaudrateArray;
            comboBox2.SelectedIndex = 4;  //초기화 9600
//            ChartSetting();
//            lblConnectTime.Text = "Connection Time : ";
//            txtCount.TextAlign = HorizontalAlignment.Center;

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }
//          private FormWindowState mLastState;
//          protected override void OnClientSizeChanged(EventArgs e)
//          {
//            if(this.WindowState != mLastState)
//            {
//                mLastState = this.WindowState;
//                OnWindowStateChanged(e);
//            }
//            else
//            {
//                Console.WriteLine("Detect Resized: {0}, {1}", Size.Width, Size.Height);
//            }
//            base.OnClientSizeChanged(e);
//        }
//        private void OnWindowStateChanged(EventArgs e)
//        {
//            Console.WriteLine("Window State: {0}", WindowState);
//        }

  /*
        private void ChartSetting()
        {
            //btnValue.ForeColor = Color.Black;
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("draw");

            chart1.ChartAreas["draw"].AxisX.Minimum = 0;
            chart1.ChartAreas["draw"].AxisX.Maximum = xCount;
            chart1.ChartAreas["draw"].AxisX.Interval = xCount/5;
            chart1.ChartAreas["draw"].AxisX.MajorGrid.LineColor=Color.Black;
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
            chart1.Series["PhotoCell0"].BorderWidth = 2;
            
            chart1.Series.Add("PhotoCell1");
            chart1.Series["PhotoCell1"].ChartType = SeriesChartType.Line;
            chart1.Series["PhotoCell1"].Color = Color.Red;
            chart1.Series["PhotoCell1"].BorderWidth = 2;
            
            chart1.Series.Add("PhotoCell2");
            chart1.Series["PhotoCell2"].ChartType = SeriesChartType.Line;
            chart1.Series["PhotoCell2"].Color = Color.Green;
            chart1.Series["PhotoCell2"].BorderWidth = 2;
            
            chart1.Series.Add("PhotoCell3");
            chart1.Series["PhotoCell3"].ChartType = SeriesChartType.Line;
            chart1.Series["PhotoCell3"].Color = Color.Purple;
            chart1.Series["PhotoCell3"].BorderWidth = 2;

            chart1.Series.Add("PhotoCell4");
            chart1.Series["PhotoCell4"].ChartType = SeriesChartType.Line;
            chart1.Series["PhotoCell4"].Color = Color.HotPink;
            chart1.Series["PhotoCell4"].BorderWidth = 2;
            if (chart1.Legends.Count > 0)
                chart1.Legends.RemoveAt(0);
        }
*/


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            sPort = new SerialPort(cb.SelectedItem.ToString());
            sPort.BaudRate = Convert.ToInt32(comboBox2.Text);
            btnValue.Text = sPort.PortName;
            btnValue2.Text = comboBox2.Text;
            
//            try
//            {
//                sPort.Open();
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("Check Port and Baudrate");
//                MessageBox.Show("   Check Port and Baudrate         ","Serial Monitor Console",MessageBoxButtons.OK,MessageBoxIcon.Error);
//                return;
//            }
//            sPort.DataReceived += SPort_DataReceived;
//            btnDisconnect.Enabled = true;        
//            lblConnectTime.Text = "Connection Time : " + DateTime.Now.ToString();
        }

//        public void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
//        {
//            string s;
//            try
//            {
//                s = sPort.ReadLine();
//            }
//            catch
//            {
//                MessageBox.Show("   Check Baudrate and Retry Connect        ", "Serial Monitor Console", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }
//                       Console.WriteLine("string {0}", s);
//            this.BeginInvoke(new Action(delegate { ShowValue(s); }));
//        }
//        public void ShowValue(string s)
//        {
//            float[] v= { 0,0,0,0,0,0 };
            //구분자
//            string[] split_s;
//            split_s = s.Split(',',' ','/');
//            for (int i= 0; i< split_s.Length; i++){
//                try
//                {
//                    v[i] = float.Parse(split_s[i]);
//                    Console.WriteLine(v[i]);
//                }
//                catch (FormatException)
//                {
//                    Console.WriteLine("Unable to convert {0}",s);
//                }
//            }

//            try
//            {
//                v = float.Parse(s);
//            }
//            catch(FormatException)
//            {
//               Console.WriteLine("Unalble to conver {0}", s);
//               return;
//            }
//            if (v < 0 || v > 1023)
//                return;

//            SensorData data = new SensorData(DateTime.Now.ToShortDateString(), DateTime.Now.ToString("HH:mm:ss"), s);          
//            myData.Add(data);
            //DBInsert(data);
            //txtCount.Text = myData.Count.ToString();

//            string item = DateTime.Now.ToString() + "\t" + s;
//            listBox1.Items.Add(item);
//            listBox1.SelectedIndex = listBox1.Items.Count - 1;

//            chart1.Series["PhotoCell0"].Points.Add(v[0]);
//            chart1.Series["PhotoCell1"].Points.Add(v[1]);
//            chart1.Series["PhotoCell2"].Points.Add(v[2]);
//            chart1.Series["PhotoCell3"].Points.Add(v[3]);
//            chart1.Series["PhotoCell4"].Points.Add(v[4]);

//            chart1.ChartAreas["draw"].AxisX.Minimum = 0;
//            chart1.ChartAreas["draw"].AxisX.Maximum = 
//                (myData.Count>=100) ? myData.Count : xCount;
//            if (myData.Count > xCount)
//                chart1.ChartAreas["draw"].AxisX.ScaleView.Zoom(myData.Count - xCount, myData.Count);
//            else
//                chart1.ChartAreas["draw"].AxisX.ScaleView.Zoom(0, xCount);          
//                "\n" + s;
//        }

//        private void DBInsert(SensorData data)
//        {
//         }
//        private void btnDisconnect_Click(object sender, EventArgs e)
//        {
//            sPort.Close();
//            btnConnect.Enabled = true;
//            btnDisconnect.Enabled = false;
//        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
//            try
//            {
//                sPort.Open();
//            }
//            catch
//            {
//                MessageBox.Show("   Check Port and Retry Connect        ", "Serial Monitor Console", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }
            btnDisconnect.Enabled = true;
            btnConnect.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(btnValue.Text, btnValue2.Text);
            form2.Owner = this;
            form2.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(btnValue.Text, btnValue2.Text);
            form3.Owner = this;
            form3.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
