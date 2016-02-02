using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using ${WcfClientController.AppName}.Winform.IView;

namespace ${WcfClientController.AppName}.Winform.Controller
{
    [WinformController(DefaultViewName = "${WcfClientController.ViewFormName}")]//在菜单上显示
    [WinformView(Name = "${WcfClientController.ViewFormName}", DllName = "${WcfClientController.AppName}.Winform.dll", ViewTypeName = "${WcfClientController.AppName}.Winform.ViewForm.${WcfClientController.ViewFormName}")]//控制器关联的界面
    public class ${WcfClientController.ClassName} : JsonWcfClientController
    {
        ${WcfClientController.IViewName} ${WcfClientController.varIViewName};
        public override void Init()
        {
            ${WcfClientController.varIViewName} = (${WcfClientController.IViewName})DefaultView;
        }

        [WinformMethod]
        public string Method1()
        {
            return "hello world !";
        }
    }
}

