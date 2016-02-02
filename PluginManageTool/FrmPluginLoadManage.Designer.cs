namespace PluginManageTool
{
    partial class FrmPluginLoadManage
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Web插件");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Winform插件");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Wcf插件");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treePlugin = new System.Windows.Forms.TreeView();
            this.gridpluginmenu = new System.Windows.Forms.DataGridView();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.btnSetstartitem = new System.Windows.Forms.Button();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMenuPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtViewName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtControllerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPluginName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMenuName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textEditorControl = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textEditorControl1 = new ICSharpCode.TextEditor.TextEditorControl();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridpluginmenu)).BeginInit();
            this.expandablePanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.ForeColor = System.Drawing.Color.Black;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treePlugin);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.Color.Black;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridpluginmenu);
            this.splitContainer1.Panel2.Controls.Add(this.expandablePanel1);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.Black;
            this.splitContainer1.Size = new System.Drawing.Size(670, 430);
            this.splitContainer1.SplitterDistance = 223;
            this.splitContainer1.TabIndex = 0;
            // 
            // treePlugin
            // 
            this.treePlugin.BackColor = System.Drawing.Color.White;
            this.treePlugin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePlugin.ForeColor = System.Drawing.Color.Black;
            this.treePlugin.HideSelection = false;
            this.treePlugin.Location = new System.Drawing.Point(0, 0);
            this.treePlugin.Name = "treePlugin";
            treeNode1.Name = "节点0";
            treeNode1.Text = "Web插件";
            treeNode2.Name = "节点1";
            treeNode2.Text = "Winform插件";
            treeNode3.Name = "节点2";
            treeNode3.Text = "Wcf插件";
            this.treePlugin.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treePlugin.Size = new System.Drawing.Size(223, 430);
            this.treePlugin.TabIndex = 0;
            this.treePlugin.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePlugin_AfterSelect);
            // 
            // gridpluginmenu
            // 
            this.gridpluginmenu.AllowUserToAddRows = false;
            this.gridpluginmenu.AllowUserToDeleteRows = false;
            this.gridpluginmenu.AllowUserToResizeColumns = false;
            this.gridpluginmenu.AllowUserToResizeRows = false;
            this.gridpluginmenu.BackgroundColor = System.Drawing.Color.White;
            this.gridpluginmenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridpluginmenu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column1,
            this.Column4});
            this.gridpluginmenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridpluginmenu.Location = new System.Drawing.Point(0, 0);
            this.gridpluginmenu.MultiSelect = false;
            this.gridpluginmenu.Name = "gridpluginmenu";
            this.gridpluginmenu.ReadOnly = true;
            this.gridpluginmenu.RowHeadersVisible = false;
            this.gridpluginmenu.RowTemplate.Height = 23;
            this.gridpluginmenu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridpluginmenu.Size = new System.Drawing.Size(281, 430);
            this.gridpluginmenu.TabIndex = 1;
            this.gridpluginmenu.Click += new System.EventHandler(this.gridpluginmenu_Click);
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.LeftToRight;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.btnSetstartitem);
            this.expandablePanel1.Controls.Add(this.txtMemo);
            this.expandablePanel1.Controls.Add(this.label6);
            this.expandablePanel1.Controls.Add(this.txtMenuPath);
            this.expandablePanel1.Controls.Add(this.label5);
            this.expandablePanel1.Controls.Add(this.txtViewName);
            this.expandablePanel1.Controls.Add(this.label4);
            this.expandablePanel1.Controls.Add(this.txtControllerName);
            this.expandablePanel1.Controls.Add(this.label3);
            this.expandablePanel1.Controls.Add(this.txtPluginName);
            this.expandablePanel1.Controls.Add(this.label2);
            this.expandablePanel1.Controls.Add(this.txtMenuName);
            this.expandablePanel1.Controls.Add(this.label1);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.expandablePanel1.ExpandButtonAlignment = DevComponents.DotNetBar.eTitleButtonAlignment.Left;
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(281, 0);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(162, 430);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 0;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "菜单详细";
            // 
            // btnSetstartitem
            // 
            this.btnSetstartitem.BackColor = System.Drawing.Color.White;
            this.btnSetstartitem.ForeColor = System.Drawing.Color.Black;
            this.btnSetstartitem.Location = new System.Drawing.Point(9, 48);
            this.btnSetstartitem.Name = "btnSetstartitem";
            this.btnSetstartitem.Size = new System.Drawing.Size(148, 23);
            this.btnSetstartitem.TabIndex = 13;
            this.btnSetstartitem.Text = "设置为启动项";
            this.btnSetstartitem.UseVisualStyleBackColor = false;
            this.btnSetstartitem.Click += new System.EventHandler(this.btnSetstartitem_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemo.Location = new System.Drawing.Point(9, 350);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ReadOnly = true;
            this.txtMemo.Size = new System.Drawing.Size(150, 78);
            this.txtMemo.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(7, 331);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "说明";
            // 
            // txtMenuPath
            // 
            this.txtMenuPath.Location = new System.Drawing.Point(9, 303);
            this.txtMenuPath.Name = "txtMenuPath";
            this.txtMenuPath.ReadOnly = true;
            this.txtMenuPath.Size = new System.Drawing.Size(150, 21);
            this.txtMenuPath.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(7, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "菜单路径";
            // 
            // txtViewName
            // 
            this.txtViewName.Location = new System.Drawing.Point(9, 255);
            this.txtViewName.Name = "txtViewName";
            this.txtViewName.ReadOnly = true;
            this.txtViewName.Size = new System.Drawing.Size(150, 21);
            this.txtViewName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(7, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "启动界面";
            // 
            // txtControllerName
            // 
            this.txtControllerName.Location = new System.Drawing.Point(9, 207);
            this.txtControllerName.Name = "txtControllerName";
            this.txtControllerName.ReadOnly = true;
            this.txtControllerName.Size = new System.Drawing.Size(150, 21);
            this.txtControllerName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(7, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "启动控制器";
            // 
            // txtPluginName
            // 
            this.txtPluginName.Location = new System.Drawing.Point(9, 159);
            this.txtPluginName.Name = "txtPluginName";
            this.txtPluginName.ReadOnly = true;
            this.txtPluginName.Size = new System.Drawing.Size(150, 21);
            this.txtPluginName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(7, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "插件";
            // 
            // txtMenuName
            // 
            this.txtMenuName.Location = new System.Drawing.Point(9, 112);
            this.txtMenuName.Name = "txtMenuName";
            this.txtMenuName.ReadOnly = true;
            this.txtMenuName.Size = new System.Drawing.Size(150, 21);
            this.txtMenuName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(7, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "菜单名称";
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ForeColor = System.Drawing.Color.Black;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 462);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(676, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "插件管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textEditorControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(676, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "WebPlatform/pluginsys.xml";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textEditorControl
            // 
            this.textEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textEditorControl.IsReadOnly = false;
            this.textEditorControl.Location = new System.Drawing.Point(3, 3);
            this.textEditorControl.Name = "textEditorControl";
            this.textEditorControl.ShowVRuler = false;
            this.textEditorControl.Size = new System.Drawing.Size(670, 430);
            this.textEditorControl.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textEditorControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(676, 436);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "WinformPlatform/pluginsys.xml";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textEditorControl1
            // 
            this.textEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textEditorControl1.IsReadOnly = false;
            this.textEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.textEditorControl1.Name = "textEditorControl1";
            this.textEditorControl1.ShowVRuler = false;
            this.textEditorControl1.Size = new System.Drawing.Size(676, 436);
            this.textEditorControl1.TabIndex = 6;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "menuname";
            this.Column2.HeaderText = "菜单名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "controllername";
            this.Column3.HeaderText = "启动控制器";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 150;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "viewname";
            this.Column1.HeaderText = "启动界面";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "menupath";
            this.Column4.HeaderText = "菜单路径";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // FrmPluginLoadManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmPluginLoadManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "插件菜单";
            this.Load += new System.EventHandler(this.FrmPluginLoadManage_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridpluginmenu)).EndInit();
            this.expandablePanel1.ResumeLayout(false);
            this.expandablePanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treePlugin;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private System.Windows.Forms.DataGridView gridpluginmenu;
        private System.Windows.Forms.TextBox txtMenuPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtViewName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtControllerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPluginName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMenuName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSetstartitem;
        public ICSharpCode.TextEditor.TextEditorControl textEditorControl;
        private System.Windows.Forms.TabPage tabPage3;
        public ICSharpCode.TextEditor.TextEditorControl textEditorControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}