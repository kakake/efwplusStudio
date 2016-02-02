using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.Devices;
using System.Xml;

namespace PluginManageTool.Common
{
    public class NewTemplateProject
    {
        public static List<string> templateFileList;
        public static List<string> renameFileList;
        public static List<string> renameDirList;

        private static void CreateWinformProject(string prjname, AddlistItem item)
        {
            string temppath = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject\\" + item.filePath;
            string projectpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + prjname;

            if (File.Exists(temppath) == false)
                throw new Exception("你选择的模板代码不存在！");

            //解压
            FastZipHelper.decompress(projectpath, temppath);

            TemplateProjectData data = new TemplateProjectData();
            data.PluginName = prjname;
            data.Title = prjname;
            data.Author = "kakake";
            //解析模板文件
            templateFileList = new List<string>();
            templateFileList.Add("plugin.xml");
            templateFileList.Add("EFW_EmptyWinformProject.sln");
            templateFileList.Add("EFWWin\\EFWWin.csproj");
            templateFileList.Add("EmptyWinformProject\\EmptyWinformProject.csproj");
            templateFileList.Add("EmptyWinformProject.Winform\\EmptyWinformProject.Winform.csproj");
            templateFileList.Add("EmptyWinformProject.Winform\\Controller\\from1winController.cs");
            templateFileList.Add("EmptyWinformProject.Winform\\IView\\IForm1.cs");
            templateFileList.Add("EmptyWinformProject.Winform\\ViewForm\\Form1.cs");
            templateFileList.Add("EmptyWinformProject.Winform\\ViewForm\\Form1.Designer.cs");

            for (int i = 0; i < templateFileList.Count; i++)
            {
                string tfile = projectpath + "\\" + templateFileList[i];
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    TemplateHelper temphelper = new TemplateHelper(finfo.Directory.FullName);
                    temphelper.Put("TemplateData", data);
                    temphelper.Put("csproj", data.PluginName + @"\" + data.PluginName + ".csproj");
                    temphelper.Put("winformcsproj", data.PluginName + ".Winform" + @"\" + data.PluginName + ".Winform" + ".csproj");
                    string code = temphelper.BuildString(finfo.Name);
                    using (StreamWriter sw = new StreamWriter(tfile, false))
                    {
                        sw.Write(code);
                    }
                }
            }
            //重命名文件名
            renameFileList = new List<string>();
            renameFileList.Add("EFW_EmptyWinformProject.sln|EFW_" + data.PluginName + ".sln");
            renameFileList.Add("EmptyWinformProject\\EmptyWinformProject.csproj|" + data.PluginName + ".csproj");
            renameFileList.Add("EmptyWinformProject.Winform\\EmptyWinformProject.Winform.csproj|" + data.PluginName + ".Winform.csproj");
            for (int i = 0; i < renameFileList.Count; i++)
            {
                string oldfile = renameFileList[i].Split(new char[] { '|' })[0];
                string newfile = renameFileList[i].Split(new char[] { '|' })[1];
                string tfile = projectpath + "\\" + oldfile;
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameFile(tfile, newfile);
                }
            }
            //重命名文件夹
            renameDirList = new List<string>();
            renameDirList.Add("EmptyWinformProject|" + data.PluginName);
            renameDirList.Add("EmptyWinformProject.Winform|" + data.PluginName + ".Winform");
            for (int i = 0; i < renameDirList.Count; i++)
            {
                string olddir = renameDirList[i].Split(new char[] { '|' })[0];
                string newdir = renameDirList[i].Split(new char[] { '|' })[1];
                string tdir = projectpath + "\\" + olddir;
                DirectoryInfo dirinfo = new DirectoryInfo(tdir);
                if (dirinfo.Exists)
                {
                    dirinfo.MoveTo(projectpath + "\\" + newdir);
                }
            }
            //pluginsys.xml
            PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            PluginSysManage.AddPlugin("WinformModulePlugin", prjname, "ModulePlugin/" + prjname + "/plugin.xml", data.Title, "1");

        }
        private static void CreateWcfProject(string prjname, AddlistItem item)
        {
            string temppath = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject\\" + item.filePath;
            string projectpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + prjname;
            if (File.Exists(temppath) == false)
                throw new Exception("你选择的模板代码不存在！");

            //解压
            FastZipHelper.decompress(projectpath, temppath);

            TemplateProjectData data = new TemplateProjectData();
            data.PluginName = prjname;
            data.Title = prjname;
            data.Author = "kakake";
            //解析模板文件
            templateFileList = new List<string>();
            templateFileList.Add("plugin.xml");
            templateFileList.Add("EFW_EmptyWcfProject.sln");
            templateFileList.Add("EFWWin\\EFWWin.csproj");
            templateFileList.Add("EmptyWcfProject\\EmptyWcfProject.csproj");
            templateFileList.Add("EmptyWcfProject\\WcfController\\HelloWcfServerController.cs");
            templateFileList.Add("EmptyWcfProject.Winform\\EmptyWcfProject.Winform.csproj");
            templateFileList.Add("EmptyWcfProject.Winform\\Controller\\HelloWcfClientController.cs");
            templateFileList.Add("EmptyWcfProject.Winform\\IView\\IfrmHello.cs");
            templateFileList.Add("EmptyWcfProject.Winform\\ViewForm\\frmHello.cs");
            templateFileList.Add("EmptyWcfProject.Winform\\ViewForm\\frmHello.Designer.cs");

            for (int i = 0; i < templateFileList.Count; i++)
            {
                string tfile = projectpath + "\\" + templateFileList[i];
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    TemplateHelper temphelper = new TemplateHelper(finfo.Directory.FullName);
                    temphelper.Put("TemplateData", data);
                    temphelper.Put("csproj", data.PluginName + @"\" + data.PluginName + ".csproj");
                    temphelper.Put("winformcsproj", data.PluginName + ".Winform" + @"\" + data.PluginName + ".Winform" + ".csproj");
                    string code = temphelper.BuildString(finfo.Name);
                    using (StreamWriter sw = new StreamWriter(tfile, false))
                    {
                        sw.Write(code);
                    }
                }
            }
            //重命名文件名
            renameFileList = new List<string>();
            renameFileList.Add("EFW_EmptyWcfProject.sln|EFW_" + data.PluginName + ".sln");
            renameFileList.Add("EmptyWcfProject\\EmptyWcfProject.csproj|" + data.PluginName + ".csproj");
            renameFileList.Add("EmptyWcfProject.Winform\\EmptyWcfProject.Winform.csproj|" + data.PluginName + ".Winform.csproj");
            for (int i = 0; i < renameFileList.Count; i++)
            {
                string oldfile = renameFileList[i].Split(new char[] { '|' })[0];
                string newfile = renameFileList[i].Split(new char[] { '|' })[1];
                string tfile = projectpath + "\\" + oldfile;
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameFile(tfile, newfile);
                }
            }
            //重命名文件夹
            renameDirList = new List<string>();
            renameDirList.Add("EmptyWcfProject|" + data.PluginName);
            renameDirList.Add("EmptyWcfProject.Winform|" + data.PluginName + ".Winform");
            for (int i = 0; i < renameDirList.Count; i++)
            {
                string olddir = renameDirList[i].Split(new char[] { '|' })[0];
                string newdir = renameDirList[i].Split(new char[] { '|' })[1];
                string tdir = projectpath + "\\" + olddir;
                DirectoryInfo dirinfo = new DirectoryInfo(tdir);
                if (dirinfo.Exists)
                {
                    dirinfo.MoveTo(projectpath + "\\" + newdir);
                }
            }
            //pluginsys.xml
            PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            PluginSysManage.AddPlugin("WcfModulePlugin", prjname, "ModulePlugin/" + prjname + "/plugin.xml", data.Title, "1");

        }
        private static void CreateWebProject(string prjname, AddlistItem item)
        {
            string temppath = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject\\" + item.filePath;
            string projectpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + prjname;
            if (File.Exists(temppath) == false)
                throw new Exception("你选择的模板代码不存在！");

            //解压
            FastZipHelper.decompress(projectpath, temppath);

            TemplateProjectData data = new TemplateProjectData();
            data.PluginName = prjname;
            data.Title = prjname;
            data.Author = "kakake";
            //解析模板文件
            templateFileList = new List<string>();
            templateFileList.Add("plugin.xml");
            templateFileList.Add("EFW_EmptyWebProject.sln");

            templateFileList.Add("EmptyWebProject\\EmptyWebProject.csproj");
            templateFileList.Add("EmptyWebProject\\WebController\\HelloController.cs");

            //templateFileList.Add("JS\\Hello.js");

            for (int i = 0; i < templateFileList.Count; i++)
            {
                string tfile = projectpath + "\\" + templateFileList[i];
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    TemplateHelper temphelper = new TemplateHelper(finfo.Directory.FullName);
                    temphelper.Put("TemplateData", data);
                    temphelper.Put("csproj", data.PluginName + @"\" + data.PluginName + ".csproj");
                    //temphelper.Put("winformcsproj", data.PluginName + ".Winform" + @"\" + data.PluginName + ".Winform" + ".csproj");
                    string code = temphelper.BuildString(finfo.Name);
                    using (StreamWriter sw = new StreamWriter(tfile, false))
                    {
                        sw.Write(code);
                    }
                }
            }
            //重命名文件名
            renameFileList = new List<string>();
            renameFileList.Add("EFW_EmptyWebProject.sln|EFW_" + data.PluginName + ".sln");
            renameFileList.Add("EmptyWebProject\\EmptyWebProject.csproj|" + data.PluginName + ".csproj");

            for (int i = 0; i < renameFileList.Count; i++)
            {
                string oldfile = renameFileList[i].Split(new char[] { '|' })[0];
                string newfile = renameFileList[i].Split(new char[] { '|' })[1];
                string tfile = projectpath + "\\" + oldfile;
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameFile(tfile, newfile);
                }
            }
            //重命名文件夹
            renameDirList = new List<string>();
            renameDirList.Add("EmptyWebProject|" + data.PluginName);

