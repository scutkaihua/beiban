namespace LD.forms
{
    partial class updata
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
            this.file = new System.Windows.Forms.TextBox();
            this.start = new System.Windows.Forms.Button();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.md5 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // file
            // 
            this.file.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.file.Location = new System.Drawing.Point(3, 3);
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(501, 21);
            this.file.TabIndex = 1;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(378, 88);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(126, 23);
            this.start.TabIndex = 3;
            this.start.Text = "开始升级";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.Start_Click);
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.Location = new System.Drawing.Point(3, 28);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(501, 2);
            this.pb.TabIndex = 4;
            // 
            // md5
            // 
            this.md5.Location = new System.Drawing.Point(3, 36);
            this.md5.Name = "md5";
            this.md5.Size = new System.Drawing.Size(501, 21);
            this.md5.TabIndex = 5;
            // 
            // updata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.md5);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.start);
            this.Controls.Add(this.file);
            this.Name = "updata";
            this.Size = new System.Drawing.Size(507, 114);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox file;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.TextBox md5;
    }
}
