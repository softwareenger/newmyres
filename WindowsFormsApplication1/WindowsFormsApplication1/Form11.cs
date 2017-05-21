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

        [DllImport("user32.dll")] private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")] private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")] private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll")] private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")] private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
   
        private const int SW_SHOWNORMAL = 1;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWNOACTIVATE = 4;
 
        private const int WM_KEYDOWN = 0X100;
        private const int WM_KEYUP = 0X101;
        private const int WM_SYSCHAR = 0X106;
        private const int WM_SYSKEYUP = 0X105;
        private const int WM_SYSKEYDOWN = 0X104;
        private const int WM_CHAR = 0X102;





        private UdpClient udpcRecv;



        String IP = "10.22.26.67";

        
        bool IsUdpcRecvStart = false;
        
        Thread thrRecv;

       
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLink_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsUdpcRecvStart) 
                {

                    IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress ipa = ipe.AddressList[2];

                    if (textIP.Text != "如果填写0则使用默认" && textIP.Text != "")
                    {
                        IP = textIP.Text;
                    }
                    else
                    {
                        IP = ipa.ToString();
                    }


                    IPEndPoint localIpep = new IPEndPoint(IPAddress.Parse(IP), 9876); 


                    udpcRecv = new UdpClient(localIpep);

                    thrRecv = new Thread(ReceiveMessage);
                    thrRecv.Start();

                    IsUdpcRecvStart = true;
                    TextMsg.Text += "UDP监听器已成功启动\n";

                }
                else                  
                {
                    thrRecv.Abort(); 
                    udpcRecv.Close();

                    IsUdpcRecvStart = false;
                    TextMsg.Text += "UDP监听器已成功关闭\n";

                }
            }
            catch (Exception ex)
            {
                ShowMessage(TextMsg, "连接已经断开。");

             
            }
        }
       
       
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
                

                    string[] A = message.Split(' ');
                    if (kx == 1)
                    {
                       

                        y = Convert.ToDouble(A[1]);
                        ShowMessage(TextMsg, A[1]);
                        if (y <= -15)
                        {   
                            IntPtr myIntPtr = FindWindow(null, "Super Mario Bross Forever");
                            ShowWindow(myIntPtr, SW_RESTORE); 
                            SetForegroundWindow(myIntPtr); 
                            SendKeys.SendWait("+");
                        }
                    
                    }
                    else
                    {
                        
                        x = Convert.ToDouble(A[0]);
                        y = Convert.ToDouble(A[1]);
                        
                        if (x > 0)
                        {
                            IntPtr myIntPtr = FindWindow(null, "Super Mario Bross Forever"); 
                            ShowWindow(myIntPtr, SW_RESTORE); 
                            SetForegroundWindow(myIntPtr); 
                            SendKeys.SendWait("{LEFT}");
                        }
                        else
                        {
                            IntPtr myIntPtr = FindWindow(null, "Super Mario Bross Forever"); 
                            ShowWindow(myIntPtr, SW_RESTORE); 
                            SetForegroundWindow(myIntPtr); 
                            SendKeys.SendWait("{RIGHT}");
                        }
                        
                    }

                }
                catch (Exception ex)
                {
                    ShowMessage(TextMsg, "连接已经断开。");

                    break;
                }
            }
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            f2.Show();
            this.Close();
        }

 
  
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
