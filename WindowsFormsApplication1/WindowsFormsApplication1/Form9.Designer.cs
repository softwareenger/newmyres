namespace WindowsFormsApplication1
{
    partial class Form9
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
            this.buttonLink = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textIP = new System.Windows.Forms.TextBox();
            this.textPORT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textMsg = new System.Windows.Forms.RichTextBox();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.textSendMsg = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonLink
            // 
            this.buttonLink.Location = new System.Drawing.Point(454, 186);
            this.buttonLink.Name = "buttonLink";
            this.buttonLink.Size = new System.Drawing.Size(124, 52);
            this.buttonLink.TabIndex = 0;
            this.buttonLink.Text = "连接到服务端";
            this.buttonLink.UseVisualStyleBackColor = true;
            this.buttonLink.Click += new System.EventHandler(this.buttonLink_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(256, 293);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point(397, 62);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(181, 21);
            this.textIP.TabIndex = 2;
            this.textIP.Text = "192.168.43.154";
            // 
            // textPORT
            // 
            this.textPORT.Location = new System.Drawing.Point(397, 119);
            this.textPORT.Name = "textPORT";
            this.textPORT.Size = new System.Drawing.Size(181, 21);
            this.textPORT.TabIndex = 3;
            this.textPORT.Text = "9876";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(362, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(350, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "端口";
            // 
            // textMsg
            // 
            this.textMsg.Location = new System.Drawing.Point(12, 12);
            this.textMsg.Name = "textMsg";
            this.textMsg.Size = new System.Drawing.Size(319, 263);
            this.textMsg.TabIndex = 6;
            this.textMsg.Text = "";
            this.textMsg.TextChanged += new System.EventHandler(this.textMsg_TextChanged);
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(502, 293);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(76, 23);
            this.buttonReturn.TabIndex = 7;
            this.buttonReturn.Text = "返回";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // textSendMsg
            // 
            this.textSendMsg.Location = new System.Drawing.Point(13, 293);
            this.textSendMsg.Name = "textSendMsg";
            this.textSendMsg.Size = new System.Drawing.Size(226, 21);
            this.textSendMsg.TabIndex = 8;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(256, 342);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 9;
            this.btnSelectFile.Text = "文件选择";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(364, 342);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(75, 23);
            this.btnSendFile.TabIndex = 10;
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(12, 342);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(188, 21);
            this.txtFileName.TabIndex = 11;
            // 
            // Form9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 379);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.textSendMsg);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.textMsg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textPORT);
            this.Controls.Add(this.textIP);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonLink);
            this.Name = "Form9";
            this.Text = "Form9";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLink;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.TextBox textPORT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox textMsg;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.TextBox textSendMsg;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.TextBox txtFileName;
    }
}