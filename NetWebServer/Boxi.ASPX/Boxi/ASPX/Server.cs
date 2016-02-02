namespace Boxi.ASPX
{
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Web;
    using System.Web.Hosting;

    public class Server : MarshalByRefObject
    {
        private Host _host;
        private string _installPath;
        private string _physicalPath;
        private int _port;
        private WaitCallback _restartCallback;
        private string _virtualPath;
        private Thread th;

        public Server(int port, string virtualPath, string physicalPath)
        {
            this._port = port;
            this._virtualPath = virtualPath;
            this._physicalPath = physicalPath.EndsWith(@"\") ? physicalPath : (physicalPath + @"\");
            this._restartCallback = new WaitCallback(this.RestartCallback);
            this._installPath = this.GetInstallPathAndConfigureAspNetIfNeeded();
            this.CreateHost();
        }

        private void CreateHost()
        {
            this._host = (Host) ApplicationHost.CreateApplicationHost(typeof(Host), this._virtualPath, this._physicalPath);
            this._host.Configure(this, this._port, this._virtualPath, this._physicalPath, this._installPath);
        }

        private string GetInstallPathAndConfigureAspNetIfNeeded()
        {
            RegistryKey key = null;
            RegistryKey key2 = null;
            RegistryKey key3 = null;
            string str = null;
            try
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(typeof(HttpRuntime).Module.FullyQualifiedName);
                string str2 = string.Format("{0}.{1}.{2}.{3}", new object[] { versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart, versionInfo.FilePrivatePart });
                string name = @"Software\Microsoft\ASP.NET\" + str2;
                if (!str2.StartsWith("1.0."))
                {
                    name = name.Substring(0, name.LastIndexOf('.') + 1) + "0";
                }
                key2 = Registry.LocalMachine.OpenSubKey(name);
                if (key2 != null)
                {
                    return (string) key2.GetValue("Path");
                }
                key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\ASP.NET");
                if (key == null)
                {
                    key = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\ASP.NET");
                    key.SetValue("RootVer", str2);
                }
                string str4 = "v" + str2.Substring(0, str2.LastIndexOf('.'));
                key3 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\.NETFramework");
                string str5 = (string) key3.GetValue("InstallRoot");
                if (str5.EndsWith(@"\"))
                {
                    str5 = str5.Substring(0, str5.Length - 1);
                }
                key2 = Registry.LocalMachine.CreateSubKey(name);
                str = str5 + @"\" + str4;
                key2.SetValue("Path", str);
                key2.SetValue("DllFullPath", str + @"\aspnet_isapi.dll");
                return str;
            }
            catch
            {
            }
            finally
            {
                if (key2 != null)
                {
                    key2.Close();
                }
                if (key != null)
                {
                    key.Close();
                }
                if (key3 != null)
                {
                    key3.Close();
                }
            }
            return str;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Restart()
        {
            ThreadPool.QueueUserWorkItem(this._restartCallback);
        }

        private void RestartCallback(object unused)
        {
            this.CreateHost();
            this.Start();
        }

        public void Start()
        {
            if (this._host != null)
            {
                this.th = new Thread(new ThreadStart(this._host.Start));
                this.th.IsBackground = true;
                this.th.Start();
            }
        }

        public void Stop()
        {
            if (this.th != null)
            {
                try
                {
                    this.th.Abort();
                }
                catch
                {
                }
            }
        }

        public string InstallPath
        {
            get
            {
                return this._installPath;
            }
        }

        public string PhysicalPath
        {
            get
            {
                return this._physicalPath;
            }
        }

        public int Port
        {
            get
            {
                return this._port;
            }
        }

        public string RootUrl
        {
            get
            {
                return ("http://localhost:" + this._port + this._virtualPath);
            }
        }

        public string VirtualPath
        {
            get
            {
                return this._virtualPath;
            }
        }
    }
}

