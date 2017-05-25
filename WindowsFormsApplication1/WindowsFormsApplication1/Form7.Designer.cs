namespace WindowsFormsApplication1
{
    partial class Form7
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonReturn = new System.Windows.Forms.Button();
            this.textPORT = new System.Windows.Forms.TextBox();
            this.textIP = new System.Windows.Forms.TextBox();
            this.buttonListener = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textSendMsg = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(509, 328);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(128, 53);
            this.buttonReturn.TabIndex = 0;
            this.buttonReturn.Text = "返回";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // textPORT
            // 
            this.textPORT.Location = new System.Drawing.Point(101, 356);
            this.textPORT.Name = "textPORT";
            this.textPORT.Size = new System.Drawing.Size(172, 21);
            this.textPORT.TabIndex = 1;
            this.textPORT.Text = "9876";
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point(101, 324);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(172, 21);
            this.textIP.TabIndex = 2;
            this.textIP.Text = "如果填写0则使用默认";
            // 
            // buttonListener
            // 
            this.buttonListener.Location = new System.Drawing.Point(279, 324);
            this.buttonListener.Name = "buttonListener";
            this.buttonListener.Size = new System.Drawing.Size(107, 53);
            this.buttonListener.TabIndex = 3;
            this.buttonListener.Text = "开始监听";
            this.buttonListener.UseVisualStyleBackColor = true;
            this.buttonListener.Click += new System.EventHandler(this.buttonListener_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(12, 29);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(374, 247);
            this.txtMsg.TabIndex = 5;
            this.txtMsg.Text = "";
            this.txtMsg.TextChanged += new System.EventHandler(this.txtMsg_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "请输入IP：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 359);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "请确认端口：";
            // 
            // textSendMsg
            // 
            this.textSendMsg.Location = new System.Drawing.Point(392, 125);
            this.textSendMsg.Name = "textSendMsg";
            this.textSendMsg.Size = new System.Drawing.Size(245, 21);
            this.textSendMsg.TabIndex = 8;
            this.textSendMsg.TextChanged += new System.EventHandler(this.textSendMsg_TextChanged);
            // 
            // buttonSend
            // 
            this.buttonSend.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSend.Location = new System.Drawing.Point(509, 167);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(128, 64);
            this.buttonSend.TabIndex = 9;
            this.buttonSend.Text = "点击发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(392, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "请输入发送内容：";
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 394);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textSendMsg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.buttonListener);
            this.Controls.Add(this.textIP);
            this.Controls.Add(this.textPORT);
            this.Controls.Add(this.buttonReturn);
            this.MaximumSize = new System.Drawing.Size(665, 432);
            this.MinimumSize = new System.Drawing.Size(665, 432);
            this.Name = "Form7";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form7";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.TextBox textPORT;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.Button buttonListener;
        private System.Windows.Forms.RichTextBox txtMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textSendMsg;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Label label3;
    }
}