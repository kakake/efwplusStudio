using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginManageTool.CodeMaker.CodeData
{
    public class AspxJSData : AbstractCodeTemplateData
    {
        private string _fileName1 = "";
        public string FileNameAspx
        {
            get { return _fileName1; }
            set { _fileName1 = value; }
        }
        private string _fileName2 = "";
        public string FileNameJS
        {
            get { return _fileName2; }
            set { _fileName2 = value; }
        }
        private bool _IsJquery = true;
        public bool IsJquery
        {
            get { return _IsJquery; }
            set { _IsJquery = value; }
        }

        private bool _IsJqueryEasyUI = false;
        public bool IsJqueryEasyUI
        {
            get { return _IsJqueryEasyUI; }
            set { _IsJqueryEasyUI = value; }
        }

        private bool _Isbootstrap = false;
        public bool Isbootstrap
        {
            get { return _Isbootstrap; }
            set { _Isbootstrap = value; }
        }

        public string includeJS { get; set; }

        public override void DataInit()
        {
            StringBuilder sb = new StringBuilder();
            if (IsJquery)
            {
                sb.Append("	<script type=\"text/javascript\" src=\"../../../WebPlugin/jquery-1.8.0.min.js\"></script>");
            }

            if (IsJqueryEasyUI)
            {
                sb.Append("\r");
                sb.Append("	<link rel=\"stylesheet\" type=\"text/css\" href=\"../../../WebPlugin/jquery-easyui-1.4.2/themes/default/easyui.css\"/>");
                sb.Append("\r");
                sb.Append("	<link rel=\"stylesheet\" type=\"text/css\" href=\"../../../WebPlugin/jquery-easyui-1.4.2/themes/icon.css\"/>");
                sb.Append("\r");
                sb.Append("	<script type=\"text/javascript\" src=\"../../../WebPlugin/jquery-easyui-1.4.2/jquery.easyui.min.js\"></script>");
                sb.Append("\r");
                sb.Append("	<script type=\"text/javascript\" src=\"../../../WebPlugin/jquery-easyui-1.4.2/locale/easyui-lang-zh_CN.js\"></script>");
            }

            if (Isbootstrap)
            {
                sb.Append("\r");
                sb.Append("	<link href=\"../../../WebPlugin/bootstrap/css/bootstrap.min.css\" rel=\"stylesheet\"/>");
                sb.Append("\r");
                sb.Append("	<script type=\"text/javascript\" src=\"../../../WebPlugin/bootstrap/js/bootstrap.min.js\"></script>");
            }

            includeJS = sb.ToString();
        }
    }
}
