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
            this.SuspendLayout();
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(557, 329);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(81, 31);
            this.buttonReturn.TabIndex = 0;
            this.buttonReturn.Text = "返回";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // textPORT
            // 
            this.textPORT.Location = new System.Drawing.Point(466, 113);
            this.textPORT.Name = "textPORT";
            this.textPORT.Size = new System.Drawing.Size(172, 21);
            this.textPORT.TabIndex = 1;
            this.textPORT.Text = "9876";
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point(466, 61);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(172, 21);
            this.textIP.TabIndex = 2;
            this.textIP.Text = "192.168.43.154";
            // 
            // buttonListener
            // 
            this.buttonListener.Location = new System.Drawing.Point(531, 165);
            this.buttonListener.Name = "buttonListener";
            this.buttonListener.Size = new System.Drawing.Size(107, 53);
            this.buttonListener.TabIndex = 3;
            this.buttonListener.Text = "开始监听";
            this.buttonListener.UseVisualStyleBackColor = true;
            this.buttonListener.Click += new System.EventHandler(this.buttonListener_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(12, 61);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(374, 247);
            this.txtMsg.TabIndex = 5;
            this.txtMsg.Text = "";
            this.txtMsg.TextChanged += new System.EventHandler(this.txtMsg_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(426, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(426, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "端口";
            // 
            // textSendMsg
            // 
            this.textSendMsg.Location = new System.Drawing.Point(13, 339);
            this.textSendMsg.Name = "textSendMsg";
            this.textSendMsg.Size = new System.Drawing.Size(277, 21);
            this.textSendMsg.TabIndex = 8;
            this.textSendMsg.TextChanged += new System.EventHandler(this.textSendMsg_TextChanged);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(311, 339);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 9;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 393);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textSendMsg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.buttonListener);
            this.Controls.Add(this.textIP);
            this.Controls.Add(this.textPORT);
            this.Controls.Add(this.buttonReturn);
            this.Name = "Form7";
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
    }
}