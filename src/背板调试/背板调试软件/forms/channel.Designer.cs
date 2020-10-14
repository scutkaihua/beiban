namespace LD.forms
{
    partial class channel
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
            this.id = new System.Windows.Forms.TextBox();
            this.values1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.addr = new System.Windows.Forms.TextBox();
            this.time = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LIndex = new System.Windows.Forms.Label();
            this.beibanAddr = new System.Windows.Forms.TextBox();
            this.values2 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.error = new LD.forms.State();
            this.warn = new LD.forms.State();
            this.state = new LD.forms.State();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(53, 4);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(133, 21);
            this.id.TabIndex = 4;
            // 
            // values1
            // 
            this.values1.AutoSize = true;
            this.values1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.values1.ForeColor = System.Drawing.Color.Coral;
            this.values1.Location = new System.Drawing.Point(47, 26);
            this.values1.Name = "values1";
            this.values1.Size = new System.Drawing.Size(45, 17);
            this.values1.TabIndex = 5;
            this.values1.Text = "label1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(3, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 92);
            this.button1.TabIndex = 9;
            this.button1.Text = "租";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(106, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(46, 63);
            this.button2.TabIndex = 10;
            this.button2.Text = "还";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(40, 1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(28, 92);
            this.button3.TabIndex = 11;
            this.button3.Text = "开仓";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(72, 1);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(31, 92);
            this.button4.TabIndex = 12;
            this.button4.Text = "运维";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // addr
            // 
            this.addr.Location = new System.Drawing.Point(29, 4);
            this.addr.Name = "addr";
            this.addr.Size = new System.Drawing.Size(22, 21);
            this.addr.TabIndex = 13;
            // 
            // time
            // 
            this.time.Location = new System.Drawing.Point(108, 72);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(46, 21);
            this.time.TabIndex = 14;
            this.time.Text = "0A";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.LIndex);
            this.panel1.Controls.Add(this.beibanAddr);
            this.panel1.Controls.Add(this.values2);
            this.panel1.Controls.Add(this.addr);
            this.panel1.Controls.Add(this.id);
            this.panel1.Controls.Add(this.values1);
            this.panel1.Location = new System.Drawing.Point(3, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 98);
            this.panel1.TabIndex = 18;
            // 
            // LIndex
            // 
            this.LIndex.AutoSize = true;
            this.LIndex.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LIndex.Location = new System.Drawing.Point(1, 73);
            this.LIndex.Name = "LIndex";
            this.LIndex.Size = new System.Drawing.Size(38, 17);
            this.LIndex.TabIndex = 16;
            this.LIndex.Text = "通道?";
            // 
            // beibanAddr
            // 
            this.beibanAddr.Location = new System.Drawing.Point(3, 4);
            this.beibanAddr.Name = "beibanAddr";
            this.beibanAddr.Size = new System.Drawing.Size(23, 21);
            this.beibanAddr.TabIndex = 15;
            // 
            // values2
            // 
            this.values2.AutoSize = true;
            this.values2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.values2.ForeColor = System.Drawing.Color.Coral;
            this.values2.Location = new System.Drawing.Point(117, 26);
            this.values2.Name = "values2";
            this.values2.Size = new System.Drawing.Size(45, 17);
            this.values2.TabIndex = 14;
            this.values2.Text = "label1";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(183, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.error);
            this.splitContainer2.Panel1.Controls.Add(this.warn);
            this.splitContainer2.Panel1.Controls.Add(this.state);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Panel2.Controls.Add(this.button3);
            this.splitContainer2.Panel2.Controls.Add(this.time);
            this.splitContainer2.Panel2.Controls.Add(this.button2);
            this.splitContainer2.Panel2.Controls.Add(this.button4);
            this.splitContainer2.Size = new System.Drawing.Size(328, 97);
            this.splitContainer2.SplitterDistance = 223;
            this.splitContainer2.TabIndex = 20;
            // 
            // error
            // 
            this.error.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.error.CheckColor = System.Drawing.Color.Empty;
            this.error.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.error.Location = new System.Drawing.Point(19, 68);
            this.error.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(333, 32);
            this.error.TabIndex = 17;
            // 
            // warn
            // 
            this.warn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.warn.CheckColor = System.Drawing.Color.Empty;
            this.warn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.warn.Location = new System.Drawing.Point(19, 35);
            this.warn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.warn.Name = "warn";
            this.warn.Size = new System.Drawing.Size(333, 32);
            this.warn.TabIndex = 16;
            // 
            // state
            // 
            this.state.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.state.CheckColor = System.Drawing.Color.Empty;
            this.state.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.state.Location = new System.Drawing.Point(20, 4);
            this.state.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.state.Name = "state";
            this.state.Size = new System.Drawing.Size(334, 32);
            this.state.TabIndex = 15;
            // 
            // channel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.panel1);
            this.Name = "channel";
            this.Size = new System.Drawing.Size(514, 100);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label values1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox addr;
        private System.Windows.Forms.TextBox time;

        private State state;
        private State warn;
        private State error;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label values2;
        public System.Windows.Forms.TextBox beibanAddr;
        private System.Windows.Forms.Label LIndex;
    }
}
