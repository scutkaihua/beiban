namespace LD.forms
{
    partial class ChartView
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
            this.MyChart = new LiveCharts.WinForms.CartesianChart();
            this.scale_option = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Tips = new System.Windows.Forms.Label();
            this.MIN = new System.Windows.Forms.TextBox();
            this.MAX = new System.Windows.Forms.TextBox();
            this.CB = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.加载数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.保存数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.清除历史数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyChart
            // 
            this.MyChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MyChart.BackColor = System.Drawing.Color.White;
            this.MyChart.Location = new System.Drawing.Point(12, 42);
            this.MyChart.Name = "MyChart";
            this.MyChart.Size = new System.Drawing.Size(770, 587);
            this.MyChart.TabIndex = 0;
            this.MyChart.Text = "cartesianChart1";
            // 
            // scale_option
            // 
            this.scale_option.FormattingEnabled = true;
            this.scale_option.Items.AddRange(new object[] {
            "无缩放",
            "缩放 X",
            "缩放 Y",
            "缩放 XY"});
            this.scale_option.Location = new System.Drawing.Point(209, 9);
            this.scale_option.Name = "scale_option";
            this.scale_option.Size = new System.Drawing.Size(59, 20);
            this.scale_option.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(274, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "选择数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Tips
            // 
            this.Tips.AutoSize = true;
            this.Tips.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Tips.ForeColor = System.Drawing.Color.Fuchsia;
            this.Tips.Location = new System.Drawing.Point(374, 8);
            this.Tips.Name = "Tips";
            this.Tips.Size = new System.Drawing.Size(37, 30);
            this.Tips.TabIndex = 3;
            this.Tips.Text = "tips";
            // 
            // MIN
            // 
            this.MIN.Location = new System.Drawing.Point(90, 9);
            this.MIN.Name = "MIN";
            this.MIN.Size = new System.Drawing.Size(55, 21);
            this.MIN.TabIndex = 4;
            // 
            // MAX
            // 
            this.MAX.Location = new System.Drawing.Point(151, 9);
            this.MAX.Name = "MAX";
            this.MAX.Size = new System.Drawing.Size(52, 21);
            this.MAX.TabIndex = 5;
            // 
            // CB
            // 
            this.CB.FormattingEnabled = true;
            this.CB.Location = new System.Drawing.Point(13, 9);
            this.CB.Name = "CB";
            this.CB.Size = new System.Drawing.Size(71, 20);
            this.CB.TabIndex = 6;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载数据ToolStripMenuItem,
            this.toolStripSeparator2,
            this.保存数据ToolStripMenuItem,
            this.toolStripSeparator1,
            this.清除历史数据ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 82);
            // 
            // 加载数据ToolStripMenuItem
            // 
            this.加载数据ToolStripMenuItem.Name = "加载数据ToolStripMenuItem";
            this.加载数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.加载数据ToolStripMenuItem.Text = "加载数据";
            this.加载数据ToolStripMenuItem.Click += new System.EventHandler(this.加载数据ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // 保存数据ToolStripMenuItem
            // 
            this.保存数据ToolStripMenuItem.Name = "保存数据ToolStripMenuItem";
            this.保存数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.保存数据ToolStripMenuItem.Text = "保存数据";
            this.保存数据ToolStripMenuItem.Click += new System.EventHandler(this.保存数据ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // 清除历史数据ToolStripMenuItem
            // 
            this.清除历史数据ToolStripMenuItem.Name = "清除历史数据ToolStripMenuItem";
            this.清除历史数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清除历史数据ToolStripMenuItem.Text = "清除历史数据";
            this.清除历史数据ToolStripMenuItem.Click += new System.EventHandler(this.清除历史数据ToolStripMenuItem_Click);
            // 
            // ChartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(794, 641);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.CB);
            this.Controls.Add(this.MAX);
            this.Controls.Add(this.MIN);
            this.Controls.Add(this.Tips);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.scale_option);
            this.Controls.Add(this.MyChart);
            this.Name = "ChartView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart MyChart;
        private System.Windows.Forms.ComboBox scale_option;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Tips;
        private System.Windows.Forms.TextBox MIN;
        private System.Windows.Forms.TextBox MAX;
        private System.Windows.Forms.ComboBox CB;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 清除历史数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 保存数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
