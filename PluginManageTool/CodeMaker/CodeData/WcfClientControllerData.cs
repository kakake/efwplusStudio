using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginManageTool.CodeMaker.CodeData
{
    public class WcfClientControllerData : AbstractCodeTemplateData
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

        private string _ViewFormName = "";
        public string ViewFormName
        {
            get
            {
                return _ViewFormName;
            }
            set
            {
                _ViewFormName = value;
            }
        }

        private string _IViewName = "";
        public string IViewName
        {
            get
            {
                return _IViewName;
            }
            set
            {
                _IViewName = value;
            }
        }

        public string varIViewName { get; set; }

        public override void DataInit()
        {
            varIViewName = "_" + IViewName.ToLower();
        }
    }
}
