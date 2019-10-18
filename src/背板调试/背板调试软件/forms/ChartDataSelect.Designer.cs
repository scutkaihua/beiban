namespace LD.forms
{
    partial class ChartDataSelect
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
            this.runtime = new System.Windows.Forms.CheckBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.items = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channel0 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.channel1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.channel2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.channel3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Channel4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // runtime
            // 
            this.runtime.AutoSize = true;
            this.runtime.Location = new System.Drawing.Point(13, 13);
            this.runtime.Name = "runtime";
            this.runtime.Size = new System.Drawing.Size(72, 16);
            this.runtime.TabIndex = 0;
            this.runtime.Text = "实时数据";
            this.runtime.UseVisualStyleBackColor = true;
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.items,
            this.channel0,
            this.channel1,
            this.channel2,
            this.channel3,
            this.Channel4});
            this.dgv.Location = new System.Drawing.Point(13, 44);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 23;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv.Size = new System.Drawing.Size(654, 683);
            this.dgv.TabIndex = 1;
            // 
            // items
            // 
            this.items.HeaderText = "数据项";
            this.items.Name = "items";
            this.items.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // channel0
            // 
            this.channel0.HeaderText = "通道1";
            this.channel0.Name = "channel0";
            this.channel0.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // channel1
            // 
            this.channel1.HeaderText = "通道2";
            this.channel1.Name = "channel1";
            this.channel1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // channel2
            // 
            this.channel2.HeaderText = "通道3";
            this.channel2.Name = "channel2";
            this.channel2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // channel3
            // 
            this.channel3.HeaderText = "通道4";
            this.channel3.Name = "channel3";
            this.channel3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Channel4
            // 
            this.Channel4.HeaderText = "通道5";
            this.Channel4.Name = "Channel4";
            this.Channel4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ChartDataSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 739);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.runtime);
            this.Name = "ChartDataSelect";
            this.Text = "Test";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox runtime;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn items;
        private System.Windows.Forms.DataGridViewCheckBoxColumn channel0;
        private System.Windows.Forms.DataGridViewCheckBoxColumn channel1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn channel2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn channel3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Channel4;
    }
}