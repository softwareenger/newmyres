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
    public partial class Form5 : Form
    {
        public Form1 t1;
        public bool use = false;
        public Form5()
        {
            InitializeComponent();
        }
        public Form5(Form1 temp)
        {

            InitializeComponent();
            t1 = temp;
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            if (use == false)
            {
                MessageBox.Show("因为你没有点击确定,这里直接保存你表上填写的信息");
                Person.name = textName.Text;
                Person.age = Convert.ToInt32(textAge.Text);
                Person.work = textWork.Text;
            }

            String tu = Person.user;
            String tpa = Person.password;
            String tpo = Person.position;
            String tn = Person.name;
            String ta = "不正常";
            String command = "insert into person values( '" + tu + "' , '" + tpa + "' , '" + tpo + "' , '" + tn + "' , '" + ta + "' );";
            MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, command, null);

            int tag = Person.age;
            String tw = Person.work;
            command = "insert into infomation values( '" + tu + "' , " + tag + " ,' " + tn + "' , '" + tw + "' );";

           MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, command, null);
            

            t1.Show();
            this.Close();
        }

        private void buttonSure_Click(object sender, EventArgs e)
        {
            Person.name = textName.Text;
            Person.age = Convert.ToInt32(textAge.Text);
            Person.work = textWork.Text;
            use = true;
        }
    }
}
