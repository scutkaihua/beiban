namespace LD.forms
{
    partial class LaoHua
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
            this.label1 = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.addrs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.counter = new System.Windows.Forms.TextBox();
            this.start = new System.Windows.Forms.Button();
            this.pause = new System.Windows.Forms.Button();
            this.details = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.Button();
            this.prints = new System.Windows.Forms.TextBox();
            this.now = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(11, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "老化时间(秒):";
            // 
            // time
            // 
            this.time.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.time.Location = new System.Drawing.Point(129, 141);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(143, 26);
            this.time.TabIndex = 1;
            this.time.Text = "10";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.addrs);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.counter);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.time);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 206);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数设置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(11, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "仓道地址:";
            // 
            // addrs
            // 
            this.addrs.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addrs.Location = new System.Drawing.Point(14, 36);
            this.addrs.Multiline = true;
            this.addrs.Name = "addrs";
            this.addrs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.addrs.Size = new System.Drawing.Size(258, 99);
            this.addrs.TabIndex = 7;
            this.addrs.Text = "1=1,3,5,7,9\r\n2=2,4,6,8,10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(11, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "老化次数(次):";
            // 
            // counter
            // 
            this.counter.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.counter.Location = new System.Drawing.Point(129, 173);
            this.counter.Name = "counter";
            this.counter.Size = new System.Drawing.Size(143, 26);
            this.counter.TabIndex = 5;
            this.counter.Text = "10000";
            // 
            // start
            // 
            this.start.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.start.Location = new System.Drawing.Point(22, 223);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(55, 37);
            this.start.TabIndex = 3;
            this.start.Text = "开始";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.Start_Click);
            // 
            // pause
            // 
            this.pause.Enabled = false;
            this.pause.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pause.Location = new System.Drawing.Point(83, 223);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(52, 37);
            this.pause.TabIndex = 4;
            this.pause.Text = "暂停";
            this.pause.UseVisualStyleBackColor = true;
            // 
            // details
            // 
            this.details.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.details.Location = new System.Drawing.Point(199, 223);
            this.details.Name = "details";
            this.details.Size = new System.Drawing.Size(91, 37);
            this.details.TabIndex = 6;
            this.details.Text = "详情";
            this.details.UseVisualStyleBackColor = true;
            // 
            // logs
            // 
            this.logs.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logs.Location = new System.Drawing.Point(141, 223);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(52, 37);
            this.logs.TabIndex = 5;
            this.logs.Text = "日志";
            this.logs.UseVisualStyleBackColor = true;
            // 
            // prints
            // 
            this.prints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prints.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prints.Location = new System.Drawing.Point(296, 11);
            this.prints.Multiline = true;
            this.prints.Name = "prints";
            this.prints.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.prints.Size = new System.Drawing.Size(745, 546);
            this.prints.TabIndex = 8;
            this.prints.Text = "实时";
            // 
            // now
            // 
            this.now.AutoSize = true;
            this.now.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.now.Location = new System.Drawing.Point(10, 273);
            this.now.Name = "now";
            this.now.Size = new System.Drawing.Size(41, 12);
            this.now.TabIndex = 9;
            this.now.Text = "label4";
            // 
            // LaoHua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1053, 563);
            this.Controls.Add(this.now);
            this.Controls.Add(this.prints);
            this.Controls.Add(this.details);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.start);
            this.Controls.Add(this.groupBox1);
            this.MinimizeBox = false;
            this.Name = "LaoHua";
            this.Text = "老化测试";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox time;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox addrs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox counter;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button pause;
        private System.Windows.Forms.Button details;
        private System.Windows.Forms.Button logs;
        private System.Windows.Forms.TextBox prints;
        private System.Windows.Forms.Label now;
    }
}