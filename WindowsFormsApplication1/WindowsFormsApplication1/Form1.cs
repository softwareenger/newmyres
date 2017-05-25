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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboPosition.Text = "用户";
        }


      
        public void change() 
        {
            
        }

        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("你确定要关闭应用程序吗？", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                this.FormClosing -= new FormClosingEventHandler(this.Form1_FormClosing);
                Application.Exit();
            }
            else
            {
                e.Cancel = true;  
            }
        }        


        private void textPassword_TextChanged(object sender, EventArgs e)
        {
            Person.password = textPassword.Text;
            this.textPassword.PasswordChar = '*';
        }
        int judge(String tu,String tpa,String tpo)
        {
            if (Person.user != tu){return 1;}
            if (Person.password != tpa){return 2;}
            if (Person.position != tpo){ return 3;}
            return 0;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

          


            MySqlDataReader dgv = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, "select * from person", null);
            int ok = 0;
            while (dgv.Read())
            {
                String tu = dgv.GetString(dgv.GetOrdinal("user"));
                String tpa = dgv.GetString(dgv.GetOrdinal("password"));
                String tpo = dgv.GetString(dgv.GetOrdinal("position"));
                String tn = dgv.GetString(dgv.GetOrdinal("name"));
                if ( (ok = judge(tu, tpa, tpo) ) == 0 )
                {
                    Person.name = tn;
                    break;
                }
            }
            dgv.Close();
            if (ok == 0)
            {
                MessageBox.Show("登陆成功");
                Form2 f2 = new Form2(this);
                f2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("登陆失败");
            }
            
        }

        private void textUser_TextChanged(object sender, EventArgs e)
        {
            Person.user = textUser.Text;
        }

        private void comboPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            Person.position = comboPosition.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (textUser.Text != "" && textPassword.Text != "")
            {
                MySqlDataReader dgv = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, "select * from person", null);
                bool ok = true;
                while (dgv.Read())
                {
                    String tu = dgv.GetString(dgv.GetOrdinal("user"));
                    if (Person.user == tu)
                    {
                        ok = false;
                        break;
                    }
                }
                dgv.Close();
                if (ok)
                {
                    MessageBox.Show("注册成功,请填写身份信息\n" + "身份状态待管理员审核。");
                    Form5 f5 = new Form5(this);
                    f5.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("注册失败,该用户已经存在\n");
                }
            }
            else
            {
                MessageBox.Show("注册失败,信息不完善\n");
            }

        }
    }
}
