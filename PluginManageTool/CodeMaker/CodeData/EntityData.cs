using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginManageTool.CodeMaker.CodeData
{
    /*
     * 相关的文件
     * 模板文件：EmptyEntity.cs
     * 配置文件：CodeProperty.xml 必须在EntityData中找到对应的属性
     * 和本类
     * 怎么增加新代码生成模板
     * 第一步，创建模板文件
     * 第二步，补充CodeProperty.xml
     * 第三步，编写Class
     * 
     * 
     * */
    public class EntityData : AbstractCodeTemplateData
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
        private string _propertyList = "";
        //格式如：string|Name,int|Id
        public string PropertyList
        {
            get
            {
                return _propertyList;
            }
            set
            {
                _propertyList = value;
            }
        }
        public List<ClassProperty> Property { get; set; }

        public override void DataInit()
        {
            List<ClassProperty> list = new List<ClassProperty>();
            if (_propertyList.Trim() != "")
            {
                string[] pros= _propertyList.Split(new char[] { ',' });
                for (int i = 0; i < pros.Length; i++)
                {
                    ClassProperty cp = new ClassProperty();
                    cp.TypeName = pros[i].Split(new char[] { '|' })[0];
                    cp.PropertyName = pros[i].Split(new char[] { '|' })[1];
                    cp.varName = "_" + pros[i].Split(new char[] { '|' })[1];
                    list.Add(cp);
                }
            }
            Property = list;
        }
    }

    public class ClassProperty
    {
        public string varName { get; set; }
        public string PropertyName { get; set; }
        public string TypeName { get; set; }
    }
}
