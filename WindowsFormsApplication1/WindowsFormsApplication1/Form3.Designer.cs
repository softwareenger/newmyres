namespace WindowsFormsApplication1
{
    partial class Form3
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
            this.buttonCheckUser = new System.Windows.Forms.Button();
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCheckUser
            // 
            this.buttonCheckUser.Location = new System.Drawing.Point(42, 63);
            this.buttonCheckUser.Name = "buttonCheckUser";
            this.buttonCheckUser.Size = new System.Drawing.Size(75, 23);
            this.buttonCheckUser.TabIndex = 0;
            this.buttonCheckUser.Text = "浏览用户";
            this.buttonCheckUser.UseVisualStyleBackColor = true;
            this.buttonCheckUser.Click += new System.EventHandler(this.buttonCheckUser_Click);
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Location = new System.Drawing.Point(164, 63);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(75, 23);
            this.buttonAddUser.TabIndex = 1;
            this.buttonAddUser.Text = "添加用户";
            this.buttonAddUser.UseVisualStyleBackColor = true;
            this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(99, 139);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(75, 23);
            this.buttonReturn.TabIndex = 2;
            this.buttonReturn.Text = "返回";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.buttonAddUser);
            this.Controls.Add(this.buttonCheckUser);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCheckUser;
        private System.Windows.Forms.Button buttonAddUser;
        private System.Windows.Forms.Button buttonReturn;
    }
}