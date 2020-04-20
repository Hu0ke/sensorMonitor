using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace sensorMonitor
{
    public partial class Form1 : Form
    {
        public static System.IO.Ports.SerialPort port;
        private Thread readThread = null;
        private MqttClientInterface client = null;
        public Form1()
        {
            InitializeComponent();
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.Add("2400");
            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("14400");
            comboBox2.Items.Add("19200");
            comboBox2.Items.Add("28800");
            comboBox2.Items.Add("38400");
            comboBox2.Items.Add("57600");
            comboBox2.Items.Add("115200");
            comboBox2.SelectedIndex = 0;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            port = new System.IO.Ports.SerialPort();
            port.PortName = comboBox1.SelectedItem.ToString();
            port.BaudRate = Int32.Parse(comboBox2.SelectedItem.ToString());
            port.DtrEnable = true;
            port.ReadTimeout = 5000;
            port.WriteTimeout = 500;
            port.Open();

            readThread = new Thread(new ThreadStart(this.Read));
            readThread.Start();
        }

        public void Read()
        {
            while (port.IsOpen)
            {
                try
                {
                    string message = port.ReadLine();
                    this.Invoke((EventHandler)delegate
                    {
                        textBox2.Text += message;
                        textBox2.Text += "\n";
                    });
                    if (client != null && client.IsConnected())
                    {
                        client.publish("CYT/kkk", message);
                    }
                }
                catch (TimeoutException)
                {

                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            readThread.Abort();
            port.Close();
        }
    }
}
