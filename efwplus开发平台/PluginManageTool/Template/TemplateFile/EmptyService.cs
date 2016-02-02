using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using EFWCoreLib.CoreFrame.Business;
using ${WebServices.AppName}.Entity;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
// [System.Web.Script.Services.ScriptService]
public class ${WebServices.ClassName} : AbstractService
{

    [WebMethod]
    public string Method1()
    {
		//实现代码
        return "Hello World";
    }

}

