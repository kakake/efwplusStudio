using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFWCoreLib.WebFrame.HttpHandler.Controller;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;

namespace ${WebController.AppName}.WebController
{
    [WebController]
    public class ${WebController.ClassName} : JEasyUIController
    {
        [WebMethod]
        public void Method1()
        {
			//代码实现
            JsonResult = RetSuccess("hello world");
        }
    }
}
