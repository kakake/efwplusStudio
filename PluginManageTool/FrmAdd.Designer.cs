namespace PluginManageTool
{
    partial class FrmAdd
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("插件项目");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("其他项目");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("应用程序", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdd));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("模板类", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "解决方案",
            "1",
            "2",
            "3"}, 4);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("模板类", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("模板项目", "proj.png");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("模板文件", "template.png");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("文件文件", "text.png");
            this.tbEntityDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbTreeView = new System.Windows.Forms.Label();
            this.treeViewClass = new System.Windows.Forms.TreeView();
            this.btnCommit = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.listViewFile = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.tbEntityName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbEntityDescription
            // 
            this.tbEntityDescription.BackColor = System.Drawing.Color.White;
            this.tbEntityDescription.ForeColor = System.Drawing.Color.Black;
            this.tbEntityDescription.Location = new System.Drawing.Point(6, 270);
            this.tbEntityDescription.Name = "tbEntityDescription";
            this.tbEntityDescription.ReadOnly = true;
            this.tbEntityDescription.Size = new System.Drawing.Size(642, 21);
            this.tbEntityDescription.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(202, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "模板(&T):";
            // 
            // lbTreeView
            // 
            this.lbTreeView.AutoSize = true;
            this.lbTreeView.BackColor = System.Drawing.Color.White;
            this.lbTreeView.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTreeView.ForeColor = System.Drawing.Color.Black;
            this.lbTreeView.Location = new System.Drawing.Point(9, 8);
            this.lbTreeView.Name = "lbTreeView";
            this.lbTreeView.Size = new System.Drawing.Size(51, 17);
            this.lbTreeView.TabIndex = 14;
            this.lbTreeView.Text = "类别(&C):";
            // 
            // treeViewClass
            // 
            this.treeViewClass.BackColor = System.Drawing.Color.White;
            this.treeViewClass.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeViewClass.ForeColor = System.Drawing.Color.Black;
            this.treeViewClass.HideSelection = false;
            this.treeViewClass.Location = new System.Drawing.Point(6, 28);
            this.treeViewClass.Name = "treeViewClass";
            treeNode1.Name = "节点0";
            treeNode1.Tag = "PluginTemplateProject";
            treeNode1.Text = "插件项目";
            treeNode2.Name = "节点1";
            treeNode2.Text = "其他项目";
            treeNode3.Name = "新增项";
            treeNode3.Text = "应用程序";
            this.treeViewClass.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.treeViewClass.Size = new System.Drawing.Size(193, 225);
            this.treeViewClass.TabIndex = 13;
            this.treeViewClass.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewClass_AfterSelect);
            // 
            // btnCommit
            // 
            this.btnCommit.BackColor = System.Drawing.Color.White;
            this.btnCommit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCommit.ForeColor = System.Drawing.Color.Black;
            this.btnCommit.Location = new System.Drawing.Point(482, 350);
            this.btnCommit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(80, 30);
            this.btnCommit.TabIndex = 10;
            this.btnCommit.Text = "添加(&A)";
            this.btnCommit.UseVisualStyleBackColor = false;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 311);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "名称(&N):";
            // 
            // listViewFile
            // 
            this.listViewFile.BackColor = System.Drawing.Color.White;
            this.listViewFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewFile.ForeColor = System.Drawing.Color.Black;
            this.listViewFile.GridLines = true;
            listViewGroup1.Header = "模板类";
            listViewGroup1.Name = "templateGroup";
            this.listViewFile.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.listViewFile.HideSelection = false;
            listViewGroup2.Header = "模板类";
            listViewGroup2.Name = "templateGroup";
            listViewItem1.Group = listViewGroup2;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Group = listViewGroup2;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.Group = listViewGroup2;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.Group = listViewGroup2;
            listViewItem4.StateImageIndex = 0;
            this.listViewFile.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.listViewFile.LargeImageList = this.imageList1;
            this.listViewFile.Location = new System.Drawing.Point(205, 28);
            this.listViewFile.MultiSelect = false;
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.Size = new System.Drawing.Size(443, 225);
            this.listViewFile.SmallImageList = this.imageList1;
            this.listViewFile.TabIndex = 12;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewFile_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "模板列表";
            this.columnHeader1.Width = 120;
            // 
            // tbEntityName
            // 
            this.tbEntityName.BackColor = System.Drawing.Color.White;
            this.tbEntityName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbEntityName.ForeColor = System.Drawing.Color.Black;
            this.tbEntityName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbEntityName.Location = new System.Drawing.Point(110, 308);
            this.tbEntityName.Name = "tbEntityName";
            this.tbEntityName.Size = new System.Drawing.Size(538, 23);
            this.tbEntityName.TabIndex = 9;
            this.tbEntityName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbEntityName_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(568, 350);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 390);
            this.Controls.Add(this.tbEntityDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbTreeView);
            this.Controls.Add(this.treeViewClass);
            this.Controls.Add(this.btnCommit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewFile);
            this.Controls.Add(this.tbEntityName);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAdd";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加新项";
            this.Load += new System.EventHandler(this.FrmAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbEntityDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbTreeView;
        private System.Windows.Forms.TreeView treeViewClass;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewFile;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        public System.Windows.Forms.TextBox tbEntityName;
        private System.Windows.Forms.Button btnCancel;
    }
}