using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
       

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string strClass, string strWindow);

       
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
        
        Thread thrRecv;

        
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecv_Click(object sender, EventArgs e)
        {

            try
            {
                IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipa = ipe.AddressList[ ClassVar.id ];
                IP = ipa.ToString();

                if (!IsUdpcRecvStart) 
                {
                    IPEndPoint localIpep = new IPEndPoint(
                        IPAddress.Parse(IP), 9876); 


                    udpcRecv = new UdpClient(localIpep);

                    thrRecv = new Thread(ReceiveMessage);
                    thrRecv.Start();

                    IsUdpcRecvStart = true;
                    txtRecvMssg.Text += "UDP监听器已成功启动\n";
                    txtRecvMssg.Text += "当前IP为"+ IPAddress.Parse(IP) + "\n";
                }
                else                  
                {
                    thrRecv.Abort(); 
                    udpcRecv.Close();

                    IsUdpcRecvStart = false;
                    txtRecvMssg.Text += "UDP监听器已成功关闭\n";

                }

            }
            catch (Exception ex)
            {
                ShowMessage(txtRecvMssg, "错误了");

            }


        }

      
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

                    if (tmessage != "*" && tmessage != "/")
                    {
                        string[] A = tmessage.Split(' ');

                        dx = Convert.ToDouble(A[0]);
                        dy = Convert.ToDouble(A[1]);
                        SetCursorPos(Convert.ToInt32(Control.MousePosition.X - dx * v), Convert.ToInt32(Control.MousePosition.Y - dy * v));
                    }
                    else
                    {
                        
                        if (tmessage == "*")
                        {
                            mouse_event(MouseEventFlag.LeftDown , 0, 0, 0, 0);
                        }
                        /*
                        else
                        {
                            mouse_event(MouseEventFlag.RightDown | MouseEventFlag.RightUp, 0, 0, 0, 0);
                        }
                        */
                    }
                    
                    
                }
                catch (Exception ex)
                {
                    ShowMessage(txtRecvMssg, "错误了行为错误\n");

                    break;
                }
            }
        }

        private void mouse_event(MouseEventFlag mouseEventFlag, int v1, int v2, int v3, int v4)
        {
            throw new NotImplementedException();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            f2.Show();
            this.Close();
        }



        private void Form12_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
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



    }
}