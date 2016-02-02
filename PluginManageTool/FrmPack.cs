using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using PluginManageTool.Common;
using System.IO;
using System.Net;
using DevComponents.DotNetBar;
using Microsoft.Office.Interop.Visio;

namespace PluginManageTool
{
    public partial class FrmPack : MetroForm
    {
        string prjpath = "";
        string prjname = "";
        string plugintype = "";

        string path = "";
        pluginxmlClass plugin;
        public FrmPack(PluginClass pc)
        {
            InitializeComponent();
            this.txtname.Text = pc.name;
            this.txttitle.Text = pc.title;
            this.txtupdatedate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            
            if (pc.plugintype == "WebModulePlugin")
            {
                plugintype = "web";
                path = CommonHelper.WebPlatformPath + "\\" + pc.path;
            }
            else if (pc.plugintype == "WinformModulePlugin")
            {
                plugintype = "winform";
                path = CommonHelper.WinformPlatformPath + "\\" + pc.path;
            }
            else if (pc.plugintype == "WcfModulePlugin")
            {
                plugintype = "wcf";
                path = CommonHelper.WinformPlatformPath + "\\" + pc.path;
            }

            FileInfo finfo = new FileInfo(path);
            if (finfo.Exists)
            {
                prjpath = finfo.Directory.FullName;
                prjname = finfo.Directory.Name;
            }


            PluginXmlManage.pluginfile = path;
            plugin = PluginXmlManage.getpluginclass();
            if (pc.name != plugin.name)
            {
                MessageBoxEx.Show("plugin.xml 与 pluginsys.xml 的名称不一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            txtname.Text = plugin.name;
            txttitle.Text = plugin.title;
            txtauthor.Text = plugin.author;
            txtversion.Text = plugin.version;
            txtintroduction.Text = plugin.data.FindIndex(x => x.key == "introduction") > -1 ? plugin.data.Find(x => x.key == "introduction").value : "";
            txtupdaterecord.Text = plugin.data.FindIndex(x => x.key == "updaterecord") > -1 ? plugin.data.Find(x => x.key == "updaterecord").value : "";
            if (plugin.data.FindIndex(x => x.key == "headpic") > -1 && plugin.data.Find(x => x.key == "headpic").value != "")
            {
                //pb_headpic.Image = Image.FromFile(prjpath + "\\" + plugin.data.Find(x => x.key == "headpic").value);
                pb_headpic.Tag = plugin.data.Find(x => x.key == "headpic").value;
            }
            else
            {
                pb_headpic.Tag = "/ModulePlugin/efwplus_website/headpic/default.png";
            }
            txtStartItem.Text = plugin.data.FindIndex(x => x.key == "StartItem") > -1 ? plugin.data.Find(x => x.key == "StartItem").value : "";
        }

        private void FrmPack_Load(object sender, EventArgs e)
        {
            cksrc_CheckedChanged(null,null);
        }
        //插件打包上传
        private void btnpackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(prjpath + ".zip"))
                {
                    File.Delete(prjpath + ".zip");
                }

                string temp = prjpath + "_temp";
                Directory.CreateDirectory(temp);
                foreach (issueClass ic in plugin.issue)
                {
                    if (ic.type == "dir")
                    {
                        if (ic.source != "")
                        {
                            CommonHelper.CopyFolder(prjpath + "\\" + ic.source, temp + "\\" + ic.path);
                        }
                        else
                        {
                            if (Directory.Exists(prjpath + "\\" + ic.path))
                                CommonHelper.CopyFolder(prjpath + "\\" + ic.path, temp + "\\" + ic.path);
                            else
                                Directory.CreateDirectory(temp + "\\" + ic.path);
                        }
                    }
                    else if (ic.type == "file")
                    {
                        if (ic.source != "")
                        {
                            new FileInfo(prjpath + "\\" + ic.source).CopyTo(temp + "\\" + ic.path, true);
                        }
                        else
                        {
                            new FileInfo(prjpath + "\\" + ic.path).CopyTo(temp + "\\" + ic.path, true);
                        }
                    }
                }


                FastZipHelper.compress(temp, prjpath + ".zip");
                Directory.Delete(temp, true);
                //txtdownloadpath.Text = prjpath + ".zip";


                txtpluginsize.Text = CommonHelper.GetFileCountSize(prjpath + ".zip");


                frmprogress progress = new frmprogress();
                

                string url = CommonHelper.plugin_serverurl + "/Controller.aspx?controller=efwplus_website@PluginController&method=uploadfile";

                //上传
                UpDownLoadFileHelper updown = new UpDownLoadFileHelper();
                updown.Cdelegate = new UpDownLoadFileHelper.controldelegate(progress.refreshControl);
                updown.UpDown = UpDownLoadFileHelper.updown.上传;
                updown.Completed = delegate()
                {
                    txtdownloadpath.Text = "/ModulePlugin/efwplus_website/pluginpackage/" + prjname + ".zip";
                    //System.Threading.Thread.Sleep(2000);
                    progress.Close();
                };
                updown.Cancelled = delegate()
                {
                    progress.Close();
                };
                updown.Start(url, prjpath + ".zip");
                progress.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBoxEx.Show("上传插件包失败！\n" + err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //取消上传
        private void btncancelup_Click(object sender, EventArgs e)
        {

        }
        //发布
        private void btnissue_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttitle.Text == "")
                {
                    MessageBoxEx.Show("别名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txttitle.Focus();
                    return;
                }
                if (txtauthor.Text == "")
                {
                    MessageBoxEx.Show("作者不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtauthor.Focus();
                    return;
                }

                if (txtversion.Text == "")
                {
                    MessageBoxEx.Show("版本号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtversion.Focus();
                    return;
                }

                if (txtdownloadpath.Text == "")
                {
                    MessageBoxEx.Show("先上传插件包！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtdownloadpath.Focus();
                    return;
                }

                if (cksrc.Checked)
                {
                    if (txtsrc.Text == "")
                    {
                        MessageBoxEx.Show("源代码包不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtsrc.Focus();
                        return;
                    }

                    if (txtdocs.Text == "")
                    {
                        MessageBoxEx.Show("设计文档不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtdocs.Focus();
                        return;
                    }
                }

                savepluginxml();

                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("name", txtname.Text);
                parameters.Add("title", txttitle.Text);//
                parameters.Add("author", txtauthor.Text);//
                parameters.Add("pversion", txtversion.Text);//
                parameters.Add("plugintype", plugintype);
                parameters.Add("pluginsize", txtpluginsize.Text);
                parameters.Add("updatedate", txtupdatedate.Text);
                parameters.Add("introduction", txtintroduction.Text);//
                parameters.Add("headpic", pb_headpic.Tag.ToString());//
                parameters.Add("updaterecord", txtupdaterecord.Text);//
                parameters.Add("downloadpath", txtdownloadpath.Text);
                parameters.Add("srcpath", "");
                parameters.Add("StartItem", txtStartItem.Text);//


                string url = CommonHelper.plugin_serverurl + "/Controller.aspx?controller=efwplus_website@PluginController&method=issueplugin";
                HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(url, parameters, null, null, Encoding.UTF8, null);
                string ret = HttpWebResponseUtility.GetHttpData(response);

                //上传源代码和设计文档
                if (cksrc.Checked)
                {
                    string srcfile = txtsrc.Text;
                    string docfile = txtdocs.Text;
                    string docpic = prjpath + "_pic";

                    //上传源代码
                    url = CommonHelper.plugin_serverurl + "/Controller.aspx?controller=efwplus_website@PluginController&method=uploadsrc&name=" + txtname.Text;
                    updateserver(url, srcfile);

                    url = CommonHelper.plugin_serverurl + "/Controller.aspx?controller=efwplus_website@PluginController&method=uploadvsd&name=" + txtname.Text;
                    updateserver(url, docfile);

                    DirectoryInfo dirs = new DirectoryInfo(docpic);
                    if (dirs.Exists)
                    {
                        foreach (FileInfo p in dirs.GetFiles())
                        {
                            string pic = p.FullName;
                            url = CommonHelper.plugin_serverurl + "/Controller.aspx?controller=efwplus_website@PluginController&method=uploaddoc_pic&name=" + txtname.Text;
                            updateserver(url, pic);
                        }
                    }
                }

                MessageBoxEx.Show("发布成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                MessageBoxEx.Show("发布失败！\n" + err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void updateserver(string url,string file)
        {
            UpDownLoadFileHelper updown = new UpDownLoadFileHelper();
            updown.UpDown = UpDownLoadFileHelper.updown.上传;
            updown.Start(url, file);
        }

        private void savepluginxml()
        {
            plugin.title = txttitle.Text;
            plugin.author = txtauthor.Text;
            plugin.version = txtversion.Text;

            if (plugin.data.FindIndex(x => x.key == "introduction") > -1)
            {
                plugin.data.Find(x => x.key == "introduction").value = txtintroduction.Text;
            }
            else
            {
                baseinfodataClass data = new baseinfodataClass();
                data.key = "introduction";
                data.value = txtintroduction.Text;
                plugin.data.Add(data);
            }

            if (plugin.data.FindIndex(x => x.key == "headpic") > -1)
            {
                plugin.data.Find(x => x.key == "headpic").value = pb_headpic.Tag.ToString();
            }
            else
            {
                baseinfodataClass data = new baseinfodataClass();
                data.key = "headpic";
                data.value = pb_headpic.Tag.ToString();
                plugin.data.Add(data);
            }

            if (plugin.data.FindIndex(x => x.key == "updaterecord") > -1)
            {
                plugin.data.Find(x => x.key == "updaterecord").value = txtupdaterecord.Text;
            }
            else
            {
                baseinfodataClass data = new baseinfodataClass();
                data.key = "updaterecord";
                data.value = txtupdaterecord.Text;
                plugin.data.Add(data);
            }

            if (plugin.data.FindIndex(x => x.key == "StartItem") > -1)
            {
                plugin.data.Find(x => x.key == "StartItem").value = txtStartItem.Text;
            }
            else
            {
                baseinfodataClass data = new baseinfodataClass();
                data.key = "StartItem";
                data.value = txtStartItem.Text;
                plugin.data.Add(data);
            }

            PluginXmlManage.pluginfile = path;
            PluginXmlManage.savepluginclass(plugin);
        }

        private void cksrc_CheckedChanged(object sender, EventArgs e)
        {
            if (cksrc.Checked)
            {

                txtsrc.Enabled = false;
                btnsrc.Enabled = true;
                txtdocs.Enabled = false;
                btndocs.Enabled = true;

                string designdocfile = prjpath + "\\" + prjname + "_designdoc.vsd";
                if (!File.Exists(designdocfile))
                {
                    File.Copy(CommonHelper.AppRootPath + "\\Template\\designdoc.vsd", designdocfile);
                }
                string srcfile = prjpath + "_src.zip";
                if (File.Exists(srcfile))
                {
                    txtsrc.Text = srcfile;
                }
                txtdocs.Text = designdocfile;
            }
            else
            {
                txtsrc.Enabled = false;
                btnsrc.Enabled = false;
                txtdocs.Enabled = false;
                btndocs.Enabled = false;
            }
        }

        private void btnsrc_Click(object sender, EventArgs e)
        {
            FrmPackSrc frmsrc = new FrmPackSrc(prjpath);
            frmsrc.ShowDialog();
            if (frmsrc.isOk)
            {
                txtsrc.Text = frmsrc.srcfile_zip;
            }
        }

        private void btndocs_Click(object sender, EventArgs e)
        {
            try
            {

                ApplicationClass app = new ApplicationClass();
                string designdocfile = prjpath + "\\" + prjname + "_designdoc.vsd";
                app.Documents.OpenEx(designdocfile, (short)Microsoft.Office.Interop.Visio.VisOpenSaveArgs.visOpenRW);

                app.BeforeDocumentSave += app_BeforeDocumentSave;
            }
            catch { }
        }

        void app_BeforeDocumentSave(Document doc)
        {
            string docpic = prjpath + "_pic";
            if (!Directory.Exists(docpic))
            {
                Directory.CreateDirectory(docpic);
            }
            foreach (Page page in doc.Pages)
            {
                page.Export(docpic + page.Name + ".jpg");
            }
        }

        #region 压缩解压
        /*
        private void browse1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                path1.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btncompress_Click(object sender, EventArgs e)
        {
            try
            {
                FastZipHelper.compress(path1.Text, path1.Text + ".zip");
                MessageBox.Show("压缩成功");
            }
            catch (Exception err)
            {
                MessageBox.Show("压缩失败\n\t"+err.Message);
            }
        }

        private void browse2_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path2.Text = openFileDialog.FileName;
            }
        }

        private void btndecompress_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo file = new FileInfo(path2.Text);
                FastZipHelper.decompress(file.Directory.FullName+"\\"+file.Name.Replace(file.Extension,""), path2.Text);
                MessageBox.Show("解压成功");
            }
            catch (Exception err)
            {
                MessageBox.Show("解压失败\n\t" + err.Message);
            }
        }
         * */
        #endregion


    }
}
