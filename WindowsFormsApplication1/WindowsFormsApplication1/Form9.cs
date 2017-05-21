using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form9 : Form
    {
        public Form8 f8;
        public Form9()
        {
            InitializeComponent();
        }
        public Form9(Form8 tf8)
        {
            InitializeComponent();
            f8 = tf8;
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }



      
        Socket socketClient = null;
        Thread threadClient = null;
        String IP = "123";
        private void buttonLink_Click(object sender, EventArgs e)
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
            try
            {
               
                socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           
                IPAddress ipaddress = IPAddress.Parse(textIP.Text.Trim());
              
                IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(textPORT.Text.Trim()));
               
                socketClient.Connect(endpoint);
              
                threadClient = new Thread(RecMsg);
             
                threadClient.IsBackground = true;
          
                threadClient.Start();

            }
            catch (Exception ex)
            {

                textMsg.AppendText(IP+"\n");
           
                textMsg.AppendText("系统异常消息:" + ex.Message);

            }
        }

       
        private void RecMsg()
        {
            while (true) 
            {
               
                byte[] arrRecMsg = new byte[1024 * 1024];
                
                int length = socketClient.Receive(arrRecMsg);
               
                string strRecMsg = Encoding.UTF8.GetString(arrRecMsg, 0, length);
              
                textMsg.AppendText("对方发送了:" + GetCurrentTime() + "\r\n" + strRecMsg + "\r\n");
            }
        }

       
        /// <param name="sendMsg">发送的字符串信息</param>
        private void ClientSendMsg(string sendMsg,int kind)
        {
            textMsg.AppendText("我发送了:" + GetCurrentTime() + "\r\n" + sendMsg + "\r\n");

            byte[] arrClientMsg = Encoding.UTF8.GetBytes(sendMsg);
         
            byte[] arrClientSendMsg = new byte[arrClientMsg.Length + 1];
            if (kind == 0)
            {
                arrClientSendMsg[0] = Convert.ToByte('$');  
            }
            else
            {
                arrClientSendMsg[0] = Convert.ToByte(kind);
            }
            Buffer.BlockCopy(arrClientMsg, 0, arrClientSendMsg, 1, arrClientMsg.Length);

            socketClient.Send(arrClientSendMsg);
          
        }

     
        private void buttonSend_Click(object sender, EventArgs e)
        {
           
            ClientSendMsg(textSendMsg.Text.Trim(),0);
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

        string filePath = null;   
        string fileName = null;   
                                 
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            if (ofDialog.ShowDialog(this) == DialogResult.OK)
            {
                fileName = ofDialog.SafeFileName; 
                txtFileName.Text = fileName;    
                filePath = ofDialog.FileName;    
            }
        }


        /// <param name="fileFullPath">文件全路径(包含文件名称)</param>
        private void SendFile(string fileFullPath)
        {
            if (string.IsNullOrEmpty(fileFullPath))
            {
                MessageBox.Show(@"请选择需要发送的文件!");
                return;
            }

            
            long fileLength = new FileInfo(fileFullPath).Length;
            string totalMsg = string.Format("{0}-{1}", fileName, fileLength);
            ClientSendMsg(totalMsg, 2);
         

            int SendBufferSize = 1024 * 1024;
           
            byte[] buffer = new byte[SendBufferSize];

            using (FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read))
            {
                int readLength = 0;
                bool firstRead = true;
                long sentFileLength = 0;
                while ((readLength = fs.Read(buffer, 0, buffer.Length)) > 0 && sentFileLength < fileLength)
                {
                    sentFileLength += readLength;
                  
                    if (firstRead)
                    {
                        byte[] firstBuffer = new byte[readLength + 1];
                        firstBuffer[0] = 1;
                        Buffer.BlockCopy(buffer, 0, firstBuffer, 1, readLength);

                        socketClient.Send(firstBuffer, 0, readLength + 1, SocketFlags.None);

                        firstRead = false;
                        continue;
                    }
                   
                    socketClient.Send(buffer, 0, readLength, SocketFlags.None);
                }
                fs.Close();
            }
            textMsg.AppendText("我发送了" + GetCurrentTime() + "\r\n您发送了文件:" + fileName + "\r\n");
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            SendFile(filePath);
        }

        private void textMsg_TextChanged(object sender, EventArgs e)
        {

        }

        private void textIP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
