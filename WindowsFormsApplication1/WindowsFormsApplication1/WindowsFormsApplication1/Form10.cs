using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class Form10 : Form
    {
        public Form2 f2;
        public Form10()
        {
            InitializeComponent();
        }
        public Form10(Form2 tf2)
        {
            InitializeComponent();
            f2 = tf2;
            TextBox.CheckForIllegalCrossThreadCalls = false;
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





        Thread threadWatch = null; //负责监听客户端的线程
        Socket socketWatch = null; //负责监听客户端的套接字

        private void buttonLink_Click(object sender, EventArgs e)
        {
            //定义一个套接字用于监听客户端发来的信息  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //服务端发送信息 需要1个IP地址和端口号
            IPAddress ipaddress = IPAddress.Parse(textIP.Text.Trim()); //获取文本框输入的IP地址
            //将IP地址和端口号绑定到网络节点endpoint上 
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(textPORT.Text.Trim())); //获取文本框上输入的端口号
            //监听绑定的网络节点
            socketWatch.Bind(endpoint);
            //将套接字的监听队列长度限制为20
            socketWatch.Listen(20);
            //创建一个监听线程 
            threadWatch = new Thread(WatchConnecting);
            //将窗体线程设置为与后台同步
            threadWatch.IsBackground = true;
            //启动线程
            threadWatch.Start();
            //启动线程后 txtMsg文本框显示相应提示
            Textshow.AppendText("开始监听客户端传来的信息!" + "\r\n");

        }

        //创建一个负责和客户端通信的套接字 
        Socket socConnection = null;

        /// <summary>
        /// 监听客户端发来的请求
        /// </summary>
        private void WatchConnecting()
        {

            while (true)
            {

                socConnection = socketWatch.Accept();
                Textshow.AppendText("客户端连接成功" + "\r\n");
                //创建一个通信线程 
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerRecMsg);
                Thread thr = new Thread(pts);
                thr.IsBackground = true;
                //启动线程
                thr.Start(socConnection);

                Thread thr2 = new Thread(mousemove);
                //启动线程
                thr2.Start();


            }
        }



        /// <summary>
        /// 接收客户端发来的信息 
        /// </summary>
        /// <param name="socketClientPara">客户端套接字对象</param>


        double v = 0.6;
        double dx = 1.0, dy = 1.0;
        double f = 1.0;
        private void mousemove()
        {
            while (true)
            {
                Thread.Sleep(10);
                if (!(dx <= f && dx >= -f && dy <= f && dy >= -f))
                    SetCursorPos(Convert.ToInt32(Control.MousePosition.X - dx * v), Convert.ToInt32(Control.MousePosition.Y - dy * v));


            }
        }

        private void ServerRecMsg(object socketClientPara)
        {

            //将接收到的信息存入到内存缓冲区,并返回其字节数组的长度
            while (true)
            {

                Socket socketServer = socketClientPara as Socket;
                byte[] arrServerRecMsg = new byte[1024 * 1024];
                if (socketServer.Available > 0)
                {
                    int length = socketServer.Receive(arrServerRecMsg);
                    if (length == 0) { continue; }
                    //将机器接受到的字节数组转换为人可以读懂的字符串
                    string strSRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);
                    string[] sArray = Regex.Split(strSRecMsg, " ", RegexOptions.IgnoreCase);
                    dx = Convert.ToDouble(sArray[0]);
                    dy = Convert.ToDouble(sArray[1]);


                }
            }
        }


        /// <summary>
        /// 获取当前系统时间的方法
        /// </summary>
        /// <returns>当前时间</returns>
        private DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }

   
        private void buttonReturn_Click(object sender, EventArgs e)
        {
            f2.Show();
            this.Close();
        }








        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hwnd, int msg, uint wParam, uint lParam);

        [DllImport("user32.dll")] private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")] private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")] private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll")] private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")] private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);

      

        private void labelText_Click(object sender, EventArgs e)
        {
            IntPtr myIntPtr = FindWindow(null, "计算器"); //null为类名，可以用Spy++得到，也可以为空
            ShowWindow(myIntPtr, 9); //将窗口还原
            SetForegroundWindow(myIntPtr); //如果没有ShowWindow，此方法不能设置最小化的窗口
            PostMessage(myIntPtr, 0x0101, 49, 0);
            PostMessage(myIntPtr, 0x0102, 49, 0);
        }

      
    }
}
