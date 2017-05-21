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




     
        [StructLayout(LayoutKind.Sequential)]
        struct NativeRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

    
        [Flags]
        enum MouseEventFlag : uint 
        {
            Move = 0x0001,               
            LeftDown = 0x0002,         
            LeftUp = 0x0004,             
            RightDown = 0x0008,         
            RightUp = 0x0010,            
            MiddleDown = 0x0020,        
            MiddleUp = 0x0040,          
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,              
            VirtualDesk = 0x4000,        
            Absolute = 0x8000
        }

     
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

   
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy,
            uint data, UIntPtr extraInfo); 

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string strClass, string strWindow);

      
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
            string strClass, string strWindow);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(HandleRef hwnd, out NativeRECT rect);





        Thread threadWatch = null; 
        Socket socketWatch = null; 

        private void buttonLink_Click(object sender, EventArgs e)
        {
            
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           
            IPAddress ipaddress = IPAddress.Parse(textIP.Text.Trim()); 
         
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(textPORT.Text.Trim()));
       
            socketWatch.Bind(endpoint);
            
            socketWatch.Listen(20);
          
            threadWatch = new Thread(WatchConnecting);
          
            threadWatch.IsBackground = true;
          
            threadWatch.Start();
          
            Textshow.AppendText("开始监听客户端传来的信息!" + "\r\n");

        }

        
        Socket socConnection = null;

        
        private void WatchConnecting()
        {

            while (true)
            {

                socConnection = socketWatch.Accept();
                Textshow.AppendText("客户端连接成功" + "\r\n");
              
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerRecMsg);
                Thread thr = new Thread(pts);
                thr.IsBackground = true;
               
                thr.Start(socConnection);

                Thread thr2 = new Thread(mousemove);
            
                thr2.Start();


            }
        }



        
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

           
            while (true)
            {

                Socket socketServer = socketClientPara as Socket;
                byte[] arrServerRecMsg = new byte[1024 * 1024];
                if (socketServer.Available > 0)
                {
                    int length = socketServer.Receive(arrServerRecMsg);
                    if (length == 0) { continue; }
                 
                    string strSRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);
                    string[] sArray = Regex.Split(strSRecMsg, " ", RegexOptions.IgnoreCase);
                    dx = Convert.ToDouble(sArray[0]);
                    dy = Convert.ToDouble(sArray[1]);


                }
            }
        }


       
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
            IntPtr myIntPtr = FindWindow(null, "计算器"); 
            ShowWindow(myIntPtr, 9); 
            SetForegroundWindow(myIntPtr); 
            PostMessage(myIntPtr, 0x0101, 49, 0);
            PostMessage(myIntPtr, 0x0102, 49, 0);
        }

      
    }
}
