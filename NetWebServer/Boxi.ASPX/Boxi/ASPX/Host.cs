namespace Boxi.ASPX
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Windows.Forms;

    internal class Host : MarshalByRefObject
    {
        private string _installPath;
        private string _lowerCasedClientScriptPathWithTrailingSlashV10;
        private string _lowerCasedClientScriptPathWithTrailingSlashV11;
        private string _lowerCasedVirtualPath;
        private string _lowerCasedVirtualPathWithTrailingSlash;
        private EventHandler _onAppDomainUnload;
        private WaitCallback _onSocketAccept;
        private WaitCallback _onStart;
        private string _physicalClientScriptPath;
        private string _physicalPath;
        private int _port;
        private Server _server;
        private Socket _socket;
        private bool _started;
        private bool _stopped;
        private string _virtualPath;
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public static void acceptCallback(IAsyncResult ar)
        {
            allDone.Set();
            StateObject asyncState = (StateObject) ar.AsyncState;
            try
            {
                Socket socket = asyncState.workSocket.EndAccept(ar);
                new Boxi.ASPX.Connection(asyncState.host, socket).ProcessOneRequest();
            }
            catch
            {
            }
        }

        public void Configure(Server server, int port, string virtualPath, string physicalPath, string installPath)
        {
            this._server = server;
            this._port = port;
            this._virtualPath = virtualPath;
            this._lowerCasedVirtualPath = CultureInfo.InvariantCulture.TextInfo.ToLower(this._virtualPath);
            this._lowerCasedVirtualPathWithTrailingSlash = virtualPath.EndsWith("/") ? virtualPath : (virtualPath + "/");
            this._lowerCasedVirtualPathWithTrailingSlash = CultureInfo.InvariantCulture.TextInfo.ToLower(this._lowerCasedVirtualPathWithTrailingSlash);
            this._physicalPath = physicalPath;
            this._installPath = installPath;
            this._physicalClientScriptPath = installPath + @"\asp.netclientfiles\";
            string fileVersion = FileVersionInfo.GetVersionInfo(typeof(HttpRuntime).Module.FullyQualifiedName).FileVersion;
            string str2 = fileVersion.Substring(0, fileVersion.LastIndexOf('.'));
            this._lowerCasedClientScriptPathWithTrailingSlashV10 = "/aspnet_client/system_web/" + fileVersion.Replace('.', '_') + "/";
            this._lowerCasedClientScriptPathWithTrailingSlashV11 = "/aspnet_client/system_web/" + str2.Replace('.', '_') + "/";
            this._onAppDomainUnload = new EventHandler(this.OnAppDomainUnload);
            Thread.GetDomain().DomainUnload += this._onAppDomainUnload;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public bool IsVirtualPathAppPath(string path)
        {
            if (path == null)
            {
                return false;
            }
            path = CultureInfo.InvariantCulture.TextInfo.ToLower(path);
            if (!(path == this._lowerCasedVirtualPath))
            {
                return (path == this._lowerCasedVirtualPathWithTrailingSlash);
            }
            return true;
        }

        public bool IsVirtualPathInApp(string path)
        {
            bool flag;
            string str;
            return this.IsVirtualPathInApp(path, out flag, out str);
        }

        public bool IsVirtualPathInApp(string path, out bool isClientScriptPath, out string clientScript)
        {
            isClientScriptPath = false;
            clientScript = null;
            if (path != null)
            {
                if ((this._virtualPath == "/") && path.StartsWith("/"))
                {
                    if (path.StartsWith(this._lowerCasedClientScriptPathWithTrailingSlashV10))
                    {
                        isClientScriptPath = true;
                        clientScript = path.Substring(this._lowerCasedClientScriptPathWithTrailingSlashV10.Length);
                    }
                    if (path.StartsWith(this._lowerCasedClientScriptPathWithTrailingSlashV11))
                    {
                        isClientScriptPath = true;
                        clientScript = path.Substring(this._lowerCasedClientScriptPathWithTrailingSlashV11.Length);
                    }
                    return true;
                }
                path = CultureInfo.InvariantCulture.TextInfo.ToLower(path);
                if (path.StartsWith(this._lowerCasedVirtualPathWithTrailingSlash))
                {
                    return true;
                }
                if (path == this._lowerCasedVirtualPath)
                {
                    return true;
                }
                if (path.StartsWith(this._lowerCasedClientScriptPathWithTrailingSlashV10))
                {
                    isClientScriptPath = true;
                    clientScript = path.Substring(this._lowerCasedClientScriptPathWithTrailingSlashV10.Length);
                    return true;
                }
                if (path.StartsWith(this._lowerCasedClientScriptPathWithTrailingSlashV11))
                {
                    isClientScriptPath = true;
                    clientScript = path.Substring(this._lowerCasedClientScriptPathWithTrailingSlashV11.Length);
                    return true;
                }
            }
            return false;
        }

        private void OnAppDomainUnload(object unusedObject, EventArgs unusedEventArgs)
        {
            Thread.GetDomain().DomainUnload -= this._onAppDomainUnload;
            if (!this._stopped)
            {
                this.Stop();
                this._server.Restart();
                this._server = null;
            }
        }

        private void OnSocketAccept(object acceptedSocket)
        {
            new Boxi.ASPX.Connection(this, (Socket) acceptedSocket).ProcessOneRequest();
        }

        private void OnStart(Socket _socket)
        {
            while (this._started)
            {
                try
                {
                    allDone.Reset();
                    StateObject state = new StateObject();
                    state.host = this;
                    state.workSocket = _socket;
                    _socket.BeginAccept(new AsyncCallback(Host.acceptCallback), state);
                    allDone.WaitOne();
                    continue;
                }
                catch
                {
                    Thread.Sleep(100);
                    continue;
                }
            }
            this._stopped = true;
        }

        public static void Send(Socket socket, string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            try
            {
                socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, null, null);
            }
            catch (SocketException)
            {
            }
        }

        public static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket asyncState = (Socket) ar.AsyncState;
                MessageBox.Show(asyncState.EndSend(ar).ToString());
            }
            catch
            {
            }
        }

        public void Start()
        {
            if (this._started)
            {
                throw new InvalidOperationException();
            }
            this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._socket.Bind(new IPEndPoint(IPAddress.Any, this._port));
            int backlog = 0x7fffffff;
            this._socket.Listen(backlog);
            this._started = true;
            this.OnStart(this._socket);
        }

        public void Stop()
        {
            if (!this._started)
            {
                return;
            }
            this._started = false;
            try
            {
                try
                {
                    this._socket.Close();
                }
                catch
                {
                }
                goto Label_0031;
            }
            finally
            {
                this._socket = null;
            }
        Label_002A:
            Thread.Sleep(100);
        Label_0031:
            if (!this._stopped)
            {
                goto Label_002A;
            }
        }

        public string InstallPath
        {
            get
            {
                return this._installPath;
            }
        }

        public string NormalizedVirtualPath
        {
            get
            {
                return this._lowerCasedVirtualPathWithTrailingSlash;
            }
        }

        public string PhysicalClientScriptPath
        {
            get
            {
                return this._physicalClientScriptPath;
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

        public string VirtualPath
        {
            get
            {
                return this._virtualPath;
            }
        }
    }
}

