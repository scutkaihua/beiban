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
            this.ver = new System.Windows.Forms.TextBox();
            this.len = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pp = new System.Windows.Forms.Label();
            this.per = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // file
            // 
            this.file.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.file.Location = new System.Drawing.Point(3, 3);
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(268, 21);
            this.file.TabIndex = 1;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(169, 61);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(73, 23);
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
            this.pb.Size = new System.Drawing.Size(268, 2);
            this.pb.TabIndex = 4;
            // 
            // md5
            // 
            this.md5.Location = new System.Drawing.Point(55, 35);
            this.md5.Name = "md5";
            this.md5.Size = new System.Drawing.Size(210, 21);
            this.md5.TabIndex = 5;
            this.md5.Text = "00000000000000000000000000000000";
            // 
            // ver
            // 
            this.ver.Location = new System.Drawing.Point(4, 36);
            this.ver.Name = "ver";
            this.ver.Size = new System.Drawing.Size(35, 21);
            this.ver.TabIndex = 6;
            this.ver.Text = "0001";
            // 
            // len
            // 
            this.len.Location = new System.Drawing.Point(55, 63);
            this.len.Name = "len";
            this.len.Size = new System.Drawing.Size(66, 21);
            this.len.TabIndex = 7;
            this.len.Text = "00000000";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(17, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "V";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // pp
            // 
            this.pp.AutoSize = true;
            this.pp.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pp.Location = new System.Drawing.Point(3, 62);
            this.pp.Name = "pp";
            this.pp.Size = new System.Drawing.Size(35, 22);
            this.pp.TabIndex = 9;
            this.pp.Text = "0%";
            // 
            // per
            // 
            this.per.Location = new System.Drawing.Point(127, 62);
            this.per.Name = "per";
            this.per.Size = new System.Drawing.Size(36, 21);
            this.per.TabIndex = 10;
            this.per.Text = "200";
            // 
            // updata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.per);
            this.Controls.Add(this.pp);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.len);
            this.Controls.Add(this.ver);
            this.Controls.Add(this.md5);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.start);
            this.Controls.Add(this.file);
            this.Name = "updata";
            this.Size = new System.Drawing.Size(274, 96);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox file;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.TextBox md5;
        private System.Windows.Forms.TextBox ver;
        private System.Windows.Forms.TextBox len;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label pp;
        private System.Windows.Forms.TextBox per;
    }
}
