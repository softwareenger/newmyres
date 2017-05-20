using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//本段代码中需要新增加的命名空间
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form12 : Form
    {
        Form2 f2;
        public Form12()
        {
            InitializeComponent();
        }
        public Form12(Form2 tf2)
        {
            InitializeComponent();
            f2 = tf2;
        }
        //结构体布局 本机位置
        [StructLayout(LayoutKind.Sequential)]
        struct NativeRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        //将枚举作为位域处理
        [Flags]
        enum MouseEventFlag : uint //设置鼠标动作的键值
        {
            Move = 0x0001,               //发生移动
            LeftDown = 0x0002,           //鼠标按下左键
            LeftUp = 0x0004,             //鼠标松开左键
            RightDown = 0x0008,          //鼠标按下右键
            RightUp = 0x0010,            //鼠标松开右键
            MiddleDown = 0x0020,         //鼠标按下中键
            MiddleUp = 0x0040,           //鼠标松开中键
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,              //鼠标轮被移动
            VirtualDesk = 0x4000,        //虚拟桌面
            Absolute = 0x8000
        }

        //设置鼠标位置
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        //设置鼠标按键和动作
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy,
            uint data, UIntPtr extraInfo); //UIntPtr指针多句柄类型

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string strClass, string strWindow);

        //该函数获取一个窗口句柄,该窗口雷鸣和窗口名与给定字符串匹配 hwnParent=Null从桌面窗口查找
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
            string strClass, string strWindow);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(HandleRef hwnd, out NativeRECT rect);


      
        private UdpClient udpcRecv;

       

        String IP = "192.168.191.1";

        double v = 30;
        double dx = 1.0, dy = 1.0;

        bool IsUdpcRecvStart = false;
        /// <summary>
        /// 线程：不断监听UDP报文
        /// </summary>
        Thread thrRecv;

        /// <summary>
        /// 按钮：接收数据开关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecv_Click(object sender, EventArgs e)
        {
            if (!IsUdpcRecvStart) // 未监听的情况，开始监听
            {
                IPEndPoint localIpep = new IPEndPoint(
                    IPAddress.Parse(IP), 9876); // 本机IP和监听端口号


                udpcRecv = new UdpClient(localIpep);

                thrRecv = new Thread(ReceiveMessage);
                thrRecv.Start();

                IsUdpcRecvStart = true;
                txtRecvMssg.Text += "UDP监听器已成功启动\n";
             
            }
            else                  // 正在监听的情况，终止监听
            {
                thrRecv.Abort(); // 必须先关闭这个线程，否则会异常
                udpcRecv.Close();

                IsUdpcRecvStart = false;
                txtRecvMssg.Text += "UDP监听器已成功关闭\n";
              
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="obj"></param>
        private void ReceiveMessage(object obj)
        {
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Any, 0);
          
            while (true)
            {
                try
                {
                    
                    v = Convert.ToInt32(textSpeed.Text);
                    byte[] bytRecv = udpcRecv.Receive(ref remoteIpep);
                    string tmessage = Encoding.ASCII.GetString(
                        bytRecv, 0, bytRecv.Length);

                   
                        string[] A = tmessage.Split(' ');

                         dx = Convert.ToDouble(A[0]);
                        dy = Convert.ToDouble(A[1]);
                        SetCursorPos(Convert.ToInt32(Control.MousePosition.X - dx * v), Convert.ToInt32(Control.MousePosition.Y - dy * v));
                        //ShowMessage(txtRecvMssg, message);
                    
                    
                }
                catch (Exception ex)
                {
                    ShowMessage(txtRecvMssg, "错误了");

                    break;
                }
            }
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            f2.Show();
            this.Close();
        }

        private void txtRecvMssg_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form12_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        // 向TextBox中添加文本
        delegate void ShowMessageDelegate(RichTextBox txtbox, string message);
        private void ShowMessage(RichTextBox txtbox, string message)
        {
            if (txtbox.InvokeRequired)
            {
                ShowMessageDelegate showMessageDelegate = ShowMessage;
                txtbox.Invoke(showMessageDelegate, new object[] { txtbox, message });
            }
            else
            {
                txtbox.Text += message + "\r\n";
            }
        }



    }
}