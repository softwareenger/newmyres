using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public class CmdContainer
    {
        String buff;

        public Queue<string> CmdQueue { set; get; }       // 存储已经收到的指令，该指令是多条完整的指令  

        public CmdContainer()
        {
            CmdQueue = new Queue<string>();
        }

        // 尝试获得指令，如果有指令可以获取就返回true  
        public bool GetCmd(string str)
        {

            buff = str;
            GetCmd();
            return CmdQueue.Count == 0 ? false : true;
        }

        // 根据缓存区的内容提取指令  
        private void GetCmd()
        {
            
            int i;
            String t = "";
            for (i = 0; i < buff.Length; i++)
            {
                if (buff[i] != '$')
                {
                    t += buff[i];
                }
                else
                {
                    if (t != "")
                    {
                        CmdQueue.Enqueue(t);
                    }
                    t = "";
                }
            }
            if (t != "") { CmdQueue.Enqueue(t); }
              

        }
    }
}
