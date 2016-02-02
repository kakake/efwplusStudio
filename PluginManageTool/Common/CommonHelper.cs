using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.Diagnostics;
using DevComponents.DotNetBar;
using System.Xml;

namespace PluginManageTool.Common
{
   public  class CommonHelper
    {
       public static string AppRootPath = "";
       public static string WebPlatformPath = "";
       public static string WinformPlatformPath = "";

       public static string issuekey;
       public static string plugin_serverurl;

       //打开程序
       public static void OpenProcess(string processName)
       {
           FileInfo file = new FileInfo(processName);
           if (file.Exists)
           {
               Process[] ps = Process.GetProcesses();
               foreach (Process item in ps)
               {
                   if (item.ProcessName == file.Name.Replace(file.Extension, ""))
                   {
                       item.Kill();
                   }
               }

               Process.Start(file.FullName);
               //CommonHelper.ShellExecute(IntPtr.Zero, "open", file.FullName, "", "", CommonHelper.ShowCommands.SW_NORMAL);
           }
       }

       public static string PathCombine(string absolutePath, string relativePath)
       {
           return Path.GetFullPath(Path.Combine(absolutePath, relativePath));
       }

       /// <summary>          
       /// Copy文件夹          
       /// </summary>          
       /// <param name="sPath">源文件夹路径</param>          
       /// <param name="dPath">目的文件夹路径</param>          
       /// <returns>完成状态：success-完成；其他-报错</returns>          
       public static void CopyFolder(string sPath, string dPath)
       {
           // 创建目的文件夹                  
           if (!Directory.Exists(dPath))
           {
               Directory.CreateDirectory(dPath);
           }
           // 拷贝文件                  
           DirectoryInfo sDir = new DirectoryInfo(sPath);
           FileInfo[] fileArray = sDir.GetFiles();
           foreach (FileInfo file in fileArray)
           {
               file.CopyTo(dPath + "\\" + file.Name, true);
           }
           // 循环子文件夹                  
           DirectoryInfo dDir = new DirectoryInfo(dPath);
           DirectoryInfo[] subDirArray = sDir.GetDirectories();
           foreach (DirectoryInfo subDir in subDirArray)
           {
               CopyFolder(subDir.FullName, dPath + "//" + subDir.Name);
           }
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="sPath"></param>
       /// <param name="dPath"></param>
       /// <param name="exceptDir">排除此文件夹名称</param>
       public static void CopyFolder(string sPath, string dPath, string exceptDir)
       {
           // 创建目的文件夹                  
           if (!Directory.Exists(dPath))
           {
               Directory.CreateDirectory(dPath);
           }
           // 拷贝文件                  
           DirectoryInfo sDir = new DirectoryInfo(sPath);
           FileInfo[] fileArray = sDir.GetFiles();
           foreach (FileInfo file in fileArray)
           {
               file.CopyTo(dPath + "\\" + file.Name, true);
           }
           // 循环子文件夹                  
           DirectoryInfo dDir = new DirectoryInfo(dPath);
           DirectoryInfo[] subDirArray = sDir.GetDirectories();
           foreach (DirectoryInfo subDir in subDirArray)
           {
               if (exceptDir.Split('|').ToList().FindIndex(x => x == subDir.Name) == -1)
                   CopyFolder(subDir.FullName, dPath + "//" + subDir.Name,exceptDir);
           }
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="sPath"></param>
       /// <param name="dPath"></param>
       /// <param name="exceptDir">排除此文件夹路径</param>
       public static void CopyFolder(string sPath, string dPath, List<string> exceptDir)
       {
           // 创建目的文件夹                  
           if (!Directory.Exists(dPath))
           {
               Directory.CreateDirectory(dPath);
           }
           // 拷贝文件                  
           DirectoryInfo sDir = new DirectoryInfo(sPath);
           FileInfo[] fileArray = sDir.GetFiles();
           foreach (FileInfo file in fileArray)
           {
               file.CopyTo(dPath + "\\" + file.Name, true);
           }
           // 循环子文件夹                  
           DirectoryInfo dDir = new DirectoryInfo(dPath);
           DirectoryInfo[] subDirArray = sDir.GetDirectories();
           foreach (DirectoryInfo subDir in subDirArray)
           {
               if (exceptDir.FindIndex(x => x == subDir.FullName) == -1)
                   CopyFolder(subDir.FullName, dPath + "//" + subDir.Name,exceptDir);
           }
       }

       /// <summary>
       /// 获取文件大小
       /// </summary>
       /// <param name="sFullName"></param>
       /// <returns></returns>
       public static long GetFileSize(string sFullName)
       {
           long lSize = 0;
           if (File.Exists(sFullName))
               lSize = new FileInfo(sFullName).Length;
           return lSize;
       }

       /// <summary>
       /// 计算文件大小函数(保留两位小数),Size为字节大小
       /// </summary>
       /// <param name="Size">初始文件大小</param>
       /// <returns></returns>
       public static string GetFileCountSize(string sFullName)
       {
           long Size = GetFileSize(sFullName);
           string m_strSize = "";
           long FactSize = 0;
           FactSize = Size;
           if (FactSize < 1024.00)
               m_strSize = FactSize.ToString("F2") + " Byte";
           else if (FactSize >= 1024.00 && FactSize < 1048576)
               m_strSize = (FactSize / 1024.00).ToString("F2") + " K";
           else if (FactSize >= 1048576 && FactSize < 1073741824)
               m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " M";
           else if (FactSize >= 1073741824)
               m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " G";
           return m_strSize;
       }


       public static bool PluginSetup(string localzippath)
       {
           FileInfo fileinfo = new FileInfo(localzippath);
           if (fileinfo.Exists == false) throw new Exception("插件包不存在！");

           string temp_pluginpath = fileinfo.Directory.FullName + "\\" + fileinfo.Name.Replace(fileinfo.Extension, "");
           FastZipHelper.decompress(temp_pluginpath, localzippath);
           PluginXmlManage.pluginfile = temp_pluginpath + "\\plugin.xml";
           pluginxmlClass plugin = PluginXmlManage.getpluginclass();

           string pluginpath = "";
           string pluginsyspath = "";
           string dbconfig = "";
           string plugintype = "";
           string ptype = plugin.plugintype.ToLower();

           bool ishave = false;
           switch (ptype)
           {
               case "web":
                   pluginsyspath = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
                   dbconfig = CommonHelper.WebPlatformPath + "\\Web.config";
                   plugintype = "WebModulePlugin";
                   pluginpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + plugin.name;
                   PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
                   if (PluginSysManage.ContainsPlugin("WebModulePlugin", plugin.name))
                   {
                       ishave = true;
                   }
                   break;
               case "winform":
                   pluginsyspath = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                   dbconfig = CommonHelper.WinformPlatformPath + "\\Config\\EntLib.config";
                   plugintype = "WinformModulePlugin";
                   pluginpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + plugin.name;
                   PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                   if (PluginSysManage.ContainsPlugin("WinformModulePlugin", plugin.name))
                   {
                       ishave = true;
                   }
                   break;
               case "wcf":
                   pluginsyspath = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                   dbconfig = CommonHelper.WinformPlatformPath + "\\Config\\EntLib.config";
                   plugintype = "WcfModulePlugin";
                   pluginpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + plugin.name;
                   PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                   if (PluginSysManage.ContainsPlugin("WcfModulePlugin", plugin.name))
                   {
                       ishave = true;
                   }
                   break;

           }

           //先判断此插件本地是否存在
           if (ishave == true)
           {
               DevComponents.DotNetBar.MessageBoxEx.Show("你选择的插件本地已安装！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
               return false;
           }

           string connectionString;
           //添加创建数据库的方法
           if (PluginSetupCreateTable(temp_pluginpath, plugin.setup.FindAll(x => x.type == "sql"), out connectionString) == false)
           {
               if (MessageBoxEx.Show("是否继续安装此插件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                   return false;
           }
           else//修改数据库连接
           {
               if (string.IsNullOrEmpty(dbconfig) == false && File.Exists(dbconfig) && string.IsNullOrEmpty(connectionString)==false)
               {
                   XmlDocument xmlDoc = new XmlDocument();
                   xmlDoc.Load(dbconfig);

                   XmlNode nodeN = xmlDoc.DocumentElement.SelectSingleNode("connectionStrings/add[@name='" + plugin.defaultdbkey + "']");
                   if (nodeN != null)
                   {
                       nodeN.Attributes["connectionString"].Value = connectionString;
                       xmlDoc.Save(dbconfig);
                   }
               }
           }

           //移动到插件目录
           if (temp_pluginpath != pluginpath)
               new DirectoryInfo(temp_pluginpath).MoveTo(pluginpath);


           foreach (setupClass sc in plugin.setup)
           {
               if (sc.type == "dir")
               {
                   if (sc.copyto != "")
                   {
                       CommonHelper.CopyFolder(pluginpath + "\\" + sc.path, pluginpath + "\\" + sc.copyto);
                   }
               }
               else if (sc.type == "file")
               {
                   if (sc.copyto != "")
                   {
                       new FileInfo(pluginpath + "\\" + sc.path).CopyTo(pluginpath + "\\" + sc.copyto,true);
                   }
               }
           }

           //pluginsys.xml
           PluginSysManage.pluginsysFile = pluginsyspath;
           PluginSysManage.AddPlugin(plugintype, plugin.name, "ModulePlugin/" + plugin.name + "/plugin.xml", plugin.title, "0");

           return true;
       }

       public static bool PluginSetupCreateTable(string sqlpath, List<setupClass> sqlList, out string connectionString)
       {
           connectionString = "";
           if (sqlList.Count > 0)
           {
               frmDatabase frmdb = new frmDatabase();
               frmdb.ShowDialog();
               if (frmdb.IsConnction == true)
               {
                   connectionString = string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=true",
                                             frmdb.sqlIp, frmdb.sqlUsername, frmdb.sqlPassword, frmdb.dbName);

                   new Loading(
                        delegate(object sender, EventArgs e)
                        {
                            for (int i = 0; i < sqlList.Count; i++)
                            {
                                installController.CreateTable(frmdb.sqlIp, frmdb.sqlUsername, frmdb.sqlPassword, frmdb.dbName, sqlpath + "\\" + sqlList[i].path);
                            }
                        }
                       ).ShowDialog();

                    
               }
               else
                   return false;
           }
           return true;
       }


       public enum ShowCommands : int { SW_HIDE = 0, SW_SHOWNORMAL = 1, SW_NORMAL = 1, SW_SHOWMINIMIZED = 2, SW_SHOWMAXIMIZED = 3, SW_MAXIMIZE = 3, SW_SHOWNOACTIVATE = 4, SW_SHOW = 5, SW_MINIMIZE = 6, SW_SHOWMINNOACTIVE = 7, SW_SHOWNA = 8, SW_RESTORE = 9, SW_SHOWDEFAULT = 10, SW_FORCEMINIMIZE = 11, SW_MAX = 11 }
       [DllImport("shell32.dll")]
       public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);


       public static bool SetFolderACL(String FolderPath, String UserName, FileSystemRights Rights, AccessControlType AllowOrDeny)
       {

           InheritanceFlags inherits = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;

           return SetFolderACL(FolderPath, UserName, Rights, AllowOrDeny, inherits, PropagationFlags.None, AccessControlModification.Add);

       }

       public static bool SetFolderACL(String FolderPath, String UserName, FileSystemRights Rights, AccessControlType AllowOrDeny, InheritanceFlags Inherits, PropagationFlags PropagateToChildren, AccessControlModification AddResetOrRemove)
       {

           bool ret;

           DirectoryInfo folder = new DirectoryInfo(FolderPath);

           DirectorySecurity dSecurity = folder.GetAccessControl(AccessControlSections.All);

           FileSystemAccessRule accRule = new FileSystemAccessRule(UserName, Rights, Inherits, PropagateToChildren, AllowOrDeny); dSecurity.ModifyAccessRule(AddResetOrRemove, accRule, out ret);

           folder.SetAccessControl(dSecurity);

           return ret;

       }
    }
}
