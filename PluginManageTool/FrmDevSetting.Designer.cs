namespace PluginManageTool
{
    partial class FrmDevSetting
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClose1 = new System.Windows.Forms.Button();
            this.btnSave1 = new System.Windows.Forms.Button();
            this.txtStartPage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtwcfendpoint = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtcname = new System.Windows.Forms.TextBox();
            this.cbpname = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbwcfclient = new System.Windows.Forms.RadioButton();
            this.rbwinform = new System.Windows.Forms.RadioButton();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ForeColor = System.Drawing.Color.Black;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(457, 400);
            this.tabControl.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtPort);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.btnClose1);
            this.tabPage1.Controls.Add(this.btnSave1);
            this.tabPage1.Controls.Add(this.txtStartPage);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(449, 374);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Web平台设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.Color.White;
            this.txtPort.ForeColor = System.Drawing.Color.Black;
            this.txtPort.Location = new System.Drawing.Point(8, 82);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 21);
            this.txtPort.TabIndex = 11;
            this.txtPort.Text = "888";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "端口";
            // 
            // btnClose1
            // 
            this.btnClose1.ForeColor = System.Drawing.Color.Black;
            this.btnClose1.Location = new System.Drawing.Point(341, 329);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(75, 23);
            this.btnClose1.TabIndex = 9;
            this.btnClose1.Text = "关闭";
            this.btnClose1.UseVisualStyleBackColor = true;
            this.btnClose1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave1
            // 
            this.btnSave1.ForeColor = System.Drawing.Color.Black;
            this.btnSave1.Location = new System.Drawing.Point(245, 329);
            this.btnSave1.Name = "btnSave1";
            this.btnSave1.Size = new System.Drawing.Size(75, 23);
            this.btnSave1.TabIndex = 8;
            this.btnSave1.Text = "保存";
            this.btnSave1.UseVisualStyleBackColor = true;
            this.btnSave1.Click += new System.EventHandler(this.btnSave1_Click);
            // 
            // txtStartPage
            // 
            this.txtStartPage.BackColor = System.Drawing.Color.White;
            this.txtStartPage.ForeColor = System.Drawing.Color.Black;
            this.txtStartPage.Location = new System.Drawing.Point(8, 32);
            this.txtStartPage.Name = "txtStartPage";
            this.txtStartPage.Size = new System.Drawing.Size(372, 21);
            this.txtStartPage.TabIndex = 7;
            this.txtStartPage.Text = "index.html";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "设置启动页面";
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.btnClose);
            this.tabPage2.Controls.Add(this.btnSave);
            this.tabPage2.Controls.Add(this.txtwcfendpoint);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.rbwcfclient);
            this.tabPage2.Controls.Add(this.rbwinform);
            this.tabPage2.ForeColor = System.Drawing.Color.Black;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(449, 374);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Winform平台设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(341, 329);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(245, 329);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtwcfendpoint
            // 
            this.txtwcfendpoint.BackColor = System.Drawing.Color.White;
            this.txtwcfendpoint.ForeColor = System.Drawing.Color.Black;
            this.txtwcfendpoint.Location = new System.Drawing.Point(13, 156);
            this.txtwcfendpoint.Name = "txtwcfendpoint";
            this.txtwcfendpoint.Size = new System.Drawing.Size(372, 21);
            this.txtwcfendpoint.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(11, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "WCFClient服务地址";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtcname);
            this.groupBox1.Controls.Add(this.cbpname);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(8, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 80);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "启动插件";
            // 
            // txtcname
            // 
            this.txtcname.BackColor = System.Drawing.Color.White;
            this.txtcname.ForeColor = System.Drawing.Color.Black;
            this.txtcname.Location = new System.Drawing.Point(110, 46);
            this.txtcname.Name = "txtcname";
            this.txtcname.Size = new System.Drawing.Size(267, 21);
            this.txtcname.TabIndex = 3;
            // 
            // cbpname
            // 
            this.cbpname.BackColor = System.Drawing.Color.White;
            this.cbpname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbpname.ForeColor = System.Drawing.Color.Black;
            this.cbpname.FormattingEnabled = true;
            this.cbpname.Location = new System.Drawing.Point(110, 20);
            this.cbpname.Name = "cbpname";
            this.cbpname.Size = new System.Drawing.Size(267, 20);
            this.cbpname.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(18, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "控制器名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(18, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "插件名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "客户端启动类型";
            // 
            // rbwcfclient
            // 
            this.rbwcfclient.AutoSize = true;
            this.rbwcfclient.ForeColor = System.Drawing.Color.Black;
            this.rbwcfclient.Location = new System.Drawing.Point(189, 20);
            this.rbwcfclient.Name = "rbwcfclient";
            this.rbwcfclient.Size = new System.Drawing.Size(77, 16);
            this.rbwcfclient.TabIndex = 1;
            this.rbwcfclient.Text = "WCFClient";
            this.rbwcfclient.UseVisualStyleBackColor = true;
            // 
            // rbwinform
            // 
            this.rbwinform.AutoSize = true;
            this.rbwinform.Checked = true;
            this.rbwinform.ForeColor = System.Drawing.Color.Black;
            this.rbwinform.Location = new System.Drawing.Point(118, 20);
            this.rbwinform.Name = "rbwinform";
            this.rbwinform.Size = new System.Drawing.Size(65, 16);
            this.rbwinform.TabIndex = 0;
            this.rbwinform.TabStop = true;
            this.rbwinform.Text = "Winform";
            this.rbwinform.UseVisualStyleBackColor = true;
            this.rbwinform.CheckedChanged += new System.EventHandler(this.rbwinform_CheckedChanged);
            // 
            // FrmDevSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 400);
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDevSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "运行插件设置";
            this.Load += new System.EventHandler(this.FrmDevSetting_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RadioButton rbwcfclient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbpname;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtwcfendpoint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbwinform;
        private System.Windows.Forms.TextBox txtcname;
        private System.Windows.Forms.TextBox txtStartPage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClose1;
        private System.Windows.Forms.Button btnSave1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label6;
    }
}