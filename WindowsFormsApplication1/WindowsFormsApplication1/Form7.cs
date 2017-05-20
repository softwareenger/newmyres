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


        Thread threadWatch = null; //负责监听客户端的线程
        Socket socketWatch = null; //负责监听客户端的套接字

        private void buttonListener_Click(object sender, EventArgs e)
        {
            //定义一个套接字用于监听客户端发来的信息  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //服务端发送信息 需要1个IP地址和端口号
            IPAddress ipaddress = IPAddress.Parse(textIP.Text.Trim()); //获取文本框输入的IP地址
            //将IP地址和端口号绑定到网络节点endpoint上 
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(textPORT.Text.Trim())); //获取文本框上输入的端口号
            //监听绑定的网络节点
            try
            {
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
                txtMsg.AppendText("开始监听客户端传来的信息!" + "\r\n");
            }
            catch (Exception ex)
            {
                txtMsg.AppendText("系统异常消息:" + ex.Message);
                
            }
          

        }

        //创建一个负责和客户端通信的套接字 
        Socket socConnection = null;

        /// <summary>
        /// 监听客户端发来的请求
        /// </summary>
        private void WatchConnecting()
        {
            while (true)  //持续不断监听客户端发来的请求
            {
                socConnection = socketWatch.Accept();
                txtMsg.AppendText("客户端连接成功" + "\r\n");
                //创建一个通信线程 
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerRecMsg);
                Thread thr = new Thread(pts);
                thr.IsBackground = true;
                //启动线程
                thr.Start(socConnection);
            }
        }

        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        private void ServerSendMsg(string sendMsg)
        {
            //将输入的字符串转换成 机器可以识别的字节数组
            byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);
            //向客户端发送字节数组信息
            socConnection.Send(arrSendMsg);
            //将发送的字符串信息附加到文本框txtMsg上
            txtMsg.AppendText("我发送了:" + GetCurrentTime() + "\r\n" + sendMsg + "\r\n");
        }

        /// <summary>
        /// 接收客户端发来的信息 
        /// </summary>
        /// <param name="socketClientPara">客户端套接字对象</param>
        /// 


        string strSRecMsg = null;
        /// <summary>
        /// 接收客户端发来的信息
        /// </summary>
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
                    //获取接收的数据,并存入内存缓冲区  返回一个字节数组的长度
                    if (socketServer != null){ firstReceived = socketServer.Receive(buffer); }

                    if (firstReceived > 0) //接受到的长度大于0 说明有信息或文件传来
                    {
                        if (buffer[0] == '$') //0为文字信息
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
                        if (buffer[0] == 2)//2为文件名字和长度
                        {
                            string fileNameWithLength = Encoding.UTF8.GetString(buffer, 1, firstReceived - 1);
                            strSRecMsg = fileNameWithLength.Split('-').First(); //文件名
                            fileLength = Convert.ToInt64(fileNameWithLength.Split('-').Last());//文件长度
                        }
                        if (buffer[0] == 1)//1为文件
                        {
                            string fileNameSuffix = strSRecMsg.Substring(strSRecMsg.LastIndexOf('.')); //文件后缀
                            SaveFileDialog sfDialog = new SaveFileDialog()
                            {
                                Filter = "(*" + fileNameSuffix + ")|*" + fileNameSuffix + "", //文件类型
                                FileName = strSRecMsg
                            };

                            //如果点击了对话框中的保存文件按钮 
                            if (sfDialog.ShowDialog(this) == DialogResult.OK)
                            {
                                string savePath = sfDialog.FileName; //获取文件的全路径
                                //保存文件
                                int received = 0;
                                long receivedTotalFilelength = 0;
                                bool firstWrite = true;
                                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                                {
                                    while (receivedTotalFilelength < fileLength) //之后收到的文件字节数组
                                    {
                                        if (firstWrite)
                                        {
                                            fs.Write(buffer, 1, firstReceived - 1); //第一次收到的文件字节数组 需要移除标识符1 后写入文件
                                            fs.Flush();

                                            receivedTotalFilelength += firstReceived - 1;

                                            firstWrite = false;
                                            continue;
                                        }
                                        received = socketServer.Receive(buffer); //之后每次收到的文件字节数组 可以直接写入文件
                                        fs.Write(buffer, 0, received);
                                        fs.Flush();

                                        receivedTotalFilelength += received;
                                    }
                                    fs.Close();
                                }

                                string fName = savePath.Substring(savePath.LastIndexOf("\\") + 1); //文件名 不带路径
                                string fPath = savePath.Substring(0, savePath.LastIndexOf("\\")); //文件路径 不带文件名
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




     

        //发送信息到客户端
        private void buttonSend_Click(object sender, EventArgs e)
        {
            //调用 ServerSendMsg方法  发送信息到客户端
            ServerSendMsg(textSendMsg.Text.Trim());
        }

        //快捷键 Enter 发送信息
        private void txtSendMsg_KeyDown(object sender, KeyEventArgs e)
        {
            //如果用户按下了Enter键
            if (e.KeyCode == Keys.Enter)
            {
                //则调用 服务器向客户端发送信息的方法
                ServerSendMsg(textSendMsg.Text.Trim());
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
