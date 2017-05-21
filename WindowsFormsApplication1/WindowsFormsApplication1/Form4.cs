using MySql.Data.MySqlClient;
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
    public partial class Form4 : Form
    {
        public Form3 f3;
        public Form4(){InitializeComponent();}

        public Form4(Form3 tf3)
        {
            InitializeComponent();
            MySqlDataReader dgv = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, "select * from person", null);
            while (dgv.Read())
            {
                String tu = dgv.GetString(dgv.GetOrdinal("user"));
                String tpo = dgv.GetString(dgv.GetOrdinal("position"));
                String tn = dgv.GetString(dgv.GetOrdinal("name"));
                myList1.Add(tu, tn, tpo);
            }
            f3 = tf3;
            dgv.Close();
        }
    

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            f3.Show();
            this.Close();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void myList1_Load(object sender, EventArgs e)
        {
            myList1.HorizontalScroll.Enabled = false;
            myList1.VerticalScroll.Enabled = true;
        }
    }
}
