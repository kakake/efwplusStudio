using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using PluginManageTool.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluginManageTool
{
    public partial class frmDatabase : MetroForm
    {
        public bool IsConnction = false;
        public string sqlIp;
        public string sqlUsername;
        public string sqlPassword;
        public string dbName;

        public frmDatabase()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtsqlIp.Text == "")
            {
                MessageBoxEx.Show("服务器名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsqlIp.Focus();
                return;
            }

            if (txtsqlUsername.Text == "")
            {
                MessageBoxEx.Show("用户名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsqlUsername.Focus();
                return;
            }

            if (txtsqlPassword.Text == "")
            {
                MessageBoxEx.Show("密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsqlPassword.Focus();
                return;
            }

            if (txtdbName.Text == "")
            {
                MessageBoxEx.Show("数据库名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdbName.Focus();
                return;
            }

            sqlIp = txtsqlIp.Text;
            sqlUsername = txtsqlUsername.Text;
            sqlPassword = txtsqlPassword.Text;
            dbName = txtdbName.Text;

            if (installController.CheckDBConnection(sqlIp, sqlUsername, sqlPassword, "") == false)
            {
                MessageBoxEx.Show("连接数据库服务器失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



            if (installController.CheckDBConnection(sqlIp, sqlUsername, sqlPassword, dbName) == false)
            {
                if (MessageBoxEx.Show("数据库[" + dbName + "]不存在，是否创建此数据库？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;
                else
                {
                    if (installController.CreateDatabase(sqlIp, sqlUsername, sqlPassword, dbName) == false)
                    {
                        MessageBoxEx.Show("创建数据库失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                if (MessageBoxEx.Show("如果数据库[" + dbName + "]中存在插件中的表将删除重建，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;
            }

            if (installController.CheckDBCollation(sqlIp, sqlUsername, sqlPassword, dbName) == false)
            {
                MessageBoxEx.Show("数据库排序规则不是简体中文,请调整为简体中文后重新运行安装程序！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            IsConnction = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
