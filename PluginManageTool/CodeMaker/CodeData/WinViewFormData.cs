using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginManageTool.CodeMaker.CodeData
{
    public class WinViewFormData : AbstractCodeTemplateData
    {
        private string _fileName1 = "";
        public string FileNameViewForm
        {
            get { return _fileName1; }
            set { _fileName1 = value; }
        }

        private string _fileName2 = "";
        public string FileNameDesigner
        {
            get { return _fileName2; }
            set { _fileName2 = value; }
        }

        private string _fileName3 = "";
        public string FileNameIView
        {
            get { return _fileName3; }
            set { _fileName3 = value; }
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

        private string _IFaceName = "";
        public string IFaceName
        {
            get
            {
                return _IFaceName;
            }
            set
            {
                _IFaceName = value;
            }
        }

        public string IViewformName { get; set; }

        public override void DataInit()
        {
            _fileName2 = _fileName1.Replace(".cs","") + ".Designer.cs";
            IViewformName = _IFaceName;
        }
    }
}
