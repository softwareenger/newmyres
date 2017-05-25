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
    public partial class Form11 : Form
    {
        bool flag;
        public Form2 f2;
        public Form11()
        {
            InitializeComponent();
        }
        public Form11(Form2 tf2)
        {
            InitializeComponent();
            flag = false;
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

        [DllImport("user32.dll")] private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")] private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")] private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll")] private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")] private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
        //ShowWindow参数
        private const int SW_SHOWNORMAL = 1;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWNOACTIVATE = 4;
        //SendMessage参数
        private const int WM_KEYDOWN = 0X100;
        private const int WM_KEYUP = 0X101;
        private const int WM_SYSCHAR = 0X106;
        private const int WM_SYSKEYUP = 0X105;
        private const int WM_SYSKEYDOWN = 0X104;
        private const int WM_CHAR = 0X102;





        private UdpClient udpcRecv;



        String IP = "10.22.26.67";

        
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
        private void buttonLink_Click(object sender, EventArgs e)
        {
          
            if (!IsUdpcRecvStart) // 未监听的情况，开始监听
            {

                IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipa = ipe.AddressList[1];

                if (textIP.Text != "如果填写0则使用默认" && textIP.Text != "")
                {
                    IP = textIP.Text;
                }
                else
                {
                    IP = ipa.ToString();
                }


                IPEndPoint localIpep = new IPEndPoint(IPAddress.Parse(IP), 9876); // 本机IP和监听端口号


                udpcRecv = new UdpClient(localIpep);

                thrRecv = new Thread(ReceiveMessage);
                thrRecv.Start();

                IsUdpcRecvStart = true;
                TextMsg.Text += "UDP监听器已成功启动\n";

            }
            else                  // 正在监听的情况，终止监听
            {
                thrRecv.Abort(); // 必须先关闭这个线程，否则会异常
                udpcRecv.Close();

                IsUdpcRecvStart = false;
                TextMsg.Text += "UDP监听器已成功关闭\n";

            }
        }
       
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="obj"></param>
        private void ReceiveMessage(object obj)
        {
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Any, 0);
            double x, y, z;
            while (true)
            {
                try
                {

                    


                    byte[] bytRecv = udpcRecv.Receive(ref remoteIpep);
                    string tmessage = Encoding.ASCII.GetString(
                        bytRecv, 0, bytRecv.Length);

                    int kx = 0;
                    string message;
                    if (tmessage[0] == '@') { kx = 1; message = tmessage.Remove(0, 0); }
                    else
                    {
                        message = tmessage;
                    }
                    /*kind == 1 为重力*/

                    string[] A = message.Split(' ');
                    if (kx == 1)
                    {
                       

                        y = Convert.ToDouble(A[1]);
                        ShowMessage(TextMsg, A[1]);
                        if (y <= -15)
                        {   
                            IntPtr myIntPtr = FindWindow(null, "Super Mario Bross Forever"); //null为类名，可以用Spy++得到，也可以为空
                            ShowWindow(myIntPtr, SW_RESTORE); //将窗口还原
                            SetForegroundWindow(myIntPtr); //如果没有ShowWindow，此方法不能设置最小化的窗口
                            SendKeys.SendWait("+");
                        }
                        //ShowMessage(txtRecvMssg, message);
                    }
                    else
                    {
                        
                        x = Convert.ToDouble(A[0]);
                        y = Convert.ToDouble(A[1]);
                        
                        if (x > 0)
                        {
                            IntPtr myIntPtr = FindWindow(null, "Super Mario Bross Forever"); //null为类名，可以用Spy++得到，也可以为空
                            ShowWindow(myIntPtr, SW_RESTORE); //将窗口还原
                            SetForegroundWindow(myIntPtr); //如果没有ShowWindow，此方法不能设置最小化的窗口
                            SendKeys.SendWait("{LEFT}");
                        }
                        else
                        {
                            IntPtr myIntPtr = FindWindow(null, "Super Mario Bross Forever"); //null为类名，可以用Spy++得到，也可以为空
                            ShowWindow(myIntPtr, SW_RESTORE); //将窗口还原
                            SetForegroundWindow(myIntPtr); //如果没有ShowWindow，此方法不能设置最小化的窗口
                            SendKeys.SendWait("{RIGHT}");
                        }
                        
                    }

                }
                catch (Exception ex)
                {
                    ShowMessage(TextMsg, "错误了");

                    break;
                }
            }
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            f2.Show();
            this.Close();
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

        private void textIP_TextChanged(object sender, EventArgs e)
        {
            flag = true;
            if (textIP.Text == "如果填写0则使用默认")
            {
                textIP.Text = "";
            }

        }
    }












}
