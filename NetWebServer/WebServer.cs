using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Boxi.ASPX;
using System.IO;
using System.Diagnostics;
using DoNet.WebServer.Properties;
using System.Xml;

namespace DoNet.WebServer
{
    public partial class WebServer : Form
    {
        private static string _appPath;
        private static string _portString;
        private Server _server;
        private static string _virtRoot;
        private static string _default;
        private bool flag = false;

        //C:\WebServer 81 / 1/Index/?
        public WebServer(string[] args)
        {
            InitializeComponent();

            //SetEntLibPath(args[0]);

            this.ParseArgs(args);

            if (args.Length >= 1)
            {
                this.Start();
                //Process.Start(this.GetLinkText());
            }
            else
            {
                this.appDirTextBox.Focus();
            }

            //this.Hide();
        }

        public void SetEntLibPath(string _path)
        {

            string path = _path + @"\Web.config";
            if (File.Exists(path) && File.Exists(_path + @"\Config\EntLib.config"))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                XmlNodeList xmlNodesplugin = xmlDoc.DocumentElement.SelectNodes("enterpriseLibrary.ConfigurationSource/sources/add[@name='EntLibConfiguration']");
                XmlElement elem = (XmlElement)xmlNodesplugin[0];
                elem.SetAttribute("filePath", _path + @"\Config\EntLib.config");
                xmlDoc.Save(path);
            }
        }
        private string GetLinkText()
        {
            string str = "http://localhost";
            if (_portString != "80")
            {
                str = str + ":" + _portString;
            }
            str = str + _virtRoot;
            if (!str.EndsWith("/"))
            {
                str = str + "/";
            }
            return str + _default;
        }

       
        private void ParseArgs(string[] args)
        {
            try
            {
                if (args.Length >= 1)
                {
                    _appPath = args[0];
                    Environment.CurrentDirectory = Application.StartupPath;
                }
                else
                {
                    DirectoryInfo info = new DirectoryInfo(Application.StartupPath);
                    _appPath = info.Parent.FullName;
                }
                if (args.Length >= 2)
                {
                    _portString = args[1];
                }
                else
                {
                    Random random = new Random();
                    _portString = random.Next(0, 0x270f).ToString();
                }
                if (args.Length >= 3)
                {
                    _virtRoot = args[2];
                }

                if (args.Length >= 4)
                {
                    _default = args[3];
                }
            }
            catch
            {
            }
            if (_portString == null)
            {
                _portString = "80";
            }
            if (_virtRoot == null)
            {
                _virtRoot = "/";
            }
            if (_appPath == null)
            {
                _appPath = string.Empty;
            }

            this.appDirTextBox.Text = _appPath.Replace("%"," ");
            this.portTextBox.Text = _portString;
            this.vrootTextBox.Text = _virtRoot;
        }

        private void ShowError(string err)
        {
            MessageBox.Show(err, ".Net Web服务器", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void Start()
        {
            _appPath = this.appDirTextBox.Text;
            if (!Application.StartupPath.ToLower().Contains(_appPath.ToLower()))
            {
                string path = Path.Combine(_appPath, "bin");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string destFileName = Path.Combine(path, "Boxi.ASPX.dll");
                try
                {
                    File.Copy(Path.Combine(Application.StartupPath, "Boxi.ASPX.dll"), destFileName, true);
                    this.flag = true;
                }
                catch (IOException)
                {
                }
            }
            if ((_appPath.Length == 0) || !Directory.Exists(_appPath))
            {
                this.ShowError("Invalid Application Directory");
                this.appDirTextBox.SelectAll();
                this.appDirTextBox.Focus();
            }
            else
            {
                _portString = this.portTextBox.Text;
                int port = -1;
                try
                {
                    port = int.Parse(_portString);
                }
                catch
                {
                }
                if (port <= 0)
                {
                    this.ShowError("Invalid Port");
                    this.portTextBox.SelectAll();
                    this.portTextBox.Focus();
                }
                else
                {
                    _virtRoot = this.vrootTextBox.Text;
                    if ((_virtRoot.Length == 0) || !_virtRoot.StartsWith("/"))
                    {
                        this.ShowError("Invalid Virtual Root");
                        this.vrootTextBox.SelectAll();
                        this.vrootTextBox.Focus();
                    }
                    else
                    {
                        try
                        {
                            this._server = new Server(port, _virtRoot, _appPath);
                            this._server.Start();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message);
                            this.ShowError(".Net Web服务器创建失败,侦听端口" + port + ".\r\n可能其它应用占用了此端口或者未知错误");
                            this.portTextBox.SelectAll();
                            this.portTextBox.Focus();
                            return;
                        }
                        //this.startButton.Enabled = false;
                        this.appDirTextBox.Enabled = false;
                        this.portTextBox.Enabled = false;
                        this.vrootTextBox.Enabled = false;
                        //this.browseLabel.Visible = true;
                        this.browseLink.Text = this.GetLinkText();
                        this.browseLink.Visible = true;
                        this.browseLink.Focus();
                    }
                }
            }
        }

        private void Stop()
        {
            try
            {
                if (this._server != null)
                {
                    this._server.Stop();
                    this._server = null;
                }
            }
            catch
            {
            }
            try
            {
                if (this.flag)
                {
                    //File.Delete(Path.Combine(_appPath, @"bin\Boxi.ASPX.dll"));
                }
            }
            catch (IOException)
            {
            }
            //base.Close();
        }

        private void browseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(this.browseLink.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void WebServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        //退出
        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("确定要关闭吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Stop();
                this.Dispose();
            }
        }
        //退出
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("确定要关闭吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Stop();
                this.Dispose();
            }
        }

        private void 显示窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }
        //隐藏
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void WebServer_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Icon = Resources.WebCam_Note;

            this._show = true;

            this.Hide();
            try
            {
                System.Diagnostics.Process.Start(GetLinkText());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        bool _show;
        private void WebServer_Activated(object sender, EventArgs e)
        {
            if (_show)
            {
                this.Hide();
                _show = false;
            }
        }

         

        

      
    }
}
