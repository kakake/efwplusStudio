using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFWCoreLib.WinformFrame.Controller;
using ${WinViewForm.AppName}.Winform.IView;

namespace ${WinViewForm.AppName}.Winform.Viewform
{
    public partial class ${WinViewForm.ClassName} : BaseForm, ${WinViewForm.IViewformName}
    {
        public ${WinViewForm.ClassName}()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Exit");
        }

      
    }
}
