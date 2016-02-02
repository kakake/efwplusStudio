namespace PluginManageTool
{
    partial class FrmPackSrc
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnzip = new System.Windows.Forms.Button();
            this.treeSrc = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel1.Controls.Add(this.treeSrc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 173);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.panel2.Controls.Add(this.btnzip);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 173);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(324, 52);
            this.panel2.TabIndex = 1;
            // 
            // btnzip
            // 
            this.btnzip.BackColor = System.Drawing.Color.White;
            this.btnzip.ForeColor = System.Drawing.Color.Black;
            this.btnzip.Location = new System.Drawing.Point(223, 17);
            this.btnzip.Name = "btnzip";
            this.btnzip.Size = new System.Drawing.Size(75, 23);
            this.btnzip.TabIndex = 0;
            this.btnzip.Text = "确定";
            this.btnzip.UseVisualStyleBackColor = false;
            this.btnzip.Click += new System.EventHandler(this.btnzip_Click);
            // 
            // treeSrc
            // 
            this.treeSrc.BackColor = System.Drawing.Color.White;
            this.treeSrc.CheckBoxes = true;
            this.treeSrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSrc.ForeColor = System.Drawing.Color.Black;
            this.treeSrc.Location = new System.Drawing.Point(0, 0);
            this.treeSrc.Name = "treeSrc";
            this.treeSrc.Size = new System.Drawing.Size(324, 173);
            this.treeSrc.TabIndex = 0;
            this.treeSrc.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeSrc_AfterCheck);
            // 
            // FrmPackSrc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 225);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPackSrc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "源代码打包";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView treeSrc;
        private System.Windows.Forms.Button btnzip;
    }
}