using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MyItem : UserControl
    {
        public MyItem()
        {
            InitializeComponent();
        }
        public string TextUser
        {
            get { return labelUser.Text; }
            set { labelUser.Text = value; }
        }

        public string TextName
        {
            get { return labelName.Text; }
            set { labelName.Text = value; }
        }

        public string TextPosition
        {
            get { return comboPosition.Text; }
            set { comboPosition.Text = value; }
        }

        private void comboPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            String command = "UPDATE person SET position = '" + comboPosition.Text
                + "' WHERE user = '" + labelUser.Text + "';";

            MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, command , null);
    
        }

    }
}