            for (int i = 0; i < renameDirList.Count; i++)
            {
                string olddir = renameDirList[i].Split(new char[] { '|' })[0];
                string newdir = renameDirList[i].Split(new char[] { '|' })[1];
                string tdir = projectpath + "\\" + olddir;
                DirectoryInfo dirinfo = new DirectoryInfo(tdir);
                if (dirinfo.Exists)
                {
                    dirinfo.MoveTo(projectpath + "\\" + newdir);
                }
            }
            //pluginsys.xml
            PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
            PluginSysManage.AddPlugin("WebModulePlugin", prjname, "ModulePlugin/" + prjname + "/plugin.xml", data.Title, "1");

        }

        public static void NewWinformProject(string prjname,AddlistItem item)
        {
            CreateWinformProject(prjname, item);
            //pluginsys.xml配置文件的启动设置为<WinformModulePlugin EntryPlugin="WinMainUIFrame" EntryController="LoginController">
            string pluginsys = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(pluginsys);
            XmlNode nodeN = xmlDoc.DocumentElement.SelectSingleNode("WinformModulePlugin");
            nodeN.Attributes["EntryPlugin"].Value = prjname;
            nodeN.Attributes["EntryController"].Value = "from1winController";
            xmlDoc.Save(pluginsys);   
        }
        public static void NewWcfProject(string prjname, AddlistItem item)
        {
            CreateWcfProject(prjname, item);

            string pluginsys = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(pluginsys);
            XmlNode nodeN = xmlDoc.DocumentElement.SelectSingleNode("WcfModulePlugin");
            nodeN.Attributes["EntryPlugin"].Value = prjname;
            nodeN.Attributes["EntryController"].Value = "HelloWcfClientController";
            xmlDoc.Save(pluginsys); 
        }
        public static void NewWebProject(string prjname, AddlistItem item)
        {
            CreateWebProject(prjname, item);

            string netwebserver = CommonHelper.AppRootPath + "\\NetWebServer.exe.config";
            XmlDocument xmlDoc_webserver = new System.Xml.XmlDocument();
            xmlDoc_webserver.Load(netwebserver);
            XmlNode node = xmlDoc_webserver.DocumentElement.SelectSingleNode("appSettings/add[@key='defaultpage']");
            node.Attributes["value"].Value = "ModulePlugin/" + prjname + "/Aspx/Hello.aspx";
            xmlDoc_webserver.Save(netwebserver);
        }

