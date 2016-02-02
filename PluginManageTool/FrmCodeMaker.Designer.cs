namespace PluginManageTool
{
    partial class FrmCodeMaker
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("批量生成实体", 5, 5);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Entity", 5, 5);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Dao", 5, 5);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("ObjectModel", 5, 5);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("WebServices", 5, 5);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Aspx和JS", 13, 13);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("WebController", 5, 5);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("WebUI", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("ViewForm和IView", 1, 1);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("WinController", 5, 5);
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("WinForm", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("ViewForm和IView", 1, 1);
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("WcfClientController", 5, 5);
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("WcfServerController", 5, 5);
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("WcfForm", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("代码文件", 2, 2, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode8,
            treeNode11,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("登录界面代码", 13, 13);
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("主界面代码", 13, 13);
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("代码段", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCodeMaker));
            this.treeCodeType = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btncodesave = new System.Windows.Forms.Button();
            this.txtcodepath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeCodeType
            // 
            this.treeCodeType.BackColor = System.Drawing.Color.White;
            this.treeCodeType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCodeType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeCodeType.ForeColor = System.Drawing.Color.Black;
            this.treeCodeType.HideSelection = false;
            this.treeCodeType.ImageIndex = 6;
            this.treeCodeType.ImageList = this.imageList1;
            this.treeCodeType.Location = new System.Drawing.Point(0, 0);
            this.treeCodeType.Name = "treeCodeType";
            treeNode1.ImageIndex = 5;
            treeNode1.Name = "节点1";
            treeNode1.SelectedImageIndex = 5;
            treeNode1.Tag = "FrmEntityMaker|{PluginName}\\Entity|0";
            treeNode1.Text = "批量生成实体";
            treeNode2.ImageIndex = 5;
            treeNode2.Name = "节点2";
            treeNode2.SelectedImageIndex = 5;
            treeNode2.Tag = "FrmCodeEditor|{PluginName}\\Entity|Entity";
            treeNode2.Text = "Entity";
            treeNode3.ImageIndex = 5;
            treeNode3.Name = "节点3";
            treeNode3.SelectedImageIndex = 5;
            treeNode3.Tag = "FrmCodeEditor|{PluginName}\\Dao|Dao";
            treeNode3.Text = "Dao";
            treeNode4.ImageIndex = 5;
            treeNode4.Name = "节点4";
            treeNode4.SelectedImageIndex = 5;
            treeNode4.Tag = "FrmCodeEditor|{PluginName}\\ObjectModel|ObjectModel";
            treeNode4.Text = "ObjectModel";
            treeNode5.ImageIndex = 5;
            treeNode5.Name = "节点6";
            treeNode5.SelectedImageIndex = 5;
            treeNode5.Tag = "FrmCodeEditor|{PluginName}\\WebServices|WebServices";
            treeNode5.Text = "WebServices";
            treeNode6.ImageIndex = 13;
            treeNode6.Name = "节点7";
            treeNode6.SelectedImageIndex = 13;
            treeNode6.Tag = "FrmCodeEditor|Aspx;JS|AspxJS";
            treeNode6.Text = "Aspx和JS";
            treeNode7.ImageIndex = 5;
            treeNode7.Name = "节点9";
            treeNode7.SelectedImageIndex = 5;
            treeNode7.Tag = "FrmCodeEditor|{PluginName}\\WebController|WebController";
            treeNode7.Text = "WebController";
            treeNode8.Name = "节点5";
            treeNode8.Text = "WebUI";
            treeNode9.ImageIndex = 1;
            treeNode9.Name = "节点15";
            treeNode9.SelectedImageIndex = 1;
            treeNode9.Tag = "FrmCodeEditor|{PluginName}.Winform\\ViewForm;{PluginName}.Winform\\ViewForm;{Plugin" +
                "Name}.Winform\\IView|WinViewForm";
            treeNode9.Text = "ViewForm和IView";
            treeNode10.ImageIndex = 5;
            treeNode10.Name = "节点17";
            treeNode10.SelectedImageIndex = 5;
            treeNode10.Tag = "FrmCodeEditor|{PluginName}.Winform\\Controller|WinController";
            treeNode10.Text = "WinController";
            treeNode11.Name = "节点11";
            treeNode11.Text = "WinForm";
            treeNode12.ImageIndex = 1;
            treeNode12.Name = "节点19";
            treeNode12.SelectedImageIndex = 1;
            treeNode12.Tag = "FrmCodeEditor|{PluginName}.Winform\\ViewForm;{PluginName}.Winform\\ViewForm;{Plugin" +
                "Name}.Winform\\IView|WcfViewForm";
            treeNode12.Text = "ViewForm和IView";
            treeNode13.ImageIndex = 5;
            treeNode13.Name = "节点21";
            treeNode13.SelectedImageIndex = 5;
            treeNode13.Tag = "FrmCodeEditor|{PluginName}.Winform\\Controller|WcfClientController";
            treeNode13.Text = "WcfClientController";
            treeNode14.ImageIndex = 5;
            treeNode14.Name = "节点22";
            treeNode14.SelectedImageIndex = 5;
            treeNode14.Tag = "FrmCodeEditor|{PluginName}\\WcfController|WcfServerController";
            treeNode14.Text = "WcfServerController";
            treeNode15.Name = "节点12";
            treeNode15.Text = "WcfForm";
            treeNode16.ImageIndex = 2;
            treeNode16.Name = "节点0";
            treeNode16.SelectedImageIndex = 2;
            treeNode16.Text = "代码文件";
            treeNode17.ImageIndex = 13;
            treeNode17.Name = "节点24";
            treeNode17.SelectedImageIndex = 13;
            treeNode17.Text = "登录界面代码";
            treeNode18.ImageIndex = 13;
            treeNode18.Name = "节点25";
            treeNode18.SelectedImageIndex = 13;
            treeNode18.Text = "主界面代码";
            treeNode19.Name = "节点23";
            treeNode19.Text = "代码段";
            this.treeCodeType.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode19});
            this.treeCodeType.SelectedImageIndex = 6;
            this.treeCodeType.Size = new System.Drawing.Size(139, 485);
            this.treeCodeType.TabIndex = 0;
            this.treeCodeType.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCodeType_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "db.png");
            this.imageList1.Images.SetKeyName(1, "form.png");
            this.imageList1.Images.SetKeyName(2, "proj.png");
            this.imageList1.Images.SetKeyName(3, "server.png");
            this.imageList1.Images.SetKeyName(4, "solution.png");
            this.imageList1.Images.SetKeyName(5, "template.png");
            this.imageList1.Images.SetKeyName(6, "text.png");
            this.imageList1.Images.SetKeyName(7, "Web_GlobalAppClass.ico");
            this.imageList1.Images.SetKeyName(8, "Utility_VBScript.ico");
            this.imageList1.Images.SetKeyName(9, "Web_WebConfig.ico");
            this.imageList1.Images.SetKeyName(10, "XMLFileHS.png");
            this.imageList1.Images.SetKeyName(11, "PublishPlanHS.png");
            this.imageList1.Images.SetKeyName(12, "Code_WebService.png");
            this.imageList1.Images.SetKeyName(13, "Web_HTML.ico");
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.ForeColor = System.Drawing.Color.Black;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeCodeType);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.Color.Black;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel2.Controls.Add(this.panelMain);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.Black;
            this.splitContainer1.Size = new System.Drawing.Size(574, 485);
            this.splitContainer1.SplitterDistance = 139;
            this.splitContainer1.TabIndex = 1;
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.ForeColor = System.Drawing.Color.Black;
            this.panelMain.Location = new System.Drawing.Point(0, 43);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(431, 442);
            this.panelMain.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btncodesave);
            this.panel1.Controls.Add(this.txtcodepath);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(431, 43);
            this.panel1.TabIndex = 0;
            // 
            // btncodesave
            // 
            this.btncodesave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btncodesave.ForeColor = System.Drawing.Color.Black;
            this.btncodesave.Location = new System.Drawing.Point(350, 8);
            this.btncodesave.Name = "btncodesave";
            this.btncodesave.Size = new System.Drawing.Size(75, 23);
            this.btncodesave.TabIndex = 2;
            this.btncodesave.Text = "保存";
            this.btncodesave.UseVisualStyleBackColor = true;
            this.btncodesave.Click += new System.EventHandler(this.btncodesave_Click);
            // 
            // txtcodepath
            // 
            this.txtcodepath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcodepath.BackColor = System.Drawing.Color.White;
            this.txtcodepath.Enabled = false;
            this.txtcodepath.ForeColor = System.Drawing.Color.Black;
            this.txtcodepath.Location = new System.Drawing.Point(63, 10);
            this.txtcodepath.Name = "txtcodepath";
            this.txtcodepath.Size = new System.Drawing.Size(281, 21);
            this.txtcodepath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "存储目录";
            // 
            // FrmCodeMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 485);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "FrmCodeMaker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "代码生成";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeCodeType;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btncodesave;
        private System.Windows.Forms.TextBox txtcodepath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
    }
}