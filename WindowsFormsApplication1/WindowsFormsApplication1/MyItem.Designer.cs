namespace WindowsFormsApplication1
{
    partial class MyItem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboPosition = new System.Windows.Forms.ComboBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboPosition
            // 
            this.comboPosition.FormattingEnabled = true;
            this.comboPosition.Items.AddRange(new object[] {
            "管理员",
            "用户"});
            this.comboPosition.Location = new System.Drawing.Point(199, 7);
            this.comboPosition.Name = "comboPosition";
            this.comboPosition.Size = new System.Drawing.Size(61, 20);
            this.comboPosition.TabIndex = 0;
            this.comboPosition.SelectedIndexChanged += new System.EventHandler(this.comboPosition_SelectedIndexChanged);
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(29, 10);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(29, 12);
            this.labelUser.TabIndex = 1;
            this.labelUser.Text = "用户";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(114, 10);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(29, 12);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "姓名";
            // 
            // MyItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.comboPosition);
            this.Name = "MyItem";
            this.Size = new System.Drawing.Size(273, 35);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboPosition;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelName;
    }
}