        public static void NewWebProject_WebForm(string prjname, AddlistItem item)
        {
            string temppath = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject\\" + item.filePath;
            string projectpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + prjname;
            if (File.Exists(temppath) == false)
                throw new Exception("你选择的模板代码不存在！");

            //解压
            FastZipHelper.decompress(projectpath, temppath);

            TemplateProjectData data = new TemplateProjectData();
            data.PluginName = prjname;
            data.Title = prjname;
            data.Author = "kakake";
            //解析模板文件
            templateFileList = new List<string>();
            templateFileList.Add("plugin.xml");
            templateFileList.Add("EFW_EmptyWebProject.sln");

            templateFileList.Add("EmptyWebProject\\EmptyWebProject.csproj");
            //templateFileList.Add("EmptyWebProject\\WebController\\HelloController.cs");

            //templateFileList.Add("JS\\Hello.js");
            //templateFileList.Add("Webform\\TestWebForm1.aspx");
            templateFileList.Add("Webform\\TestWebForm1.aspx.cs");
            templateFileList.Add("Webform\\TestWebForm1.aspx.designer.cs");

            for (int i = 0; i < templateFileList.Count; i++)
            {
                string tfile = projectpath + "\\" + templateFileList[i];
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    TemplateHelper temphelper = new TemplateHelper(finfo.Directory.FullName);
                    temphelper.Put("TemplateData", data);
                    temphelper.Put("csproj", data.PluginName + @"\" + data.PluginName + ".csproj");
                    //temphelper.Put("winformcsproj", data.PluginName + ".Winform" + @"\" + data.PluginName + ".Winform" + ".csproj");
                    string code = temphelper.BuildString(finfo.Name);
                    using (StreamWriter sw = new StreamWriter(tfile, false))
                    {
                        sw.Write(code);
                    }
                }
            }
            //重命名文件名
            renameFileList = new List<string>();
            renameFileList.Add("EFW_EmptyWebProject.sln|EFW_" + data.PluginName + ".sln");
            renameFileList.Add("EmptyWebProject\\EmptyWebProject.csproj|" + data.PluginName + ".csproj");

            for (int i = 0; i < renameFileList.Count; i++)
            {
                string oldfile = renameFileList[i].Split(new char[] { '|' })[0];
                string newfile = renameFileList[i].Split(new char[] { '|' })[1];
                string tfile = projectpath + "\\" + oldfile;
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameFile(tfile, newfile);
                }
            }
            //重命名文件夹
            renameDirList = new List<string>();
            renameDirList.Add("EmptyWebProject|" + data.PluginName);

