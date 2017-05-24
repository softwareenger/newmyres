using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form8 : Form
    {
        public Form2 f2;
        public Form8()
        {
            InitializeComponent();
        }
        public Form8(Form2 tf2)
        {
            InitializeComponent();
            f2 = tf2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7(this);
            MessageBox.Show("生成了服务端");
            f7.Show();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form9 f9 = new Form9(this);
            MessageBox.Show("生成了客户端");
            f9.Show();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            f2.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }
    }
}
