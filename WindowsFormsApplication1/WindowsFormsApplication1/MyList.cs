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
    public partial class MyList : UserControl
    {
        public MyList()
        {
            InitializeComponent();
            this.HorizontalScroll.Enabled = false;//横的
            this.VerticalScroll.Enabled = true;//竖的
        }


        private int _itemHeight = 72;
        public int ItemHeight
        {
            get { return _itemHeight; }
            set
            {
                _itemHeight = value;
                refresh();
            }
        }


        private void refresh()
        {
            // this.Height = _itemHeight;
            for (int i = 0; i < this.Controls.Count; i++)
            {
                this.Controls[i].Height = _itemHeight;
                this.Controls[i].Top = i * _itemHeight;
            //    this.Height = _itemHeight * (i + 1);
            }
        }

        public void Add(string textuser, string textname, string textposition)
        {
            var item = new MyItem
            {
                TextUser = textuser,
                TextName = textname,
                TextPosition = textposition
            };

            item.Top = this.Controls.Count * _itemHeight;
            item.Left = 0;
            item.Width = this.Width;
            item.Height = _itemHeight;

            this.Controls.Add(item);
        //    this.Height = this.Controls.Count * _itemHeight;
        }

        public void Remove(int index)
        {
            if (this.Controls.Count > index)
            {
                this.Controls.RemoveAt(index);
            }
            refresh();
        }

        public void RemoveAll()
        {
            this.Controls.Clear();
            refresh();
        }

        public int Count
        {
            get { return this.Controls.Count; }
        }

        private void MyList_Load(object sender, EventArgs e)
        {

        }
    }
}
