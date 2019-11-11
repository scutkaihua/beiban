namespace LD.forms
{
    partial class LaoHuaView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.query = new System.Windows.Forms.Button();
            this.QueryText = new System.Windows.Forms.TextBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(12, 11);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(90, 23);
            this.query.TabIndex = 2;
            this.query.Text = "查询";
            this.query.UseVisualStyleBackColor = true;
            this.query.Click += new System.EventHandler(this.Query_Click);
            // 
            // QueryText
            // 
            this.QueryText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QueryText.Location = new System.Drawing.Point(108, 13);
            this.QueryText.Name = "QueryText";
            this.QueryText.Size = new System.Drawing.Size(964, 21);
            this.QueryText.TabIndex = 3;
            this.QueryText.Text = "select * from task;";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeight = 30;
            this.dgv.GridColor = System.Drawing.SystemColors.Control;
            this.dgv.Location = new System.Drawing.Point(12, 41);
            this.dgv.Name = "dgv";
            this.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv.RowTemplate.Height = 23;
            this.dgv.ShowCellErrors = false;
            this.dgv.ShowCellToolTips = false;
            this.dgv.ShowEditingIcon = false;
            this.dgv.ShowRowErrors = false;
            this.dgv.Size = new System.Drawing.Size(1060, 523);
            this.dgv.TabIndex = 0;
            // 
            // LaoHuaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1084, 576);
            this.Controls.Add(this.QueryText);
            this.Controls.Add(this.query);
            this.Controls.Add(this.dgv);
            this.Name = "LaoHuaView";
            this.Text = "老化";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.TextBox QueryText;
        private System.Windows.Forms.DataGridView dgv;
    }
}