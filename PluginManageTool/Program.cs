using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PluginManageTool.Common;
using System.IO;

namespace PluginManageTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var updater = FSLib.App.SimpleUpdater.Updater.Instance;
            //当检查发生错误时,这个事件会触发
            updater.Error += new EventHandler(updater_Error);
            //没有找到更新的事件
            updater.NoUpdatesFound += new EventHandler(updater_NoUpdatesFound);
            //找到更新的事件.但在此实例中,找到更新会自动进行处理,所以这里并不需要操作
            //updater.UpdatesFound += new EventHandler(updater_UpdatesFound);
            //开始检查更新-这是最简单的模式.请现在 assemblyInfo.cs 中配置更新地址,参见对应的文件.
            FSLib.App.SimpleUpdater.Updater.CheckUpdateSimple(System.Configuration.ConfigurationSettings.AppSettings["UpdaterUrl"]);


           

            CommonHelper.AppRootPath = Application.StartupPath;
            CommonHelper.WebPlatformPath = CommonHelper.PathCombine(CommonHelper.AppRootPath, System.Configuration.ConfigurationManager.AppSettings["WebPlatformPath"]);
            CommonHelper.WinformPlatformPath = CommonHelper.PathCombine(CommonHelper.AppRootPath, System.Configuration.ConfigurationManager.AppSettings["WinformPlatformPath"]);
            VSOpenCommand.devenvExeFile = System.Configuration.ConfigurationManager.AppSettings["devenvExeFile"];

            CommonHelper.issuekey = System.Configuration.ConfigurationManager.AppSettings["issuekey"];
            CommonHelper.plugin_serverurl = System.Configuration.ConfigurationManager.AppSettings["plugin_serverurl"];


            CommonHelper.SetFolderACL(CommonHelper.AppRootPath.Trim(), "Everyone", System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
            
            updatefile();

            Application.Run(new FrmMain());
        }

        static void updater_NoUpdatesFound(object sender, EventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("没有找到更新");
        }

        static void updater_Error(object sender, EventArgs e)
        {
            var updater = sender as FSLib.App.SimpleUpdater.Updater;
            //System.Windows.Forms.MessageBox.Show("访问升级服务失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //升级WebPlatform和WinformPlatform 里的文件
        static void updatefile()
        {
            string webpath_update = CommonHelper.AppRootPath + "\\WebPlatform";
            string winpath_update = CommonHelper.AppRootPath + "\\WinformPlatform";

            if (Directory.Exists(webpath_update))
            {
                CommonHelper.CopyFolder(webpath_update, CommonHelper.WebPlatformPath);
                Directory.Delete(webpath_update, true);
            }

            if (Directory.Exists(winpath_update))
            {
                CommonHelper.CopyFolder(winpath_update, CommonHelper.WinformPlatformPath);
                Directory.Delete(winpath_update, true);
            }

            
            
        }
    }
}
