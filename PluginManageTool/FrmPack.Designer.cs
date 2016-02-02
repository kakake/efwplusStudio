namespace PluginManageTool
{
    partial class FrmPack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPack));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.txtname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txttitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtauthor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtversion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtpluginsize = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtupdatedate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtintroduction = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtupdaterecord = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtdownloadpath = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pb_headpic = new System.Windows.Forms.PictureBox();
            this.btnheadpic = new System.Windows.Forms.Button();
            this.btnpackup = new System.Windows.Forms.Button();
            this.btnissue = new System.Windows.Forms.Button();
            this.txtStartItem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cksrc = new System.Windows.Forms.CheckBox();
            this.txtsrc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnsrc = new System.Windows.Forms.Button();
            this.btndocs = new System.Windows.Forms.Button();
            this.txtdocs = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_headpic)).BeginInit();
            this.SuspendLayout();
            // 
            // txtname
            // 
            this.txtname.BackColor = System.Drawing.Color.White;
            this.txtname.Enabled = false;
            this.txtname.ForeColor = System.Drawing.Color.Black;
            this.txtname.Location = new System.Drawing.Point(106, 12);
            this.txtname.Name = "txtname";
            this.txtname.ReadOnly = true;
            this.txtname.Size = new System.Drawing.Size(267, 21);
            this.txtname.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(38, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "插件名称：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txttitle
            // 
            this.txttitle.BackColor = System.Drawing.Color.White;
            this.txttitle.ForeColor = System.Drawing.Color.Black;
            this.txttitle.Location = new System.Drawing.Point(106, 39);
            this.txttitle.Name = "txttitle";
            this.txttitle.Size = new System.Drawing.Size(267, 21);
            this.txttitle.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(38, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "别名：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtauthor
            // 
            this.txtauthor.BackColor = System.Drawing.Color.White;
            this.txtauthor.ForeColor = System.Drawing.Color.Black;
            this.txtauthor.Location = new System.Drawing.Point(106, 66);
            this.txtauthor.Name = "txtauthor";
            this.txtauthor.Size = new System.Drawing.Size(267, 21);
            this.txtauthor.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(38, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "作者：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtversion
            // 
            this.txtversion.BackColor = System.Drawing.Color.White;
            this.txtversion.ForeColor = System.Drawing.Color.Black;
            this.txtversion.Location = new System.Drawing.Point(106, 93);
            this.txtversion.Name = "txtversion";
            this.txtversion.Size = new System.Drawing.Size(267, 21);
            this.txtversion.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(38, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "版本：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtpluginsize
            // 
            this.txtpluginsize.BackColor = System.Drawing.Color.White;
            this.txtpluginsize.Enabled = false;
            this.txtpluginsize.ForeColor = System.Drawing.Color.Black;
            this.txtpluginsize.Location = new System.Drawing.Point(106, 120);
            this.txtpluginsize.Name = "txtpluginsize";
            this.txtpluginsize.ReadOnly = true;
            this.txtpluginsize.Size = new System.Drawing.Size(267, 21);
            this.txtpluginsize.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(38, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "大小：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtupdatedate
            // 
            this.txtupdatedate.BackColor = System.Drawing.Color.White;
            this.txtupdatedate.Enabled = false;
            this.txtupdatedate.ForeColor = System.Drawing.Color.Black;
            this.txtupdatedate.Location = new System.Drawing.Point(106, 147);
            this.txtupdatedate.Name = "txtupdatedate";
            this.txtupdatedate.ReadOnly = true;
            this.txtupdatedate.Size = new System.Drawing.Size(267, 21);
            this.txtupdatedate.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(38, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "更新日期：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtintroduction
            // 
            this.txtintroduction.BackColor = System.Drawing.Color.White;
            this.txtintroduction.ForeColor = System.Drawing.Color.Black;
            this.txtintroduction.Location = new System.Drawing.Point(106, 202);
            this.txtintroduction.Multiline = true;
            this.txtintroduction.Name = "txtintroduction";
            this.txtintroduction.Size = new System.Drawing.Size(397, 55);
            this.txtintroduction.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(38, 205);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "插件介绍：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtupdaterecord
            // 
            this.txtupdaterecord.BackColor = System.Drawing.Color.White;
            this.txtupdaterecord.ForeColor = System.Drawing.Color.Black;
            this.txtupdaterecord.Location = new System.Drawing.Point(106, 263);
            this.txtupdaterecord.Multiline = true;
            this.txtupdaterecord.Name = "txtupdaterecord";
            this.txtupdaterecord.Size = new System.Drawing.Size(397, 55);
            this.txtupdaterecord.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(38, 266);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "更新内容：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtdownloadpath
            // 
            this.txtdownloadpath.BackColor = System.Drawing.Color.White;
            this.txtdownloadpath.Enabled = false;
            this.txtdownloadpath.ForeColor = System.Drawing.Color.Black;
            this.txtdownloadpath.Location = new System.Drawing.Point(106, 324);
            this.txtdownloadpath.Name = "txtdownloadpath";
            this.txtdownloadpath.ReadOnly = true;
            this.txtdownloadpath.Size = new System.Drawing.Size(267, 21);
            this.txtdownloadpath.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(38, 327);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "插件包：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pb_headpic
            // 
            this.pb_headpic.BackColor = System.Drawing.Color.White;
            this.pb_headpic.ForeColor = System.Drawing.Color.Black;
            this.pb_headpic.Image = ((System.Drawing.Image)(resources.GetObject("pb_headpic.Image")));
            this.pb_headpic.Location = new System.Drawing.Point(379, 12);
            this.pb_headpic.Name = "pb_headpic";
            this.pb_headpic.Size = new System.Drawing.Size(124, 129);
            this.pb_headpic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_headpic.TabIndex = 24;
            this.pb_headpic.TabStop = false;
            // 
            // btnheadpic
            // 
            this.btnheadpic.BackColor = System.Drawing.Color.White;
            this.btnheadpic.ForeColor = System.Drawing.Color.Black;
            this.btnheadpic.Location = new System.Drawing.Point(379, 145);
            this.btnheadpic.Name = "btnheadpic";
            this.btnheadpic.Size = new System.Drawing.Size(124, 23);
            this.btnheadpic.TabIndex = 25;
            this.btnheadpic.Text = "上传头像";
            this.btnheadpic.UseVisualStyleBackColor = false;
            // 
            // btnpackup
            // 
            this.btnpackup.BackColor = System.Drawing.Color.White;
            this.btnpackup.ForeColor = System.Drawing.Color.Black;
            this.btnpackup.Location = new System.Drawing.Point(379, 322);
            this.btnpackup.Name = "btnpackup";
            this.btnpackup.Size = new System.Drawing.Size(124, 23);
            this.btnpackup.TabIndex = 26;
            this.btnpackup.Text = "插件打包上传";
            this.btnpackup.UseVisualStyleBackColor = false;
            this.btnpackup.Click += new System.EventHandler(this.btnpackup_Click);
            // 
            // btnissue
            // 
            this.btnissue.BackColor = System.Drawing.Color.White;
            this.btnissue.ForeColor = System.Drawing.Color.Black;
            this.btnissue.Location = new System.Drawing.Point(106, 463);
            this.btnissue.Name = "btnissue";
            this.btnissue.Size = new System.Drawing.Size(105, 23);
            this.btnissue.TabIndex = 27;
            this.btnissue.Text = "确认发布";
            this.btnissue.UseVisualStyleBackColor = false;
            this.btnissue.Click += new System.EventHandler(this.btnissue_Click);
            // 
            // txtStartItem
            // 
            this.txtStartItem.BackColor = System.Drawing.Color.White;
            this.txtStartItem.ForeColor = System.Drawing.Color.Black;
            this.txtStartItem.Location = new System.Drawing.Point(106, 174);
            this.txtStartItem.Name = "txtStartItem";
            this.txtStartItem.Size = new System.Drawing.Size(267, 21);
            this.txtStartItem.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(38, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 28;
            this.label1.Text = "启动项：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cksrc
            // 
            this.cksrc.AutoSize = true;
            this.cksrc.BackColor = System.Drawing.Color.White;
            this.cksrc.ForeColor = System.Drawing.Color.Black;
            this.cksrc.Location = new System.Drawing.Point(40, 363);
            this.cksrc.Name = "cksrc";
            this.cksrc.Size = new System.Drawing.Size(144, 16);
            this.cksrc.TabIndex = 30;
            this.cksrc.Text = "发布源代码与设计文档";
            this.cksrc.UseVisualStyleBackColor = false;
            this.cksrc.CheckedChanged += new System.EventHandler(this.cksrc_CheckedChanged);
            // 
            // txtsrc
            // 
            this.txtsrc.BackColor = System.Drawing.Color.White;
            this.txtsrc.Enabled = false;
            this.txtsrc.ForeColor = System.Drawing.Color.Black;
            this.txtsrc.Location = new System.Drawing.Point(106, 385);
            this.txtsrc.Name = "txtsrc";
            this.txtsrc.ReadOnly = true;
            this.txtsrc.Size = new System.Drawing.Size(267, 21);
            this.txtsrc.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(38, 388);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 31;
            this.label2.Text = "源代码：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnsrc
            // 
            this.btnsrc.BackColor = System.Drawing.Color.White;
            this.btnsrc.ForeColor = System.Drawing.Color.Black;
            this.btnsrc.Location = new System.Drawing.Point(379, 383);
            this.btnsrc.Name = "btnsrc";
            this.btnsrc.Size = new System.Drawing.Size(124, 23);
            this.btnsrc.TabIndex = 33;
            this.btnsrc.Text = "源代码打包";
            this.btnsrc.UseVisualStyleBackColor = false;
            this.btnsrc.Click += new System.EventHandler(this.btnsrc_Click);
            // 
            // btndocs
            // 
            this.btndocs.BackColor = System.Drawing.Color.White;
            this.btndocs.ForeColor = System.Drawing.Color.Black;
            this.btndocs.Location = new System.Drawing.Point(379, 412);
            this.btndocs.Name = "btndocs";
            this.btndocs.Size = new System.Drawing.Size(124, 23);
            this.btndocs.TabIndex = 36;
            this.btndocs.Text = "打开文档";
            this.btndocs.UseVisualStyleBackColor = false;
            this.btndocs.Click += new System.EventHandler(this.btndocs_Click);
            // 
            // txtdocs
            // 
            this.txtdocs.BackColor = System.Drawing.Color.White;
            this.txtdocs.Enabled = false;
            this.txtdocs.ForeColor = System.Drawing.Color.Black;
            this.txtdocs.Location = new System.Drawing.Point(106, 414);
            this.txtdocs.Name = "txtdocs";
            this.txtdocs.ReadOnly = true;
            this.txtdocs.Size = new System.Drawing.Size(267, 21);
            this.txtdocs.TabIndex = 35;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(38, 417);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 34;
            this.label12.Text = "设计文档：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 487);
            this.Controls.Add(this.btndocs);
            this.Controls.Add(this.txtdocs);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnsrc);
            this.Controls.Add(this.txtsrc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cksrc);
            this.Controls.Add(this.txtStartItem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnissue);
            this.Controls.Add(this.btnpackup);
            this.Controls.Add(this.btnheadpic);
            this.Controls.Add(this.pb_headpic);
            this.Controls.Add(this.txtdownloadpath);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtupdaterecord);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtintroduction);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtupdatedate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtpluginsize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtversion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtauthor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txttitle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.label3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPack";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "插件发布";
            this.Load += new System.EventHandler(this.FrmPack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_headpic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txttitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtauthor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtversion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtpluginsize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtupdatedate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtintroduction;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtupdaterecord;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtdownloadpath;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pb_headpic;
        private System.Windows.Forms.Button btnheadpic;
        private System.Windows.Forms.Button btnpackup;
        private System.Windows.Forms.Button btnissue;
        private System.Windows.Forms.TextBox txtStartItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cksrc;
        private System.Windows.Forms.TextBox txtsrc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnsrc;
        private System.Windows.Forms.Button btndocs;
        private System.Windows.Forms.TextBox txtdocs;
        private System.Windows.Forms.Label label12;
    }
}