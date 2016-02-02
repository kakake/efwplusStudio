using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFWCoreLib.WcfFrame.ClientController;
using ${WcfViewForm.AppName}.Winform.IView;

namespace ${WcfViewForm.AppName}.Winform.Viewform
{
    public partial class ${WcfViewForm.ClassName} : BaseForm, ${WcfViewForm.IViewformName}
    {
        public ${WcfViewForm.ClassName}()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Exit");
        }

      
    }
}
