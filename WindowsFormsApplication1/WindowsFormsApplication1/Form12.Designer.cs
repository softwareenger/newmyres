namespace WindowsFormsApplication1
{
    partial class Form12
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
            this.btnRecv = new System.Windows.Forms.Button();
            this.textSpeed = new System.Windows.Forms.TextBox();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRecvMssg = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnRecv
            // 
            this.btnRecv.Location = new System.Drawing.Point(174, 12);
            this.btnRecv.Name = "btnRecv";
            this.btnRecv.Size = new System.Drawing.Size(75, 23);
            this.btnRecv.TabIndex = 1;
            this.btnRecv.Text = "开启";
            this.btnRecv.UseVisualStyleBackColor = true;
            this.btnRecv.Click += new System.EventHandler(this.btnRecv_Click);
            // 
            // textSpeed
            // 
            this.textSpeed.Location = new System.Drawing.Point(14, 117);
            this.textSpeed.Name = "textSpeed";
            this.textSpeed.Size = new System.Drawing.Size(75, 21);
            this.textSpeed.TabIndex = 4;
            this.textSpeed.Text = "15";
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(174, 76);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(75, 23);
            this.buttonReturn.TabIndex = 5;
            this.buttonReturn.Text = "返回";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "鼠标速率调整";
            // 
            // txtRecvMssg
            // 
            this.txtRecvMssg.Location = new System.Drawing.Point(12, 12);
            this.txtRecvMssg.Name = "txtRecvMssg";
            this.txtRecvMssg.Size = new System.Drawing.Size(156, 87);
            this.txtRecvMssg.TabIndex = 3;
            this.txtRecvMssg.Text = "";
            // 
            // Form12
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 149);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.textSpeed);
            this.Controls.Add(this.txtRecvMssg);
            this.Controls.Add(this.btnRecv);
            this.MaximumSize = new System.Drawing.Size(273, 188);
            this.MinimumSize = new System.Drawing.Size(273, 188);
            this.Name = "Form12";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form12";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRecv;
        private System.Windows.Forms.TextBox textSpeed;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtRecvMssg;
    }
}