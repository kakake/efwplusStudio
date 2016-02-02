using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using ICSharpCode.TextEditor;
using PluginManageTool.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluginManageTool
{
    public partial class FrmPluginConfigEdit : MetroForm
    {
        string path = null;
        pluginxmlClass plugin=null;
        PluginClass _pc;
        public FrmPluginConfigEdit(PluginClass pc)
        {
            InitializeComponent();
            _pc = pc;
            loadplugin(pc);

            txtmenupn.Text = pc.name;
        }

        private void FrmPluginConfigEdit_Load(object sender, EventArgs e)
        {
            
        }

        private void loadplugin(PluginClass pc)
        {
            if (pc.plugintype == "WebModulePlugin")
            {
                path = CommonHelper.WebPlatformPath + "\\" + pc.path;
            }
            else if (pc.plugintype == "WinformModulePlugin")
            {
                path = CommonHelper.WinformPlatformPath + "\\" + pc.path;
            }
            else if (pc.plugintype == "WcfModulePlugin")
            {
                path = CommonHelper.WinformPlatformPath + "\\" + pc.path;
            }

            if (File.Exists(path))
            {
                textEditorControl.LoadFile(path);
            }

            PluginXmlManage.pluginfile = path;
            plugin = PluginXmlManage.getpluginclass();

            //将plugin赋值给控件
            if (plugin != null)
            {
                txtname.Text = plugin.name;
                txttitle.Text = plugin.title;
                txtauthor.Text = plugin.author;
                txtversion.Text = plugin.version;
                cbplugintype.Text = plugin.plugintype;
                txtdefaultdbkey.Text = plugin.defaultdbkey;
                txtdefaultcachekey.Text = plugin.defaultcachekey;

                gridbasedata.AutoGenerateColumns = false;
                gridbasedata.DataSource = plugin.data;

                griddll.AutoGenerateColumns = false;
                griddll.DataSource = plugin.dll;

                gridissue.AutoGenerateColumns = false;
                gridissue.DataSource = plugin.issue;

                gridsetup.AutoGenerateColumns = false;
                gridsetup.DataSource = plugin.setup;

                gridmenu.AutoGenerateColumns = false;
                gridmenu.DataSource = plugin.menu;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 3)//plugin.xml
            {
                textEditorControl.SaveFile(path);
            }
            else
            {
                //将控件给plugin赋值
                plugin.name = txtname.Text;
                plugin.title = txttitle.Text;
                plugin.author = txtauthor.Text;
                plugin.version = txtversion.Text;
                plugin.plugintype = cbplugintype.Text;
                plugin.defaultdbkey = txtdefaultdbkey.Text;
                plugin.defaultcachekey = txtdefaultcachekey.Text;

                PluginXmlManage.savepluginclass(plugin);
            }

            MessageBoxEx.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadplugin(_pc);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridbasedata_Click(object sender, EventArgs e)
        {
            if (gridbasedata.CurrentCell == null) return;
            int rowindex = gridbasedata.CurrentCell.RowIndex;
            txtbasedatakey.Text = plugin.data[rowindex].key;
            txtbasedatavalue.Text = plugin.data[rowindex].value;
        }

        private void griddll_Click(object sender, EventArgs e)
        {
            if (griddll.CurrentCell == null) return;
            int rowindex = griddll.CurrentCell.RowIndex;
            txtdllname.Text = plugin.dll[rowindex].name;
            txtdllversion.Text = plugin.dll[rowindex].version;
        }

        private void gridissue_Click(object sender, EventArgs e)
        {
            if (gridissue.CurrentCell == null) return;
            int rowindex = gridissue.CurrentCell.RowIndex;
            txtissuetype.Text = plugin.issue[rowindex].type;
            txtissuepath.Text = plugin.issue[rowindex].path;
            txtissuesource.Text = plugin.issue[rowindex].source;
        }

        private void gridsetup_Click(object sender, EventArgs e)
        {
            if (gridsetup.CurrentCell == null) return;
            int rowindex = gridsetup.CurrentCell.RowIndex;
            txtsetuptype.Text = plugin.setup[rowindex].type;
            txtsetuppath.Text = plugin.setup[rowindex].path;
            txtsetupcopyto.Text = plugin.setup[rowindex].copyto;
        }

        private void gridmenu_Click(object sender, EventArgs e)
        {
            if (gridmenu.CurrentCell == null) return;
            int rowindex = gridmenu.CurrentCell.RowIndex;
            txtmenuname.Text = plugin.menu[rowindex].menuname;
            txtmenupn.Text = plugin.menu[rowindex].pluginname;
            txtmenucn.Text = plugin.menu[rowindex].controllername;
            txtmenuvn.Text = plugin.menu[rowindex].viewname;
            txtmenump.Text = plugin.menu[rowindex].menupath;
            txtmenumemo.Text = plugin.menu[rowindex].memo;
        }

        private void btnbasedataadd_Click(object sender, EventArgs e)
        {
            if (txtbasedatakey.Text.Trim() == "")
            {
                MessageBoxEx.Show("不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (plugin.data.FindIndex(x => x.key == txtbasedatakey.Text.Trim()) == -1)
            {
                baseinfodataClass data = new baseinfodataClass();
                data.key = txtbasedatakey.Text;
                data.value = txtbasedatavalue.Text;
                plugin.data.Add(data);
            }
            else
            {
                baseinfodataClass data = plugin.data.Find(x => x.key == txtbasedatakey.Text.Trim());
                data.key = txtbasedatakey.Text;
                data.value = txtbasedatavalue.Text;
            }
            gridbasedata.DataSource = null;
            gridbasedata.DataSource = plugin.data;
        }

        private void btnbasedatadelete_Click(object sender, EventArgs e)
        {
            if (gridbasedata.CurrentCell == null) return;
            int rowindex = gridbasedata.CurrentCell.RowIndex;

            plugin.data.RemoveAt(rowindex);
            gridbasedata.DataSource = null;
            gridbasedata.DataSource = plugin.data;
        }

        private void btndlladd_Click(object sender, EventArgs e)
        {
            if (txtdllname.Text.Trim() == "")
            {
                MessageBoxEx.Show("不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (plugin.dll.FindIndex(x => x.name == txtdllname.Text.Trim()) == -1)
            {
                businessinfodllClass dll = new businessinfodllClass();
                dll.name = txtdllname.Text;
                dll.version = txtdllversion.Text;
                plugin.dll.Add(dll);
            }
            else
            {
                businessinfodllClass dll = plugin.dll.Find(x => x.name == txtdllname.Text.Trim());
                dll.name = txtdllname.Text;
                dll.version = txtdllversion.Text;
            }
            griddll.DataSource = null;
            griddll.DataSource = plugin.dll;

        }

        private void btndlldelete_Click(object sender, EventArgs e)
        {
            if (griddll.CurrentCell == null) return;
            int rowindex = griddll.CurrentCell.RowIndex;

            plugin.dll.RemoveAt(rowindex);
            griddll.DataSource = null;
            griddll.DataSource = plugin.dll;
        }

        private void btnissueadd_Click(object sender, EventArgs e)
        {
            if (txtissuepath.Text.Trim() == "")
            {
                MessageBoxEx.Show("不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (plugin.issue.FindIndex(x => x.path == txtissuepath.Text.Trim()) == -1)
            {
                issueClass issue = new issueClass();
                issue.type = txtissuetype.Text;
                issue.path = txtissuepath.Text;
                issue.source = txtissuesource.Text;
                plugin.issue.Add(issue);
            }
            else
            {
                issueClass issue = plugin.issue.Find(x => x.path == txtissuepath.Text.Trim());
                issue.type = txtissuetype.Text;
                issue.path = txtissuepath.Text;
                issue.source = txtissuesource.Text;
            }
            gridissue.DataSource = null;
            gridissue.DataSource = plugin.issue;
        }

        private void btnissuedelete_Click(object sender, EventArgs e)
        {
            if (gridissue.CurrentCell == null) return;
            int rowindex = gridissue.CurrentCell.RowIndex;

            plugin.issue.RemoveAt(rowindex);
            gridissue.DataSource = null;
            gridissue.DataSource = plugin.issue;
        }

        private void btnsetupadd_Click(object sender, EventArgs e)
        {
            if (txtsetuppath.Text.Trim() == "")
            {
                MessageBoxEx.Show("不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (plugin.setup.FindIndex(x => x.path == txtsetuppath.Text.Trim()) == -1)
            {
                setupClass setup = new setupClass();
                setup.type = txtsetuptype.Text;
                setup.path = txtsetuppath.Text;
                setup.copyto = txtsetupcopyto.Text;
                plugin.setup.Add(setup);
            }
            else
            {
                setupClass setup = plugin.setup.Find(x => x.path == txtsetuppath.Text.Trim());
                setup.type = txtsetuptype.Text;
                setup.path = txtsetuppath.Text;
                setup.copyto = txtsetupcopyto.Text;
            }
            gridsetup.DataSource = null;
            gridsetup.DataSource = plugin.setup;

        }

        private void btnsetupdelete_Click(object sender, EventArgs e)
        {
            if (gridsetup.CurrentCell == null) return;
            int rowindex = gridsetup.CurrentCell.RowIndex;

            plugin.setup.RemoveAt(rowindex);
            gridsetup.DataSource = null;
            gridsetup.DataSource = plugin.setup;

        }

        private void btnmenuadd_Click(object sender, EventArgs e)
        {
            if (txtmenuname.Text.Trim() == "")
            {
                MessageBoxEx.Show("不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (plugin.menu.FindIndex(x => x.menuname == txtmenuname.Text.Trim()) == -1)
            {
                menuClass menu = new menuClass();
                menu.menuname = txtmenuname.Text;
                menu.pluginname = txtmenupn.Text;
                menu.controllername = txtmenucn.Text;
                menu.viewname = txtmenuvn.Text;
                menu.menupath = txtmenump.Text;
                menu.memo = txtmenumemo.Text;
                plugin.menu.Add(menu);
            }
            else
            {
                menuClass menu = plugin.menu.Find(x => x.menuname == txtmenuname.Text.Trim());
                menu.menuname = txtmenuname.Text;
                menu.pluginname = txtmenupn.Text;
                menu.controllername = txtmenucn.Text;
                menu.viewname = txtmenuvn.Text;
                menu.menupath = txtmenump.Text;
                menu.memo = txtmenumemo.Text;
            }
            gridmenu.DataSource = null;
            gridmenu.DataSource = plugin.menu;
        }

        private void btnmenudelete_Click(object sender, EventArgs e)
        {
            if (gridmenu.CurrentCell == null) return;
            int rowindex = gridmenu.CurrentCell.RowIndex;

            plugin.menu.RemoveAt(rowindex);
            gridmenu.DataSource = null;
            gridmenu.DataSource = plugin.menu;
        }
    }
}
