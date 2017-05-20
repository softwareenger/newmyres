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



        //创建 1个客户端套接字 和1个负责监听服务端请求的线程  
        Socket socketClient = null;
        Thread threadClient = null;

        private void buttonLink_Click(object sender, EventArgs e)
        {
            //定义一个套字节监听  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //需要获取文本框中的IP地址
            IPAddress ipaddress = IPAddress.Parse(textIP.Text.Trim());
            //将获取的ip地址和端口号绑定到网络节点endpoint上
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(textPORT.Text.Trim()));
            //这里客户端套接字连接到网络节点(服务端)用的方法是Connect 而不是Bind
            socketClient.Connect(endpoint);
            //创建一个线程 用于监听服务端发来的消息
            threadClient = new Thread(RecMsg);
            //将窗体线程设置为与后台同步
            threadClient.IsBackground = true;
            //启动线程
            threadClient.Start();
        }

        /// <summary>
        /// 接收服务端发来信息的方法
        /// </summary>
        private void RecMsg()
        {
            while (true) //持续监听服务端发来的消息
            {
                //定义一个1M的内存缓冲区 用于临时性存储接收到的信息
                byte[] arrRecMsg = new byte[1024 * 1024];
                //将客户端套接字接收到的数据存入内存缓冲区, 并获取其长度
                int length = socketClient.Receive(arrRecMsg);
                //将套接字获取到的字节数组转换为人可以看懂的字符串
                string strRecMsg = Encoding.UTF8.GetString(arrRecMsg, 0, length);
                //将发送的信息追加到聊天内容文本框中
                textMsg.AppendText("对方发送了:" + GetCurrentTime() + "\r\n" + strRecMsg + "\r\n");
            }
        }

        /// <summary>
        /// 发送字符串信息到服务端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        private void ClientSendMsg(string sendMsg,int kind)
        {
            textMsg.AppendText("我发送了:" + GetCurrentTime() + "\r\n" + sendMsg + "\r\n");

            byte[] arrClientMsg = Encoding.UTF8.GetBytes(sendMsg);
            //实际发送的字节数组比实际输入的长度多1 用于存取标识符
            byte[] arrClientSendMsg = new byte[arrClientMsg.Length + 1];
            if (kind == 0)
            {
                arrClientSendMsg[0] = Convert.ToByte('$');  //在索引为0的位置上添加一个标识符
            }
            else
            {
                arrClientSendMsg[0] = Convert.ToByte(kind);
            }
            Buffer.BlockCopy(arrClientMsg, 0, arrClientSendMsg, 1, arrClientMsg.Length);

            socketClient.Send(arrClientSendMsg);
          
        }

        //点击按钮btnSend 向服务端发送信息
        private void buttonSend_Click(object sender, EventArgs e)
        {
            //调用ClientSendMsg方法 将文本框中输入的信息发送给服务端
            ClientSendMsg(textSendMsg.Text.Trim(),0);
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
            f8.Show();
            this.Close();
        }

        string filePath = null;   //文件的全路径
        string fileName = null;   //文件名称(不包含路径) 
                                  //选择要发送的文件
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            if (ofDialog.ShowDialog(this) == DialogResult.OK)
            {
                fileName = ofDialog.SafeFileName; //获取选取文件的文件名
                txtFileName.Text = fileName;      //将文件名显示在文本框上 
                filePath = ofDialog.FileName;     //获取包含文件名的全路径
            }
        }


        /// <summary>
        /// 发送文件的方法
        /// </summary>
        /// <param name="fileFullPath">文件全路径(包含文件名称)</param>
        private void SendFile(string fileFullPath)
        {
            if (string.IsNullOrEmpty(fileFullPath))
            {
                MessageBox.Show(@"请选择需要发送的文件!");
                return;
            }

            //发送文件之前 将文件名字和长度发送过去
            long fileLength = new FileInfo(fileFullPath).Length;
            string totalMsg = string.Format("{0}-{1}", fileName, fileLength);
            ClientSendMsg(totalMsg, 2);
            /*先发送过去了 FileName和FileLength*/

            int SendBufferSize = 1024 * 1024;
            //发送文件
            byte[] buffer = new byte[SendBufferSize];

            using (FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read))
            {
                int readLength = 0;
                bool firstRead = true;
                long sentFileLength = 0;
                while ((readLength = fs.Read(buffer, 0, buffer.Length)) > 0 && sentFileLength < fileLength)
                {
                    sentFileLength += readLength;
                    //在第一次发送的字节流上加个前缀1
                    if (firstRead)
                    {
                        byte[] firstBuffer = new byte[readLength + 1];
                        firstBuffer[0] = 1; //告诉机器该发送的字节数组为文件
                        Buffer.BlockCopy(buffer, 0, firstBuffer, 1, readLength);

                        socketClient.Send(firstBuffer, 0, readLength + 1, SocketFlags.None);

                        firstRead = false;
                        continue;
                    }
                    //之后发送的均为直接读取的字节流
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
    }
}
