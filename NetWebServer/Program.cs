using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace DoNet.WebServer
{
    static class Program
    {
        private static WebServer webserver;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            string path = System.Configuration.ConfigurationSettings.AppSettings["path"];
            string port = System.Configuration.ConfigurationSettings.AppSettings["port"];
            string virtRoot = System.Configuration.ConfigurationSettings.AppSettings["virtRoot"];
            string defaultpage = System.Configuration.ConfigurationSettings.AppSettings["defaultpage"];

            path = Path.GetFullPath(Path.Combine(Application.StartupPath, path));

            if (Directory.Exists(path) == false)
            {
                DirectoryInfo info = new DirectoryInfo(Application.StartupPath);
                path = info.FullName + path;
                if (Directory.Exists(path) == false)
                    throw new Exception("目录不存在！");
            }
            string[] args = new string[] { path, port, virtRoot, defaultpage };
            webserver = new WebServer(args);
            Application.Run(webserver);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show("异常信息：" + e.Exception.Message);
            webserver.Dispose();
        }
        
         
    }
}
