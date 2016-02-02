using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginManageTool.CodeMaker
{
    public class EntityTemplateData
    {
        public string AppName { get; set; }
        public string ClassName { get; set; }
        public string TableName { get; set; }
        public List<ClassProperty> Property { get; set; }
    }

    public class ClassProperty
    {

        public string varName { get; set; }
        public string PropertyName { get; set; }
        public string FieldName { get; set; }
        public string TypeName { get; set; }
        public string DataKey { get; set; }
        public string IsSingleQuote { get; set; }
        public string Match { get; set; }
        public string IsInsert { get; set; }
        public string remarks { get; set; }
    }
}
