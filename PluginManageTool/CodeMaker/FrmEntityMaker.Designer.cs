namespace PluginManageTool.CodeMaker
{
    partial class FrmEntityMaker
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.ckAll = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DbcontionStr = new System.Windows.Forms.TextBox();
            this.btnGetTableList = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gridTable = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnGeneration = new System.Windows.Forms.Button();
            this.txtnamespace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTable)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.ckAll);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.DbcontionStr);
            this.panel2.Controls.Add(this.btnGetTableList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 343);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(495, 88);
            this.panel2.TabIndex = 5;
            // 
            // ckAll
            // 
            this.ckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckAll.AutoSize = true;
            this.ckAll.Location = new System.Drawing.Point(411, 6);
            this.ckAll.Name = "ckAll";
            this.ckAll.Size = new System.Drawing.Size(78, 16);
            this.ckAll.TabIndex = 5;
            this.ckAll.Text = "全选/反选";
            this.ckAll.UseVisualStyleBackColor = true;
            this.ckAll.CheckedChanged += new System.EventHandler(this.ckAll_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "数据库连接字符串";
            // 
            // DbcontionStr
            // 
            this.DbcontionStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DbcontionStr.Location = new System.Drawing.Point(6, 19);
            this.DbcontionStr.Multiline = true;
            this.DbcontionStr.Name = "DbcontionStr";
            this.DbcontionStr.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DbcontionStr.Size = new System.Drawing.Size(391, 66);
            this.DbcontionStr.TabIndex = 3;
            this.DbcontionStr.Text = "Data Source=127.0.0.1;Initial Catalog=EFWDB;User ID=sa;pwd=1;";
            // 
            // btnGetTableList
            // 
            this.btnGetTableList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetTableList.Location = new System.Drawing.Point(411, 62);
            this.btnGetTableList.Name = "btnGetTableList";
            this.btnGetTableList.Size = new System.Drawing.Size(75, 23);
            this.btnGetTableList.TabIndex = 0;
            this.btnGetTableList.Text = "获取表结构";
            this.btnGetTableList.UseVisualStyleBackColor = true;
            this.btnGetTableList.Click += new System.EventHandler(this.btnGetTableList_Click);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Check";
            this.Column1.HeaderText = "选择";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 50;
            // 
            // gridTable
            // 
            this.gridTable.AllowUserToAddRows = false;
            this.gridTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.gridTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTable.Location = new System.Drawing.Point(0, 37);
            this.gridTable.Name = "gridTable";
            this.gridTable.ReadOnly = true;
            this.gridTable.RowTemplate.Height = 23;
            this.gridTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTable.Size = new System.Drawing.Size(495, 306);
            this.gridTable.TabIndex = 0;
            this.gridTable.Click += new System.EventHandler(this.gridTable_Click);
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "TableName";
            this.Column2.HeaderText = "表名";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridTable);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 343);
            this.panel1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnGeneration);
            this.panel3.Controls.Add(this.txtnamespace);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(495, 37);
            this.panel3.TabIndex = 1;
            this.panel3.Visible = false;
            // 
            // btnGeneration
            // 
            this.btnGeneration.Location = new System.Drawing.Point(411, 4);
            this.btnGeneration.Name = "btnGeneration";
            this.btnGeneration.Size = new System.Drawing.Size(75, 23);
            this.btnGeneration.TabIndex = 2;
            this.btnGeneration.Text = "生成";
            this.btnGeneration.UseVisualStyleBackColor = true;
            this.btnGeneration.Click += new System.EventHandler(this.btnGeneration_Click);
            // 
            // txtnamespace
            // 
            this.txtnamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtnamespace.Location = new System.Drawing.Point(63, 6);
            this.txtnamespace.Name = "txtnamespace";
            this.txtnamespace.Size = new System.Drawing.Size(334, 21);
            this.txtnamespace.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "命名空间";
            // 
            // FrmEntityMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 431);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "FrmEntityMaker";
            this.Text = "实体代码生成工具";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox ckAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DbcontionStr;
        private System.Windows.Forms.Button btnGetTableList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridView gridTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtnamespace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGeneration;
    }
}

