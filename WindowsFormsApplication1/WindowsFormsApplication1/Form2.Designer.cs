namespace WindowsFormsApplication1
{
    partial class Form2
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
            this.button2 = new System.Windows.Forms.Button();
            this.buttonCommunicate = new System.Windows.Forms.Button();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.buttonExe = new System.Windows.Forms.Button();
            this.buttonMouse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(36, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "用户管理";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonUser_Click);
            // 
            // buttonCommunicate
            // 
            this.buttonCommunicate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCommunicate.Location = new System.Drawing.Point(155, 100);
            this.buttonCommunicate.Name = "buttonCommunicate";
            this.buttonCommunicate.Size = new System.Drawing.Size(75, 23);
            this.buttonCommunicate.TabIndex = 6;
            this.buttonCommunicate.Text = " 交流平台";
            this.buttonCommunicate.UseVisualStyleBackColor = true;
            this.buttonCommunicate.Click += new System.EventHandler(this.buttonCommunicate_Click);
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(108, 173);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(75, 23);
            this.buttonReturn.TabIndex = 8;
            this.buttonReturn.Text = "返回";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // buttonExe
            // 
            this.buttonExe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExe.Location = new System.Drawing.Point(155, 47);
            this.buttonExe.Name = "buttonExe";
            this.buttonExe.Size = new System.Drawing.Size(75, 23);
            this.buttonExe.TabIndex = 9;
            this.buttonExe.Text = "娱乐";
            this.buttonExe.UseVisualStyleBackColor = true;
            this.buttonExe.Click += new System.EventHandler(this.buttonExe_Click);
            // 
            // buttonMouse
            // 
            this.buttonMouse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMouse.Location = new System.Drawing.Point(36, 47);
            this.buttonMouse.Name = "buttonMouse";
            this.buttonMouse.Size = new System.Drawing.Size(75, 23);
            this.buttonMouse.TabIndex = 10;
            this.buttonMouse.Text = "控制鼠标";
            this.buttonMouse.UseVisualStyleBackColor = true;
            this.buttonMouse.Click += new System.EventHandler(this.buttonMouse_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.buttonMouse);
            this.Controls.Add(this.buttonExe);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.buttonCommunicate);
            this.Controls.Add(this.button2);
            this.MaximumSize = new System.Drawing.Size(308, 305);
            this.MinimumSize = new System.Drawing.Size(308, 305);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonCommunicate;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.Button buttonExe;
        private System.Windows.Forms.Button buttonMouse;
    }
}