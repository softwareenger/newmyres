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
    public partial class Form3 : Form
    {
        public Form2 f2;
        public Form3()
        {
            InitializeComponent();
        }


        public Form3(Form2 tf2)
        {
            InitializeComponent();
            f2 = tf2;
        }


  
        private void buttonCheckUser_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(this);
            f4.Show();
            this.Hide();

        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6(this);
            f6.Show();
            this.Hide();
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