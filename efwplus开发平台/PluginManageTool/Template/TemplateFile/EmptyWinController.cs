using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFWCoreLib.WinformFrame.Controller;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using ${WinController.AppName}.Winform.IView;

namespace ${WinController.AppName}.Winform.Controller
{
    [WinformController(DefaultViewName = "${WinController.ViewFormName}")]//在菜单上显示
    [WinformView(Name = "${WinController.ViewFormName}", DllName = "${WinController.AppName}.Winform.dll", ViewTypeName = "${WinController.AppName}.Winform.ViewForm.${WinController.ViewFormName}")]//控制器关联的界面
    public class ${WinController.ClassName} : WinformController
    {
        ${WinController.IViewName} ${WinController.varIViewName};
        public override void Init()
        {
            ${WinController.varIViewName} = (${WinController.IViewName})DefaultView;
        }

        [WinformMethod]
        public string Method1()
        {
            return "hello world !";
        }
    }
}

