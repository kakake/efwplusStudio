using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginManageTool.CodeMaker.CodeData
{
    public class ObjectModelData : AbstractCodeTemplateData
    {
        private string _fileName = "";
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        private string _className = "";
        public string ClassName
        {
            get
            {
                return _className;
            }
            set
            {
                _className = value;
            }
        }

        private string _daoName = "";
        public string DaoName
        {
            get { return _daoName; }
            set { _daoName = value; }
        }

        public string varDao { get; set; }

        public override void DataInit()
        {
            varDao = "_" + _daoName.ToLower();
        }
    }
}
