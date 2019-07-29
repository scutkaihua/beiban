namespace LD.forms
{
    partial class PacketView
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rtb_view = new System.Windows.Forms.RichTextBox();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清屏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示数据帧ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示解析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb_view
            // 
            this.rtb_view.ContextMenuStrip = this.cms;
            this.rtb_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_view.Location = new System.Drawing.Point(0, 0);
            this.rtb_view.Name = "rtb_view";
            this.rtb_view.Size = new System.Drawing.Size(1039, 424);
            this.rtb_view.TabIndex = 0;
            this.rtb_view.Text = "";
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清屏ToolStripMenuItem,
            this.显示数据帧ToolStripMenuItem,
            this.显示解析ToolStripMenuItem});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(137, 70);
            // 
            // 清屏ToolStripMenuItem
            // 
            this.清屏ToolStripMenuItem.Name = "清屏ToolStripMenuItem";
            this.清屏ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.清屏ToolStripMenuItem.Text = "清屏";
            this.清屏ToolStripMenuItem.Click += new System.EventHandler(this.清屏ToolStripMenuItem_Click);
            // 
            // 显示数据帧ToolStripMenuItem
            // 
            this.显示数据帧ToolStripMenuItem.Checked = true;
            this.显示数据帧ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.显示数据帧ToolStripMenuItem.Name = "显示数据帧ToolStripMenuItem";
            this.显示数据帧ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.显示数据帧ToolStripMenuItem.Text = "显示数据帧";
            this.显示数据帧ToolStripMenuItem.Click += new System.EventHandler(this.显示数据帧ToolStripMenuItem_Click);
            // 
            // 显示解析ToolStripMenuItem
            // 
            this.显示解析ToolStripMenuItem.Name = "显示解析ToolStripMenuItem";
            this.显示解析ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.显示解析ToolStripMenuItem.Text = "显示解析";
            this.显示解析ToolStripMenuItem.Click += new System.EventHandler(this.显示解析ToolStripMenuItem_Click);
            // 
            // PacketView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1039, 424);
            this.Controls.Add(this.rtb_view);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PacketView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.cms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_view;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem 清屏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示数据帧ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示解析ToolStripMenuItem;
    }
}
