namespace WindowsFormsApplication1
{
    partial class Form6
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
            this.components = new System.ComponentModel.Container();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.buttonSure = new System.Windows.Forms.Button();
            this.buttonUnsure = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(289, 195);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(75, 23);
            this.buttonReturn.TabIndex = 0;
            this.buttonReturn.Text = "返回";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(271, 261);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // buttonSure
            // 
            this.buttonSure.Location = new System.Drawing.Point(289, 53);
            this.buttonSure.Name = "buttonSure";
            this.buttonSure.Size = new System.Drawing.Size(75, 23);
            this.buttonSure.TabIndex = 2;
            this.buttonSure.Text = "通过";
            this.buttonSure.UseVisualStyleBackColor = true;
            this.buttonSure.Click += new System.EventHandler(this.buttonSure_Click);
            // 
            // buttonUnsure
            // 
            this.buttonUnsure.Location = new System.Drawing.Point(289, 116);
            this.buttonUnsure.Name = "buttonUnsure";
            this.buttonUnsure.Size = new System.Drawing.Size(75, 23);
            this.buttonUnsure.TabIndex = 3;
            this.buttonUnsure.Text = "不通过";
            this.buttonUnsure.UseVisualStyleBackColor = true;
            this.buttonUnsure.Click += new System.EventHandler(this.buttonUnsure_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 300);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 333);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonUnsure);
            this.Controls.Add(this.buttonSure);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonReturn);
            this.MaximumSize = new System.Drawing.Size(411, 372);
            this.MinimumSize = new System.Drawing.Size(411, 372);
            this.Name = "Form6";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form6";
            this.Load += new System.EventHandler(this.Form6_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button buttonSure;
        private System.Windows.Forms.Button buttonUnsure;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
    }
}