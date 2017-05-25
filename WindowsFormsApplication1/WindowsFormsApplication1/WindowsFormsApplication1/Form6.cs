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
    public partial class Form6 : Form
    {
        public Form3 f3;
        public Form6()
        {
            InitializeComponent();
        }

        public Form6(Form3 tf3)
        {
            InitializeComponent();
            f3 = tf3;
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            
            this.listView1.Columns.Add("用户", 50, HorizontalAlignment.Left); //一步添加 
            this.listView1.Columns.Add("姓名", 50, HorizontalAlignment.Left); //一步添加 
            this.listView1.Columns.Add("工作职位", 80, HorizontalAlignment.Left); //一步添加 
            this.listView1.Columns.Add("状态", 50, HorizontalAlignment.Left); //一步添加 

            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 

            MySqlDataReader dgv = MySqlHelper.ExecuteReader(MySqlHelper.Conn, CommandType.Text, "select * from infomation", null);
            int tot = 0;
            while (dgv.Read())
            {
                String tu = dgv.GetString(dgv.GetOrdinal("user"));
                String tn = dgv.GetString(dgv.GetOrdinal("name"));
                String tw = dgv.GetString(dgv.GetOrdinal("work"));
                String ts = dgv.GetString(dgv.GetOrdinal("status"));
                ListViewItem temp = new ListViewItem();

                temp.Text = tu;
                temp.SubItems.Add(tn);
                temp.SubItems.Add(tw);
                temp.SubItems.Add(ts);

                this.listView1.Items.Add(temp);

                tot++;
            }

            dgv.Close();

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            f3.Show();
            this.Close();
        }

        private void buttonSure_Click(object sender, EventArgs e)
        {
            listView1.FocusedItem.SubItems[3].Text = "正常";
            String command = "UPDATE infomation  SET status = '正常' "
              + " WHERE user = '" + listView1.FocusedItem.SubItems[0].Text + "';";
            MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, command, null); 
        }

        private void buttonUnsure_Click(object sender, EventArgs e)
        {
            listView1.FocusedItem.SubItems[3].Text = "不正常";
            String command = "UPDATE infomation  SET status = '不正常' "
              + " WHERE user = '" + listView1.FocusedItem.SubItems[0].Text + "';";
            MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, command, null);
        }

   
    }
}
