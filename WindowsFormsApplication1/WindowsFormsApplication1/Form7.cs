using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form7 : Form
    {
        public Form8 f8;
        public Form7(){InitializeComponent();}
        public Form7(Form8 tf8)
        {
            InitializeComponent();
            f8 = tf8;
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        String IP = "123";
        Thread threadWatch = null; 
        Socket socketWatch = null; 

        private void buttonListener_Click(object sender, EventArgs e)
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
            txtMsg.AppendText("监听IP"+IP+"\n");

           
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
         
            IPAddress ipaddress = IPAddress.Parse( IP ); 
       
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(textPORT.Text.Trim())); 
          
            try
            {
                socketWatch.Bind(endpoint);
               
                socketWatch.Listen(20);
               
                threadWatch = new Thread(WatchConnecting);
               
                threadWatch.IsBackground = true;
            
                threadWatch.Start();
               
                txtMsg.AppendText("开始监听客户端传来的信息!" + "\r\n");
            }
            catch (Exception ex)
            {
                txtMsg.AppendText("系统异常消息:" + ex.Message);
                
            }
          

        }

      
        Socket socConnection = null;

      
        private void WatchConnecting()
        {
            while (true)  
            {
                socConnection = socketWatch.Accept();
                txtMsg.AppendText("客户端连接成功" + "\r\n");
                
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerRecMsg);
                Thread thr = new Thread(pts);
                thr.IsBackground = true;
             
                thr.Start(socConnection);
            }
        }

       
        /// <param name="sendMsg">发送的字符串信息</param>
        private void ServerSendMsg(string sendMsg)
        {
           
            byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);
           
            socConnection.Send(arrSendMsg);
           
            txtMsg.AppendText("我发送了:" + GetCurrentTime() + "\r\n" + sendMsg + "\r\n");
        }

       
        /// <param name="socketClientPara">客户端套接字对象</param>
      

        string strSRecMsg = null;
       
        private void ServerRecMsg(object socketClientPara)
        {
            Socket socketServer = socketClientPara as Socket;

            long fileLength = 0;
            CmdContainer cmd = new CmdContainer();
            int ReceiveBufferSize = 1024 * 1024;
            while (true)
            {
                int firstReceived = 0;
                byte[] buffer = new byte[ReceiveBufferSize];


                try
                {
                   
                    if (socketServer != null){ firstReceived = socketServer.Receive(buffer); }

                    if (firstReceived > 0) 
                    {
                        if (buffer[0] == '$') 
                        {
                      

                          
                                
                                string strRecMsg = Encoding.UTF8.GetString(buffer, 1, firstReceived-1);

                         
                                if (!cmd.GetCmd(strRecMsg))
                                {

                                    continue;
                                }


                                while (cmd.CmdQueue.Count != 0)
                                {
                                    string strCmd = cmd.CmdQueue.Dequeue();
                                    txtMsg.AppendText("对方发送了:" + strCmd + "\n");
                                }


                            
                        }
                        if (buffer[0] == 2)
                        {
                            string fileNameWithLength = Encoding.UTF8.GetString(buffer, 1, firstReceived - 1);
                            strSRecMsg = fileNameWithLength.Split('-').First(); 
                            fileLength = Convert.ToInt64(fileNameWithLength.Split('-').Last());
                        }
                        if (buffer[0] == 1)
                        {
                            string fileNameSuffix = strSRecMsg.Substring(strSRecMsg.LastIndexOf('.'));
                            SaveFileDialog sfDialog = new SaveFileDialog()
                            {
                                Filter = "(*" + fileNameSuffix + ")|*" + fileNameSuffix + "", 
                                FileName = strSRecMsg
                            };

                        
                            if (sfDialog.ShowDialog(this) == DialogResult.OK)
                            {
                                string savePath = sfDialog.FileName; 
                     
                                int received = 0;
                                long receivedTotalFilelength = 0;
                                bool firstWrite = true;
                                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                                {
                                    while (receivedTotalFilelength < fileLength) 
                                    {
                                        if (firstWrite)
                                        {
                                            fs.Write(buffer, 1, firstReceived - 1); 
                                            fs.Flush();

                                            receivedTotalFilelength += firstReceived - 1;

                                            firstWrite = false;
                                            continue;
                                        }
                                        received = socketServer.Receive(buffer); 
                                        fs.Write(buffer, 0, received);
                                        fs.Flush();

                                        receivedTotalFilelength += received;
                                    }
                                    fs.Close();
                                }

                                string fName = savePath.Substring(savePath.LastIndexOf("\\") + 1); 
                                string fPath = savePath.Substring(0, savePath.LastIndexOf("\\")); 
                                txtMsg.AppendText("对方:" + GetCurrentTime() + "\r\n您成功接收了文件" + fName + "\r\n保存路径为:" + fPath + "\r\n");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    txtMsg.AppendText("系统异常消息:" + ex.Message);
                    break;
                }
            }
        }




     


        private void buttonSend_Click(object sender, EventArgs e)
        {
          
            ServerSendMsg(textSendMsg.Text.Trim());
        }

     
        private void txtSendMsg_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                
                ServerSendMsg(textSendMsg.Text.Trim());
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
            f8.Show();
            this.Close();
        }

        private void textSendMsg_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMsg_TextChanged(object sender, EventArgs e)
        {

        }

     
    }
}
