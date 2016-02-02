using DevComponents.DotNetBar.Metro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluginManageTool
{
    public partial class Loading : MetroForm
    {
        private EventHandler _handler;
        public Loading(EventHandler handler)
        {
            InitializeComponent();
            _handler = handler;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            this.timer2.Enabled = true;
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new Thread((ThreadStart)(delegate()
            {
                this.timer1.Enabled = false;
                if (_handler != null)
                    _handler.Invoke(null, null);
                
                this.timer2.Enabled = false;
                //this.Close();
                this.Invoke((MethodInvoker)delegate() { this.Close(); });
            })).Start();

           
        }

        private int count = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            labTime.Text = "耗时：" + (count++) + "秒";
        }
    }
}
