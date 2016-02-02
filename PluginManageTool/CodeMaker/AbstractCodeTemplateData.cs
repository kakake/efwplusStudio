using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginManageTool.CodeMaker
{
    public abstract class AbstractCodeTemplateData
    {
        public string AppName { get; set; }
        public string TemplateFileName { get; set; }
        public string CodeFileNameTopropertyName { get; set; }
        public abstract void DataInit();
    }
}
