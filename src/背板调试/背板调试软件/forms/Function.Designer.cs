namespace LD.forms
{
    partial class Function
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timer = new System.Windows.Forms.CheckBox();
            this.Inter = new System.Windows.Forms.TextBox();
            this.xintiao = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Addr = new System.Windows.Forms.TextBox();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.update = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tb_set_addr = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.channel1 = new LD.forms.channel();
            this.channel2 = new LD.forms.channel();
            this.channel3 = new LD.forms.channel();
            this.channel4 = new LD.forms.channel();
            this.channel5 = new LD.forms.channel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.timer);
            this.groupBox1.Controls.Add(this.Inter);
            this.groupBox1.Controls.Add(this.xintiao);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(811, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "心跳";
            // 
            // timer
            // 
            this.timer.AutoSize = true;
            this.timer.Location = new System.Drawing.Point(216, 33);
            this.timer.Name = "timer";
            this.timer.Size = new System.Drawing.Size(48, 16);
            this.timer.TabIndex = 3;
            this.timer.Text = "定时";
            this.timer.UseVisualStyleBackColor = true;
            // 
            // Inter
            // 
            this.Inter.Location = new System.Drawing.Point(216, 10);
            this.Inter.Name = "Inter";
            this.Inter.Size = new System.Drawing.Size(48, 21);
            this.Inter.TabIndex = 2;
            this.Inter.Text = "500";
            // 
            // xintiao
            // 
            this.xintiao.Location = new System.Drawing.Point(6, 18);
            this.xintiao.Name = "xintiao";
            this.xintiao.Size = new System.Drawing.Size(151, 21);
            this.xintiao.TabIndex = 1;
            this.xintiao.Text = "01 00 00 00 00 00 00 03";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(163, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "读";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Addr);
            this.groupBox2.Location = new System.Drawing.Point(758, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(47, 49);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "地址";
            // 
            // Addr
            // 
            this.Addr.Location = new System.Drawing.Point(6, 20);
            this.Addr.Name = "Addr";
            this.Addr.Size = new System.Drawing.Size(35, 21);
            this.Addr.TabIndex = 1;
            this.Addr.Text = "02";
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(758, 388);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(337, 35);
            this.button4.TabIndex = 0;
            this.button4.Text = "重启";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.update);
            this.groupBox6.Controls.Add(this.button9);
            this.groupBox6.Location = new System.Drawing.Point(758, 60);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(337, 64);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "升级";
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(6, 16);
            this.update.Multiline = true;
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(240, 42);
            this.update.TabIndex = 1;
            this.update.Text = "0001 \r\n00000000000000000000000000000000 \r\n00000000";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(252, 20);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 0;
            this.button9.Text = "开始";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // result
            // 
            this.result.Location = new System.Drawing.Point(758, 190);
            this.result.Multiline = true;
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(337, 192);
            this.result.TabIndex = 16;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tb_set_addr);
            this.groupBox7.Controls.Add(this.button7);
            this.groupBox7.Location = new System.Drawing.Point(758, 127);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(337, 57);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "设置地址";
            // 
            // tb_set_addr
            // 
            this.tb_set_addr.Location = new System.Drawing.Point(6, 16);
            this.tb_set_addr.Name = "tb_set_addr";
            this.tb_set_addr.Size = new System.Drawing.Size(240, 21);
            this.tb_set_addr.TabIndex = 1;
            this.tb_set_addr.Text = "02 02 04 06 08 0A";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(252, 14);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "设置";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // channel1
            // 
            this.channel1.Addr = "";
            this.channel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.channel1.Id = "";
            this.channel1.Location = new System.Drawing.Point(1, 2);
            this.channel1.Name = "channel1";
            this.channel1.Size = new System.Drawing.Size(751, 100);
            this.channel1.TabIndex = 17;
            this.channel1.Values = "label1";
            // 
            // channel2
            // 
            this.channel2.Addr = "";
            this.channel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.channel2.Id = "";
            this.channel2.Location = new System.Drawing.Point(1, 106);
            this.channel2.Name = "channel2";
            this.channel2.Size = new System.Drawing.Size(751, 100);
            this.channel2.TabIndex = 18;
            this.channel2.Values = "label1";
            // 
            // channel3
            // 
            this.channel3.Addr = "";
            this.channel3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.channel3.Id = "";
            this.channel3.Location = new System.Drawing.Point(1, 210);
            this.channel3.Name = "channel3";
            this.channel3.Size = new System.Drawing.Size(751, 100);
            this.channel3.TabIndex = 19;
            this.channel3.Values = "label1";
            // 
            // channel4
            // 
            this.channel4.Addr = "";
            this.channel4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.channel4.Id = "";
            this.channel4.Location = new System.Drawing.Point(1, 316);
            this.channel4.Name = "channel4";
            this.channel4.Size = new System.Drawing.Size(751, 100);
            this.channel4.TabIndex = 20;
            this.channel4.Values = "label1";
            // 
            // channel5
            // 
            this.channel5.Addr = "";
            this.channel5.BackColor = System.Drawing.SystemColors.ControlDark;
            this.channel5.Id = "";
            this.channel5.Location = new System.Drawing.Point(1, 422);
            this.channel5.Name = "channel5";
            this.channel5.Size = new System.Drawing.Size(751, 100);
            this.channel5.TabIndex = 21;
            this.channel5.Values = "label1";
            // 
            // Function
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 532);
            this.Controls.Add(this.channel5);
            this.Controls.Add(this.channel4);
            this.Controls.Add(this.channel3);
            this.Controls.Add(this.channel2);
            this.Controls.Add(this.channel1);
            this.Controls.Add(this.result);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Function";
            this.Text = "Function";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox xintiao;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Addr;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox update;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox result;
        private System.Windows.Forms.CheckBox timer;
        private System.Windows.Forms.TextBox Inter;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox tb_set_addr;
        private System.Windows.Forms.Button button7;
        private channel channel1;
        private channel channel2;
        private channel channel3;
        private channel channel4;
        private channel channel5;
    }
}