using DevComponents.DotNetBar.Metro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PluginManageTool
{
    public partial class frmprogress : MetroForm
    {
        public frmprogress()
        {
            InitializeComponent();
        }

        delegate void updowndelegate(string name, params object[] val);
        public void refreshControl(string name, params object[] val)
        {
            
            if ("progressBar_1" == name)
            {
                if (this.progressBar1.InvokeRequired)
                {
                    updowndelegate ud = new updowndelegate(refreshControl);
                    this.Invoke(ud, name, val);
                }
                else
                {
                    progressBar1.Maximum = 100;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 0;
                }
            }
            else if ("progressBar_2" == name)
            {
                if (this.progressBar1.InvokeRequired)
                {
                    updowndelegate ud = new updowndelegate(refreshControl);
                    this.Invoke(ud, name, val);
                }
                else
                {
                    progressBar1.Value = Convert.ToInt32(val[0]);
                }
            }
            else if ("lblTime" == name)
            {
                if (this.lblTime.InvokeRequired)
                {
                    updowndelegate ud = new updowndelegate(refreshControl);
                    this.Invoke(ud, name, val);
                }
                else
                {
                    lblTime.Visible = true;
                   
                    lblTime.Text = val[0].ToString();
                }

            }
            else if ("lblSpeed" == name)
            {
                if (this.lblSpeed.InvokeRequired)
                {
                    updowndelegate ud = new updowndelegate(refreshControl);
                    this.Invoke(ud, name, val);
                }
                else
                {
                    lblSpeed.Visible = true;
                  
                    lblSpeed.Text = val[0].ToString();
                }


            }
            else if ("lblState" == name)
            {
                if (this.lblState.InvokeRequired)
                {
                    updowndelegate ud = new updowndelegate(refreshControl);
                    this.Invoke(ud, name, val);
                }
                else
                {
                   
                    lblState.Visible = true;
                    lblState.Text = val[0].ToString();
                }

            }
            else if ("lblSize" == name)
            {
                if (this.lblSize.InvokeRequired)
                {
                    updowndelegate ud = new updowndelegate(refreshControl);
                    this.Invoke(ud, name, val);
                }
                else
                {
                    lblSize.Visible = true;
                    lblSize.Text = val[0].ToString();
                }

            }
            else if ("btncancel" == name)
            {
                if (this.btncancelup.InvokeRequired)
                {
                    updowndelegate ud = new updowndelegate(refreshControl);
                    this.Invoke(ud, name, val);
                }
                else
                {
                    btncancelup.Enabled = false;
                }
            }

        }
    }
}
