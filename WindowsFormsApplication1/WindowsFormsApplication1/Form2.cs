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
    public partial class Form2 : Form
    {
        public Form1 f1;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 tf1)
        {

            InitializeComponent();
            f1 = tf1;
        }




       



        
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        


        private void buttonUser_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(this);
            this.Hide();
            f3.Show();
        }



        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            f1.Show();
            this.Close();
        }

        private void buttonExe_Click(object sender, EventArgs e)
        {
            Form11 f11 = new Form11(this);
            this.Hide();
            f11.Show();
        }

        private void buttonCommunicate_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8(this);
            this.Hide();
            f8.Show();
        }

        private void buttonMouse_Click(object sender, EventArgs e)
        {
            Form12 f12 = new Form12(this);
            this.Hide();
            f12.Show();
        }
    }
}
