using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;

namespace sensorMonitor
{
    public partial class Form2 : Form
    {
        int x,y;

        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string url = "http://127.0.0.1:8082/temp.json?time="+ dateTimePicker1 .Value +"&"+ dateTimePicker2.Value;
            MessageBox.Show(sss.PostHttp(url, "roleId=1&uid=2", "application/x-www-form-urlencoded"));

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // chart1 . Series[1].Points.DataBindXY(x, y);
            //Student two = JsonConvert.DeserializeObject<Student>(sss.PostHttp(url, "roleId=1&uid=2", "application/x-www-form-urlencoded"));
            //Console.WriteLine(
                   //string.Format("学生信息  ID:{0},姓名:{1},年龄:{2},性别:{3}",
                  // ,,, ));//显示结果
            Console.ReadLine();
        }
    }
}