            for (int i = 0; i < renameDirList.Count; i++)
            {
                string olddir = renameDirList[i].Split(new char[] { '|' })[0];
                string newdir = renameDirList[i].Split(new char[] { '|' })[1];
                string tdir = projectpath + "\\" + olddir;
                DirectoryInfo dirinfo = new DirectoryInfo(tdir);
                if (dirinfo.Exists)
                {
                    dirinfo.MoveTo(projectpath + "\\" + newdir);
                }
            }
            //pluginsys.xml
            PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
            PluginSysManage.AddPlugin("WebModulePlugin", prjname, "ModulePlugin/" + prjname + "/plugin.xml", data.Title, "1");

        }
        public static void NewWebProject_NVelocity(string prjname, AddlistItem item)
        {
            string temppath = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject\\" + item.filePath;
            string projectpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + prjname;
            if (File.Exists(temppath) == false)
                throw new Exception("你选择的模板代码不存在！");

            //解压
            FastZipHelper.decompress(projectpath, temppath);

            TemplateProjectData data = new TemplateProjectData();
            data.PluginName = prjname;
            data.Title = prjname;
            data.Author = "kakake";
            //解析模板文件
            templateFileList = new List<string>();
            templateFileList.Add("plugin.xml");
            templateFileList.Add("EFW_EmptyWebProject.sln");

            templateFileList.Add("EmptyWebProject\\EmptyWebProject.csproj");
            templateFileList.Add("EmptyWebProject\\WebController\\HelloController.cs");
            templateFileList.Add("EmptyWebProject\\WebController\\TestTemplateController.cs");

            //templateFileList.Add("JS\\Hello.js");

            for (int i = 0; i < templateFileList.Count; i++)
            {
                string tfile = projectpath + "\\" + templateFileList[i];
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    TemplateHelper temphelper = new TemplateHelper(finfo.Directory.FullName);
                    temphelper.Put("TemplateData", data);
                    temphelper.Put("csproj", data.PluginName + @"\" + data.PluginName + ".csproj");
                    //temphelper.Put("winformcsproj", data.PluginName + ".Winform" + @"\" + data.PluginName + ".Winform" + ".csproj");
                    string code = temphelper.BuildString(finfo.Name);
                    using (StreamWriter sw = new StreamWriter(tfile, false))
                    {
                        sw.Write(code);
                    }
                }
            }
            //重命名文件名
            renameFileList = new List<string>();
            renameFileList.Add("EFW_EmptyWebProject.sln|EFW_" + data.PluginName + ".sln");
            renameFileList.Add("EmptyWebProject\\EmptyWebProject.csproj|" + data.PluginName + ".csproj");

            for (int i = 0; i < renameFileList.Count; i++)
            {
                string oldfile = renameFileList[i].Split(new char[] { '|' })[0];
                string newfile = renameFileList[i].Split(new char[] { '|' })[1];
                string tfile = projectpath + "\\" + oldfile;
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameFile(tfile, newfile);
                }
            }
            //重命名文件夹
            renameDirList = new List<string>();
            renameDirList.Add("EmptyWebProject|" + data.PluginName);

            for (int i = 0; i < renameDirList.Count; i++)
            {
                string olddir = renameDirList[i].Split(new char[] { '|' })[0];
                string newdir = renameDirList[i].Split(new char[] { '|' })[1];
                string tdir = projectpath + "\\" + olddir;
                DirectoryInfo dirinfo = new DirectoryInfo(tdir);
                if (dirinfo.Exists)
                {
                    dirinfo.MoveTo(projectpath + "\\" + newdir);
                }
            }
            //pluginsys.xml
            PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
            PluginSysManage.AddPlugin("WebModulePlugin", prjname, "ModulePlugin/" + prjname + "/plugin.xml", data.Title, "1");

        }
        public static void NewWebProject_Razor(string prjname, AddlistItem item)
        {
            string temppath = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject\\" + item.filePath;
            string projectpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + prjname;
            if (File.Exists(temppath) == false)
                throw new Exception("你选择的模板代码不存在！");

            //解压
            FastZipHelper.decompress(projectpath, temppath);

            TemplateProjectData data = new TemplateProjectData();
            data.PluginName = prjname;
            data.Title = prjname;
            data.Author = "kakake";
            //解析模板文件
            templateFileList = new List<string>();
            templateFileList.Add("plugin.xml");
            templateFileList.Add("EFW_EmptyWebProject.sln");

            templateFileList.Add("EmptyWebProject\\EmptyWebProject.csproj");
            templateFileList.Add("EmptyWebProject\\WebController\\HelloController.cs");
            templateFileList.Add("EmptyWebProject\\WebController\\TestTemplateController.cs");

            //templateFileList.Add("JS\\Hello.js");
            templateFileList.Add("templatefile\\test01.cshtml");
            templateFileList.Add("templatefile\\head.cshtml");
            templateFileList.Add("templatefile\\footer.cshtml");

            for (int i = 0; i < templateFileList.Count; i++)
            {
                string tfile = projectpath + "\\" + templateFileList[i];
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    TemplateHelper temphelper = new TemplateHelper(finfo.Directory.FullName);
                    temphelper.Put("TemplateData", data);
                    temphelper.Put("csproj", data.PluginName + @"\" + data.PluginName + ".csproj");
                    //temphelper.Put("winformcsproj", data.PluginName + ".Winform" + @"\" + data.PluginName + ".Winform" + ".csproj");
                    string code = temphelper.BuildString(finfo.Name);
                    using (StreamWriter sw = new StreamWriter(tfile, false))
                    {
                        sw.Write(code);
                    }
                }
            }
            //重命名文件名
            renameFileList = new List<string>();
            renameFileList.Add("EFW_EmptyWebProject.sln|EFW_" + data.PluginName + ".sln");
            renameFileList.Add("EmptyWebProject\\EmptyWebProject.csproj|" + data.PluginName + ".csproj");

            for (int i = 0; i < renameFileList.Count; i++)
            {
                string oldfile = renameFileList[i].Split(new char[] { '|' })[0];
                string newfile = renameFileList[i].Split(new char[] { '|' })[1];
                string tfile = projectpath + "\\" + oldfile;
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameFile(tfile, newfile);
                }
            }
            //重命名文件夹
            renameDirList = new List<string>();
            renameDirList.Add("EmptyWebProject|" + data.PluginName);

            for (int i = 0; i < renameDirList.Count; i++)
            {
                string olddir = renameDirList[i].Split(new char[] { '|' })[0];
                string newdir = renameDirList[i].Split(new char[] { '|' })[1];
                string tdir = projectpath + "\\" + olddir;
                DirectoryInfo dirinfo = new DirectoryInfo(tdir);
                if (dirinfo.Exists)
                {
                    dirinfo.MoveTo(projectpath + "\\" + newdir);
                }
            }
            //pluginsys.xml
            PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
            PluginSysManage.AddPlugin("WebModulePlugin", prjname, "ModulePlugin/" + prjname + "/plugin.xml", data.Title, "1");

        }
        public static void NewWebProject_AspNetMvc(string prjname, AddlistItem item)
        {
            string temppath = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject\\" + item.filePath;
            string projectpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + prjname;
            if (File.Exists(temppath) == false)
                throw new Exception("你选择的模板代码不存在！");

            //解压
            FastZipHelper.decompress(projectpath, temppath);

            TemplateProjectData data = new TemplateProjectData();
            data.PluginName = prjname;
            data.Title = prjname;
            data.Author = "kakake";
            //解析模板文件
            templateFileList = new List<string>();
            templateFileList.Add("plugin.xml");
            templateFileList.Add("EFW_EmptyWebProject.sln");

            templateFileList.Add("EmptyWebProject\\EmptyWebProject.csproj");
            templateFileList.Add("EmptyWebProject\\WebController\\HelloController.cs");
            templateFileList.Add("EmptyWebProject\\MvcGlobal.cs");

            //templateFileList.Add("JS\\Hello.js");

            for (int i = 0; i < templateFileList.Count; i++)
            {
                string tfile = projectpath + "\\" + templateFileList[i];
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    TemplateHelper temphelper = new TemplateHelper(finfo.Directory.FullName);
                    temphelper.Put("TemplateData", data);
                    temphelper.Put("csproj", data.PluginName + @"\" + data.PluginName + ".csproj");
                    //temphelper.Put("winformcsproj", data.PluginName + ".Winform" + @"\" + data.PluginName + ".Winform" + ".csproj");
                    string code = temphelper.BuildString(finfo.Name);
                    using (StreamWriter sw = new StreamWriter(tfile, false))
                    {
                        sw.Write(code);
                    }
                }
            }
            //重命名文件名
            renameFileList = new List<string>();
            renameFileList.Add("EFW_EmptyWebProject.sln|EFW_" + data.PluginName + ".sln");
            renameFileList.Add("EmptyWebProject\\EmptyWebProject.csproj|" + data.PluginName + ".csproj");

            for (int i = 0; i < renameFileList.Count; i++)
            {
                string oldfile = renameFileList[i].Split(new char[] { '|' })[0];
                string newfile = renameFileList[i].Split(new char[] { '|' })[1];
                string tfile = projectpath + "\\" + oldfile;
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameFile(tfile, newfile);
                }
            }
            //重命名文件夹
            renameDirList = new List<string>();
            renameDirList.Add("EmptyWebProject|" + data.PluginName);

            for (int i = 0; i < renameDirList.Count; i++)
            {
                string olddir = renameDirList[i].Split(new char[] { '|' })[0];
                string newdir = renameDirList[i].Split(new char[] { '|' })[1];
                string tdir = projectpath + "\\" + olddir;
                DirectoryInfo dirinfo = new DirectoryInfo(tdir);
                if (dirinfo.Exists)
                {
                    dirinfo.MoveTo(projectpath + "\\" + newdir);
                }
            }
            //pluginsys.xml
            PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
            PluginSysManage.AddPlugin("WebModulePlugin", prjname, "ModulePlugin/" + prjname + "/plugin.xml", data.Title, "1");

        }

        public static void NewWebProject_RightFrame(string prjname, AddlistItem item)
        {
            //如果本地没有安装WebUIFrame插件，提示先从中心下载WebUIFrame插件
            string winpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\WebUIFrame";
            if (Directory.Exists(winpath) == false)
            {
                throw new Exception("请先从插件中心下载WebUIFrame插件！");
            }

            CreateWebProject(prjname, item);
            
            //往WcfMainUIFrame插件的menus.xml配置文件中增加一个菜单
            string menuspath = winpath + "\\menus.xml";
            XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(menuspath);
            XmlNode menuN = xmlDoc.DocumentElement.SelectSingleNode("menus");
            XmlNode lastN = menuN.LastChild.CloneNode(true);
            if (lastN.Attributes["PMenuId"].Value == "-1")
                lastN.Attributes["PMenuId"].Value = lastN.Attributes["MenuId"].Value;
            lastN.Attributes["MenuId"].Value = (Convert.ToInt32(lastN.Attributes["MenuId"].Value) + 1).ToString();
            lastN.Attributes["Name"].Value = prjname;
            lastN.Attributes["DllName"].Value = "";
            lastN.Attributes["FunName"].Value = "";
            lastN.Attributes["UrlName"].Value = "ModulePlugin/"+prjname+"/Aspx/Hello.aspx";
            menuN.AppendChild(lastN);//   
            xmlDoc.Save(menuspath);
            //plugin.xml配置文件的isdebug置为true
            //string winplugin = winpath + "\\plugin.xml";
            //xmlDoc = new System.Xml.XmlDocument();
            //xmlDoc.Load(winplugin);
            //XmlNode dataN = xmlDoc.DocumentElement.SelectSingleNode("plugin/baseinfo/data[@key='isdebug']");
            //dataN.Attributes["value"].Value = "true";
            //xmlDoc.Save(winplugin);
            //设置启动页面为调试页面index-debug.html
            string netwebserver = CommonHelper.AppRootPath + "\\NetWebServer.exe.config";
            XmlDocument xmlDoc_webserver = new System.Xml.XmlDocument();
            xmlDoc_webserver.Load(netwebserver);
            XmlNode node = xmlDoc_webserver.DocumentElement.SelectSingleNode("appSettings/add[@key='defaultpage']");
            node.Attributes["value"].Value = "index-debug.html";
            xmlDoc_webserver.Save(netwebserver);
            #region old
            /*
            string temppath = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject\\" + item.filePath;
            string projectpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + prjname;
            if (File.Exists(temppath) == false)
                throw new Exception("你选择的模板代码不存在！");

            //解压
            FastZipHelper.decompress(projectpath+"_temp", temppath);
            new DirectoryInfo(projectpath + "_temp\\EmptyWebProject").MoveTo(projectpath);
            if (Directory.Exists(CommonHelper.WebPlatformPath + "\\ModulePlugin\\WebUIFrame") == false)
            {
                new DirectoryInfo(projectpath + "_temp\\WebUIFrame\\dll").GetFiles().ToList().ForEach(delegate(FileInfo f)
                {
                    f.CopyTo(Path.GetFullPath(Path.Combine(projectpath, "../../bin/")) + f.Name, true);
                });

                new DirectoryInfo(projectpath + "_temp\\WebUIFrame").MoveTo(CommonHelper.WebPlatformPath + "\\ModulePlugin\\WebUIFrame");
                //pluginsys.xml
                PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
                PluginSysManage.AddPlugin("WebModulePlugin", "WebUIFrame", "ModulePlugin/WebUIFrame/plugin.xml","Web通用权限框架", "0");
            }
            Directory.Delete(projectpath + "_temp",true);

            TemplateProjectData data = new TemplateProjectData();
            data.PluginName = prjname;
            data.Title = prjname;
            data.Author = "kakake";
            //解析模板文件
            templateFileList = new List<string>();
            templateFileList.Add("plugin.xml");
            templateFileList.Add("EFW_EmptyWebProject.sln");

            templateFileList.Add("EmptyWebProject\\EmptyWebProject.csproj");
            templateFileList.Add("EmptyWebProject\\WebController\\HelloController.cs");

            //templateFileList.Add("JS\\Hello.js");

            for (int i = 0; i < templateFileList.Count; i++)
            {
                string tfile = projectpath + "\\" + templateFileList[i];
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    TemplateHelper temphelper = new TemplateHelper(finfo.Directory.FullName);
                    temphelper.Put("TemplateData", data);
                    temphelper.Put("csproj", data.PluginName + @"\" + data.PluginName + ".csproj");
                    //temphelper.Put("winformcsproj", data.PluginName + ".Winform" + @"\" + data.PluginName + ".Winform" + ".csproj");
                    string code = temphelper.BuildString(finfo.Name);
                    using (StreamWriter sw = new StreamWriter(tfile, false))
                    {
                        sw.Write(code);
                    }
                }
            }
            //重命名文件名
            renameFileList = new List<string>();
            renameFileList.Add("EFW_EmptyWebProject.sln|EFW_" + data.PluginName + ".sln");
            renameFileList.Add("EmptyWebProject\\EmptyWebProject.csproj|" + data.PluginName + ".csproj");

            for (int i = 0; i < renameFileList.Count; i++)
            {
                string oldfile = renameFileList[i].Split(new char[] { '|' })[0];
                string newfile = renameFileList[i].Split(new char[] { '|' })[1];
                string tfile = projectpath + "\\" + oldfile;
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameFile(tfile, newfile);
                }
            }
            //重命名文件夹
            renameDirList = new List<string>();
            renameDirList.Add("EmptyWebProject|" + data.PluginName);

            for (int i = 0; i < renameDirList.Count; i++)
            {
                string olddir = renameDirList[i].Split(new char[] { '|' })[0];
                string newdir = renameDirList[i].Split(new char[] { '|' })[1];
                string tdir = projectpath + "\\" + olddir;
                DirectoryInfo dirinfo = new DirectoryInfo(tdir);
                if (dirinfo.Exists)
                {
                    dirinfo.MoveTo(projectpath + "\\" + newdir);
                }
            }
            //pluginsys.xml
            PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
            PluginSysManage.AddPlugin("WebModulePlugin", prjname, "ModulePlugin/" + prjname + "/plugin.xml", data.Title, "1");
            */
            #endregion
        }
        public static void NewWinformProject_RightFrame(string prjname, AddlistItem item)
        {
            //如果本地没有安装WinMainUIFrame插件，提示先从中心下载WinMainUIFrame插件
            string winpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\WinMainUIFrame";
            if (Directory.Exists(winpath) == false)
            {
                throw new Exception("请先从插件中心下载WinMainUIFrame插件！");
            }

            CreateWinformProject(prjname, item);

            //往WinMainUIFrame插件的menus.xml配置文件中增加一个菜单
            string menuspath = winpath + "\\menus.xml";
            XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(menuspath);
            XmlNode menuN= xmlDoc.DocumentElement.SelectSingleNode("menus");
            XmlNode lastN = menuN.LastChild.CloneNode(true);
            if (lastN.Attributes["PMenuId"].Value == "-1")
                lastN.Attributes["PMenuId"].Value = lastN.Attributes["MenuId"].Value;
            lastN.Attributes["MenuId"].Value = (Convert.ToInt32(lastN.Attributes["MenuId"].Value) + 1).ToString();
            lastN.Attributes["Name"].Value = prjname;
            lastN.Attributes["DllName"].Value = prjname;
            lastN.Attributes["FunName"].Value = "from1winController";
            lastN.Attributes["UrlName"].Value = "";
            menuN.AppendChild(lastN);//添加到<bookstore>节点中   
            xmlDoc.Save(menuspath);   
            //plugin.xml配置文件的isdebug置为true
            string winplugin = winpath + "\\plugin.xml";
            xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(winplugin);
            XmlNode dataN = xmlDoc.DocumentElement.SelectSingleNode("plugin/baseinfo/data[@key='isdebug']");
            dataN.Attributes["value"].Value = "true";
            xmlDoc.Save(winplugin);   
            //pluginsys.xml配置文件的启动设置为<WinformModulePlugin EntryPlugin="WinMainUIFrame" EntryController="LoginController">
            string pluginsys = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(pluginsys);
            XmlNode nodeN = xmlDoc.DocumentElement.SelectSingleNode("WinformModulePlugin");
            nodeN.Attributes["EntryPlugin"].Value = "WinMainUIFrame";
            nodeN.Attributes["EntryController"].Value = "LoginController";
            xmlDoc.Save(pluginsys);   
            #region old
            /*
            string temppath = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject\\" + item.filePath;
            string projectpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + prjname;

            if (File.Exists(temppath) == false)
                throw new Exception("你选择的模板代码不存在！");

            //解压
            FastZipHelper.decompress(projectpath + "_temp", temppath);
            new DirectoryInfo(projectpath + "_temp\\EmptyWinformProject").MoveTo(projectpath);
            //另一个办法从中心下载最新的WinMainUIFrame插件，因为模板中的不是最新版本的
            if (Directory.Exists(CommonHelper.WinformPlatformPath + "\\ModulePlugin\\WinMainUIFrame") == false)
            {
                new DirectoryInfo(projectpath + "_temp\\WinMainUIFrame\\dll").GetFiles().ToList().ForEach(delegate(FileInfo f)
                {
                    f.CopyTo(Path.GetFullPath(Path.Combine(projectpath, "../../")) + f.Name, true);
                });

                new DirectoryInfo(projectpath + "_temp\\WinMainUIFrame").MoveTo(CommonHelper.WinformPlatformPath + "\\ModulePlugin\\WinMainUIFrame");
                //pluginsys.xml
                PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                PluginSysManage.AddPlugin("WinformModulePlugin", "WinMainUIFrame", "ModulePlugin/WinMainUIFrame/plugin.xml", "Winform通用权限管理", "0");

            }
            else
            {
                //调用一下更新插件
                PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                if (PluginSysManage.ContainsPlugin("WinformModulePlugin", "WinMainUIFrame") == false)
                {
                    PluginSysManage.AddPlugin("WinformModulePlugin", "WinMainUIFrame", "ModulePlugin/WinMainUIFrame/plugin.xml", "Winform通用权限管理", "0");
                }
            }
            Directory.Delete(projectpath + "_temp",true);

            TemplateProjectData data = new TemplateProjectData();
            data.PluginName = prjname;
            data.Title = prjname;
            data.Author = "kakake";
            //解析模板文件
            templateFileList = new List<string>();
            templateFileList.Add("plugin.xml");
            templateFileList.Add("EFW_EmptyWinformProject.sln");
            templateFileList.Add("EFWWin\\EFWWin.csproj");
            templateFileList.Add("EmptyWinformProject\\EmptyWinformProject.csproj");
            templateFileList.Add("EmptyWinformProject.Winform\\EmptyWinformProject.Winform.csproj");
            templateFileList.Add("EmptyWinformProject.Winform\\Controller\\from1winController.cs");
            templateFileList.Add("EmptyWinformProject.Winform\\IView\\IForm1.cs");
            templateFileList.Add("EmptyWinformProject.Winform\\ViewForm\\Form1.cs");
            templateFileList.Add("EmptyWinformProject.Winform\\ViewForm\\Form1.Designer.cs");

            for (int i = 0; i < templateFileList.Count; i++)
            {
                string tfile = projectpath + "\\" + templateFileList[i];
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    TemplateHelper temphelper = new TemplateHelper(finfo.Directory.FullName);
                    temphelper.Put("TemplateData", data);
                    temphelper.Put("csproj", data.PluginName + @"\" + data.PluginName + ".csproj");
                    temphelper.Put("winformcsproj", data.PluginName + ".Winform" + @"\" + data.PluginName + ".Winform" + ".csproj");
                    string code = temphelper.BuildString(finfo.Name);
                    using (StreamWriter sw = new StreamWriter(tfile, false))
                    {
                        sw.Write(code);
                    }
                }
            }
            //重命名文件名
            renameFileList = new List<string>();
            renameFileList.Add("EFW_EmptyWinformProject.sln|EFW_" + data.PluginName + ".sln");
            renameFileList.Add("EmptyWinformProject\\EmptyWinformProject.csproj|" + data.PluginName + ".csproj");
            renameFileList.Add("EmptyWinformProject.Winform\\EmptyWinformProject.Winform.csproj|" + data.PluginName + ".Winform.csproj");
            for (int i = 0; i < renameFileList.Count; i++)
            {
                string oldfile = renameFileList[i].Split(new char[] { '|' })[0];
                string newfile = renameFileList[i].Split(new char[] { '|' })[1];
                string tfile = projectpath + "\\" + oldfile;
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameFile(tfile, newfile);
                }
            }
            //重命名文件夹
            renameDirList = new List<string>();
            renameDirList.Add("EmptyWinformProject|" + data.PluginName);
            renameDirList.Add("EmptyWinformProject.Winform|" + data.PluginName + ".Winform");
            for (int i = 0; i < renameDirList.Count; i++)
            {
                string olddir = renameDirList[i].Split(new char[] { '|' })[0];
                string newdir = renameDirList[i].Split(new char[] { '|' })[1];
                string tdir = projectpath + "\\" + olddir;
                DirectoryInfo dirinfo = new DirectoryInfo(tdir);
                if (dirinfo.Exists)
                {
                    dirinfo.MoveTo(projectpath + "\\" + newdir);
                }
            }
            //pluginsys.xml
            PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            PluginSysManage.AddPlugin("WinformModulePlugin", prjname, "ModulePlugin/" + prjname + "/plugin.xml", data.Title, "1");
            */
            #endregion
        }
        public static void NewWcfProject_RightFrame(string prjname, AddlistItem item)
        {
            //如果本地没有安装WcfMainUIFrame插件，提示先从中心下载WcfMainUIFrame插件
            string winpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\WcfMainUIFrame";
            if (Directory.Exists(winpath) == false)
            {
                throw new Exception("请先从插件中心下载WcfMainUIFrame插件！");
            }

            CreateWcfProject(prjname, item);

            //往WcfMainUIFrame插件的menus.xml配置文件中增加一个菜单
            string menuspath = winpath + "\\menus.xml";
            XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(menuspath);
            XmlNode menuN = xmlDoc.DocumentElement.SelectSingleNode("menus");
            XmlNode lastN = menuN.LastChild.CloneNode(true);
            if (lastN.Attributes["PMenuId"].Value == "-1")
                lastN.Attributes["PMenuId"].Value = lastN.Attributes["MenuId"].Value;
            lastN.Attributes["MenuId"].Value = (Convert.ToInt32(lastN.Attributes["MenuId"].Value) + 1).ToString();
            lastN.Attributes["Name"].Value = prjname;
            lastN.Attributes["DllName"].Value = prjname;
            lastN.Attributes["FunName"].Value = "HelloWcfClientController";
            lastN.Attributes["UrlName"].Value = "";
            menuN.AppendChild(lastN);//   
            xmlDoc.Save(menuspath);
            //plugin.xml配置文件的isdebug置为true
            string winplugin = winpath + "\\plugin.xml";
            xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(winplugin);
            XmlNode dataN = xmlDoc.DocumentElement.SelectSingleNode("plugin/baseinfo/data[@key='isdebug']");
            dataN.Attributes["value"].Value = "true";
            xmlDoc.Save(winplugin);
            //pluginsys.xml配置文件的启动设置为<WinformModulePlugin EntryPlugin="WinMainUIFrame" EntryController="LoginController">
            string pluginsys = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(pluginsys);
            XmlNode nodeN = xmlDoc.DocumentElement.SelectSingleNode("WcfModulePlugin");
            nodeN.Attributes["EntryPlugin"].Value = "WcfMainUIFrame";
            nodeN.Attributes["EntryController"].Value = "wcfclientLoginController";
            xmlDoc.Save(pluginsys); 
            #region old
            /*
            string temppath = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject\\" + item.filePath;
            string projectpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + prjname;
            if (File.Exists(temppath) == false)
                throw new Exception("你选择的模板代码不存在！");

            //解压
            FastZipHelper.decompress(projectpath + "_temp", temppath);
            new DirectoryInfo(projectpath + "_temp\\EmptyWcfProject").MoveTo(projectpath);
            if (Directory.Exists(CommonHelper.WinformPlatformPath + "\\ModulePlugin\\WinMainUIFrame") == false)
            {
                new DirectoryInfo(projectpath + "_temp\\WinMainUIFrame\\dll").GetFiles().ToList().ForEach(delegate(FileInfo f)
                {
                    f.CopyTo(Path.GetFullPath(Path.Combine(projectpath, "../../")) + f.Name, true);
                });

                new DirectoryInfo(projectpath + "_temp\\WinMainUIFrame").MoveTo(CommonHelper.WinformPlatformPath + "\\ModulePlugin\\WinMainUIFrame");
                //pluginsys.xml
                PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                PluginSysManage.AddPlugin("WcfModulePlugin", "WinMainUIFrame", "ModulePlugin/WinMainUIFrame/plugin.xml","Wcf通用权限管理", "0");

            }
            else
            {
                PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                if (PluginSysManage.ContainsPlugin("WcfModulePlugin", "WinMainUIFrame") == false)
                {
                    PluginSysManage.AddPlugin("WcfModulePlugin", "WinMainUIFrame", "ModulePlugin/WinMainUIFrame/plugin.xml", "Wcf通用权限管理", "0");
                }
            }
            Directory.Delete(projectpath + "_temp",true);

            TemplateProjectData data = new TemplateProjectData();
            data.PluginName = prjname;
            data.Title = prjname;
            data.Author = "kakake";
            //解析模板文件
            templateFileList = new List<string>();
            templateFileList.Add("plugin.xml");
            templateFileList.Add("EFW_EmptyWcfProject.sln");
            templateFileList.Add("EFWWin\\EFWWin.csproj");
            templateFileList.Add("EmptyWcfProject\\EmptyWcfProject.csproj");
            templateFileList.Add("EmptyWcfProject\\WcfController\\HelloWcfServerController.cs");
            templateFileList.Add("EmptyWcfProject.Winform\\EmptyWcfProject.Winform.csproj");
            templateFileList.Add("EmptyWcfProject.Winform\\Controller\\HelloWcfClientController.cs");
            templateFileList.Add("EmptyWcfProject.Winform\\IView\\IfrmHello.cs");
            templateFileList.Add("EmptyWcfProject.Winform\\ViewForm\\frmHello.cs");
            templateFileList.Add("EmptyWcfProject.Winform\\ViewForm\\frmHello.Designer.cs");

            for (int i = 0; i < templateFileList.Count; i++)
            {
                string tfile = projectpath + "\\" + templateFileList[i];
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    TemplateHelper temphelper = new TemplateHelper(finfo.Directory.FullName);
                    temphelper.Put("TemplateData", data);
                    temphelper.Put("csproj", data.PluginName + @"\" + data.PluginName + ".csproj");
                    temphelper.Put("winformcsproj", data.PluginName + ".Winform" + @"\" + data.PluginName + ".Winform" + ".csproj");
                    string code = temphelper.BuildString(finfo.Name);
                    using (StreamWriter sw = new StreamWriter(tfile, false))
                    {
                        sw.Write(code);
                    }
                }
            }
            //重命名文件名
            renameFileList = new List<string>();
            renameFileList.Add("EFW_EmptyWcfProject.sln|EFW_" + data.PluginName + ".sln");
            renameFileList.Add("EmptyWcfProject\\EmptyWcfProject.csproj|" + data.PluginName + ".csproj");
            renameFileList.Add("EmptyWcfProject.Winform\\EmptyWcfProject.Winform.csproj|" + data.PluginName + ".Winform.csproj");
            for (int i = 0; i < renameFileList.Count; i++)
            {
                string oldfile = renameFileList[i].Split(new char[] { '|' })[0];
                string newfile = renameFileList[i].Split(new char[] { '|' })[1];
                string tfile = projectpath + "\\" + oldfile;
                FileInfo finfo = new FileInfo(tfile);
                if (finfo.Exists)
                {
                    Computer MyComputer = new Computer();
                    MyComputer.FileSystem.RenameFile(tfile, newfile);
                }
            }
            //重命名文件夹
            renameDirList = new List<string>();
            renameDirList.Add("EmptyWcfProject|" + data.PluginName);
            renameDirList.Add("EmptyWcfProject.Winform|" + data.PluginName + ".Winform");
            for (int i = 0; i < renameDirList.Count; i++)
            {
                string olddir = renameDirList[i].Split(new char[] { '|' })[0];
                string newdir = renameDirList[i].Split(new char[] { '|' })[1];
                string tdir = projectpath + "\\" + olddir;
                DirectoryInfo dirinfo = new DirectoryInfo(tdir);
                if (dirinfo.Exists)
                {
                    dirinfo.MoveTo(projectpath + "\\" + newdir);
                }
            }
            //pluginsys.xml
            PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            PluginSysManage.AddPlugin("WcfModulePlugin", prjname, "ModulePlugin/" + prjname + "/plugin.xml", data.Title, "1");
            */
            #endregion
        }
    }

    public class TemplateProjectData
    {
        public string PluginName{get;set;}
        public string Title { get; set; }
        public string Author { get; set; }
        public string plugintype { get; set; }
        public string defaultdbkey { get; set; }
        public string defaultcachekey { get; set; }
    }
}
