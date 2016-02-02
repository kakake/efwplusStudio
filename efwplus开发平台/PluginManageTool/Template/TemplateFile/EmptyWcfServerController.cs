using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ServerController;

namespace ${WcfServerController.AppName}.WcfController
{
    [WCFController]
    public class ${WcfServerController.ClassName} : JsonWcfServerController
    {
        [WCFMethod]
        public string Method1()
        {
            return "hello world !";
        }
    }
}

