﻿namespace LD.forms
{
    partial class channelHeartbreak
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
            this.channel_counts = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.addr_list = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CB_LANT = new System.Windows.Forms.CheckBox();
            this.CB_LISTEN = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.xintiao = new System.Windows.Forms.TextBox();
            this.textbox_timer = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.重新加载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ltx = new System.Windows.Forms.Label();
            this.lrx = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // channel_counts
            // 
            this.channel_counts.Location = new System.Drawing.Point(65, 17);
            this.channel_counts.Name = "channel_counts";
            this.channel_counts.Size = new System.Drawing.Size(129, 21);
            this.channel_counts.TabIndex = 1;
            this.channel_counts.Text = "4";
            this.channel_counts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.addr_list);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.channel_counts);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 75);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "地址列表";
            // 
            // addr_list
            // 
            this.addr_list.Location = new System.Drawing.Point(65, 44);
            this.addr_list.Name = "addr_list";
            this.addr_list.Size = new System.Drawing.Size(129, 21);
            this.addr_list.TabIndex = 3;
            this.addr_list.Text = "2";
            this.addr_list.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "通道个数";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lrx);
            this.groupBox2.Controls.Add(this.ltx);
            this.groupBox2.Controls.Add(this.CB_LANT);
            this.groupBox2.Controls.Add(this.CB_LISTEN);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.xintiao);
            this.groupBox2.Controls.Add(this.textbox_timer);
            this.groupBox2.Location = new System.Drawing.Point(218, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 75);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "定时读";
            // 
            // CB_LANT
            // 
            this.CB_LANT.AutoSize = true;
            this.CB_LANT.Location = new System.Drawing.Point(245, 20);
            this.CB_LANT.Name = "CB_LANT";
            this.CB_LANT.Size = new System.Drawing.Size(72, 16);
            this.CB_LANT.TabIndex = 11;
            this.CB_LANT.Text = "循环租借";
            this.CB_LANT.UseVisualStyleBackColor = true;
            this.CB_LANT.CheckedChanged += new System.EventHandler(this.CB_LANT_CheckedChanged);
            // 
            // CB_LISTEN
            // 
            this.CB_LISTEN.AutoSize = true;
            this.CB_LISTEN.Checked = true;
            this.CB_LISTEN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_LISTEN.Location = new System.Drawing.Point(217, 50);
            this.CB_LISTEN.Name = "CB_LISTEN";
            this.CB_LISTEN.Size = new System.Drawing.Size(72, 16);
            this.CB_LISTEN.TabIndex = 10;
            this.CB_LISTEN.Text = "使能监听";
            this.CB_LISTEN.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "分时充电";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "定时 ms";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(155, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "使能定时读";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // xintiao
            // 
            this.xintiao.Location = new System.Drawing.Point(65, 48);
            this.xintiao.Name = "xintiao";
            this.xintiao.Size = new System.Drawing.Size(146, 21);
            this.xintiao.TabIndex = 8;
            this.xintiao.Text = "01 00 00 00 00 00 00 03";
            // 
            // textbox_timer
            // 
            this.textbox_timer.Location = new System.Drawing.Point(65, 17);
            this.textbox_timer.Name = "textbox_timer";
            this.textbox_timer.Size = new System.Drawing.Size(84, 21);
            this.textbox_timer.TabIndex = 1;
            this.textbox_timer.Text = "2000";
            this.textbox_timer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(654, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 66);
            this.button1.TabIndex = 7;
            this.button1.Text = "实时曲线";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.AutoScroll = true;
            this.panel.ContextMenuStrip = this.contextMenuStrip1;
            this.panel.Location = new System.Drawing.Point(12, 84);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(753, 482);
            this.panel.TabIndex = 9;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重新加载ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // 重新加载ToolStripMenuItem
            // 
            this.重新加载ToolStripMenuItem.Name = "重新加载ToolStripMenuItem";
            this.重新加载ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.重新加载ToolStripMenuItem.Text = "重新加载";
            this.重新加载ToolStripMenuItem.Click += new System.EventHandler(this.重新加载ToolStripMenuItem_Click);
            // 
            // ltx
            // 
            this.ltx.AutoSize = true;
            this.ltx.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ltx.ForeColor = System.Drawing.Color.Red;
            this.ltx.Location = new System.Drawing.Point(323, 17);
            this.ltx.Name = "ltx";
            this.ltx.Size = new System.Drawing.Size(29, 21);
            this.ltx.TabIndex = 12;
            this.ltx.Text = "TX";
            // 
            // lrx
            // 
            this.lrx.AutoSize = true;
            this.lrx.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lrx.ForeColor = System.Drawing.Color.DarkGreen;
            this.lrx.Location = new System.Drawing.Point(323, 44);
            this.lrx.Name = "lrx";
            this.lrx.Size = new System.Drawing.Size(30, 21);
            this.lrx.TabIndex = 13;
            this.lrx.Text = "RX";
            // 
            // channelHeartbreak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(779, 578);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimizeBox = false;
            this.Name = "channelHeartbreak";
            this.Text = "多通道监控";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox channel_counts;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox addr_list;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textbox_timer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox xintiao;
        private System.Windows.Forms.FlowLayoutPanel panel;
        private System.Windows.Forms.CheckBox CB_LISTEN;
        private System.Windows.Forms.CheckBox CB_LANT;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 重新加载ToolStripMenuItem;
        private System.Windows.Forms.Label lrx;
        private System.Windows.Forms.Label ltx;
    }
}