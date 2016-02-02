using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginManageTool.CodeMaker.CodeData
{
    public class WebServicesData : AbstractCodeTemplateData
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

        public override void DataInit()
        {
        }
    }
}
